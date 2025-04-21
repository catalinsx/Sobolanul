using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sobolanul_server
{
    public partial class DisplayListForm : Form
    {
        private string clientIP; // client ip address
        private Action<string> sendDataAction; // is a delegate which receives a string and return nothing, used to send xml code 
        private string[] displayResolutions; // string vector for resolutions

        public DisplayListForm(string clientIP, string[] displays, Action<string> sendDataFunc)
        {
            InitializeComponent();

            this.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                this.Width,
                this.Height,
                20, 20
            ));


            this.clientIP = clientIP;
            this.sendDataAction = sendDataFunc;
            this.displayResolutions = displays;


            for (int i = 0; i < displayResolutions.Length; i++)
            {
                listBox1.Items.Add($"index={i} ({displayResolutions[i]})");
            }

            if (displayResolutions.Length == 1)
                listBox1.SelectedIndex = 0;
        }

        private void DisplayListForm_Load(object sender, EventArgs e)
        {

        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Select a display first");
                return;
            }

            int displayIndex = listBox1.SelectedIndex;
            string cmd = $"<screenshot index=\"{displayIndex}\"></screenshot>";
            sendDataAction(cmd);

            Close();
        }


        // it sends a fake message to windows system forms to simulate that the user clicked the title bar to move the window around
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

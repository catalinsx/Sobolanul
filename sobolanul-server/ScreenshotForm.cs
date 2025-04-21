using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace sobolanul_server
{
    public partial class ScreenshotForm : Form
    {
        private Bitmap screenshotBitmap;

        public ScreenshotForm(byte[] pngData)
        {
            InitializeComponent();

            this.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                this.Width,
                this.Height,
                20, 20
            ));

            // png -> bmp
            // transforming that pngData, byte array, to a bitmap that can be shown in the form 
            using (var ms = new MemoryStream(pngData))
            {
                screenshotBitmap = new Bitmap(ms);
            }

            pictureBox1.Image = screenshotBitmap;
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {

        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (screenshotBitmap == null) return;

             // when user click on the save button opens a savefiledialog to ask him where to save the screenshot
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Files|*.png|All Files|*.*";
                sfd.Title = "Save Screenshot";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    screenshotBitmap.Save(sfd.FileName, ImageFormat.Png);
                    MessageBox.Show("Screenshot saved successfully!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(this.Handle, WinApi.WM_NCLBUTTONDOWN, WinApi.HT_CAPTION, 0);
        }
    }
}

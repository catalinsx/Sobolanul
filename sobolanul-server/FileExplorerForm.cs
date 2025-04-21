using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace sobolanul_server
{
    public partial class FileExplorerForm : Form
    {
        private string clientIP; // client ip
        private MainForm mainForm; // mainform object
        private string currentPath = "C:\\"; 


        public FileExplorerForm(string clientIP, MainForm mainForm)
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(
                0, 0,
                this.Width,
                this.Height,
                20, 20
            ));

            this.clientIP = clientIP;
            this.mainForm = mainForm;

            pathBox.Text = currentPath;
            Width = 800;
            Height = 450;

            LoadDirectory(currentPath);
        }

        // used whenever the user clicks on some files, use the load/go button.
        public void LoadDirectory(string path)
        {
            currentPath = path;
            listView.Items.Clear();
            //CDATA - it is a special section in XML document that tell the parser that inside it is raw text, not xml code
            // creating a cdata path to be send through an xml tag that can be processed
            string cdataPath = $"<![CDATA[{path}]]>";

            mainForm.SendDataToClient(clientIP, $"<listdir>{cdataPath}</listdir>");
        }



        public void PopulateList(List<(string name, bool isFolder, long size)> items)
        {
            listView.Items.Clear();
            foreach (var it in items)
            {
                var lvi = new ListViewItem(it.name); // create a listviewitem using the name of a folder/document
                // ading or not the size in the second column
                lvi.SubItems.Add(it.isFolder ? "" : it.size.ToString());
                lvi.Tag = it.isFolder ? "folder" : "file";
                listView.Items.Add(lvi);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(this.Handle, WinApi.WM_NCLBUTTONDOWN, WinApi.HT_CAPTION, 0);
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0) return;

            var item = listView.SelectedItems[0];
            bool isFolder = (string)item.Tag == "folder";
            string name = item.Text;
            string nextPath = System.IO.Path.Combine(currentPath, name); // combining the actual name + currentpath eg. (C:\Users)

            if (isFolder)
            {
                LoadDirectory(nextPath);
            }
            else
            {
                // if it's a file, send it for download through download xml command"
                var result = MessageBox.Show($"Download {name}?", "Download", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    mainForm.SendDataToClient(clientIP, $"<download>{nextPath}</download>");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadClick(object sender, EventArgs e) => LoadDirectory(pathBox.Text);
    }
}

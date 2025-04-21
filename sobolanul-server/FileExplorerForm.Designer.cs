namespace sobolanul_server
{
    partial class FileExplorerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button1 = new Button();
            listView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            panel2 = new Panel();
            goButton = new Button();
            pathBox = new TextBox();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 36);
            panel1.TabIndex = 0;
            panel1.MouseDown += panel1_MouseDown;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(0, 126, 249);
            button1.Location = new Point(774, 0);
            button1.Name = "button1";
            button1.Size = new Size(26, 36);
            button1.TabIndex = 4;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listView
            // 
            listView.BackColor = Color.FromArgb(46, 51, 73);
            listView.BorderStyle = BorderStyle.None;
            listView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView.Dock = DockStyle.Fill;
            listView.Font = new Font("Nirmala UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listView.ForeColor = Color.FromArgb(0, 126, 249);
            listView.FullRowSelect = true;
            listView.Location = new Point(0, 0);
            listView.Name = "listView";
            listView.Size = new Size(800, 372);
            listView.TabIndex = 1;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.DoubleClick += listView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 600;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Size";
            columnHeader2.Width = 150;
            // 
            // panel2
            // 
            panel2.Controls.Add(goButton);
            panel2.Controls.Add(pathBox);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 36);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 42);
            panel2.TabIndex = 2;
            // 
            // goButton
            // 
            goButton.BackColor = Color.FromArgb(37, 42, 64);
            goButton.Dock = DockStyle.Fill;
            goButton.FlatAppearance.BorderSize = 0;
            goButton.FlatStyle = FlatStyle.Flat;
            goButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            goButton.ForeColor = Color.FromArgb(0, 126, 249);
            goButton.Location = new Point(754, 0);
            goButton.Name = "goButton";
            goButton.Size = new Size(46, 42);
            goButton.TabIndex = 1;
            goButton.Text = "Go";
            goButton.UseVisualStyleBackColor = false;
            goButton.Click += LoadClick;
            // 
            // pathBox
            // 
            pathBox.BackColor = Color.FromArgb(37, 42, 64);
            pathBox.BorderStyle = BorderStyle.None;
            pathBox.Dock = DockStyle.Left;
            pathBox.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pathBox.ForeColor = Color.FromArgb(0, 126, 249);
            pathBox.Location = new Point(0, 0);
            pathBox.Multiline = true;
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(754, 42);
            pathBox.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(listView);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 78);
            panel3.Name = "panel3";
            panel3.Size = new Size(800, 372);
            panel3.TabIndex = 3;
            // 
            // FileExplorerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(800, 450);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FileExplorerForm";
            Text = "FileExplorerForm";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListView listView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button button1;
        private Panel panel2;
        private Panel panel3;
        private Button goButton;
        private TextBox pathBox;
    }
}
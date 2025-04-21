namespace sobolanul_server
{
    partial class DisplayListForm
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
            CaptureButton = new Button();
            listBox1 = new ListBox();
            panel1 = new Panel();
            button1 = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // CaptureButton
            // 
            CaptureButton.BackColor = Color.FromArgb(24, 30, 54);
            CaptureButton.Dock = DockStyle.Bottom;
            CaptureButton.FlatAppearance.BorderSize = 0;
            CaptureButton.FlatStyle = FlatStyle.Flat;
            CaptureButton.Font = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CaptureButton.ForeColor = Color.FromArgb(0, 126, 249);
            CaptureButton.Location = new Point(0, 214);
            CaptureButton.Name = "CaptureButton";
            CaptureButton.Size = new Size(206, 47);
            CaptureButton.TabIndex = 0;
            CaptureButton.Text = "Capture";
            CaptureButton.UseVisualStyleBackColor = false;
            CaptureButton.Click += CaptureButton_Click;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(46, 51, 73);
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.Dock = DockStyle.Fill;
            listBox1.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBox1.ForeColor = Color.White;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(206, 183);
            listBox1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(37, 42, 64);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(206, 31);
            panel1.TabIndex = 2;
            panel1.MouseDown += panel1_MouseDown;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(0, 126, 249);
            button1.Location = new Point(180, 0);
            button1.Name = "button1";
            button1.Size = new Size(26, 31);
            button1.TabIndex = 1;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(46, 51, 73);
            panel2.Controls.Add(listBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 31);
            panel2.Name = "panel2";
            panel2.Size = new Size(206, 183);
            panel2.TabIndex = 3;
            // 
            // DisplayListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(206, 261);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(CaptureButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DisplayListForm";
            Text = "DisplayListForm";
            Load += DisplayListForm_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button CaptureButton;
        private ListBox listBox1;
        private Panel panel1;
        private Panel panel2;
        private Button button1;
    }
}
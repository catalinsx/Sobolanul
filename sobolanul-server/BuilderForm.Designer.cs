namespace sobolanul_server
{
    partial class BuilderForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox ipTextBox;
        private NumericUpDown portNumericUpDown;
        private TextBox iconTextBox;
        private Button buildButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ipTextBox = new TextBox();
            portNumericUpDown = new NumericUpDown();
            iconTextBox = new TextBox();
            buildButton = new Button();
            browseButton = new Button();
            panel1 = new Panel();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)portNumericUpDown).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ipTextBox
            // 
            ipTextBox.BackColor = Color.FromArgb(37, 42, 64);
            ipTextBox.BorderStyle = BorderStyle.None;
            ipTextBox.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ipTextBox.ForeColor = Color.FromArgb(0, 126, 249);
            ipTextBox.Location = new Point(12, 49);
            ipTextBox.Name = "ipTextBox";
            ipTextBox.Size = new Size(150, 18);
            ipTextBox.TabIndex = 0;
            // 
            // portNumericUpDown
            // 
            portNumericUpDown.BackColor = Color.FromArgb(37, 42, 64);
            portNumericUpDown.BorderStyle = BorderStyle.None;
            portNumericUpDown.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            portNumericUpDown.ForeColor = Color.FromArgb(0, 126, 249);
            portNumericUpDown.Location = new Point(168, 49);
            portNumericUpDown.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            portNumericUpDown.Name = "portNumericUpDown";
            portNumericUpDown.Size = new Size(80, 21);
            portNumericUpDown.TabIndex = 1;
            // 
            // iconTextBox
            // 
            iconTextBox.BackColor = Color.FromArgb(37, 42, 64);
            iconTextBox.BorderStyle = BorderStyle.None;
            iconTextBox.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconTextBox.ForeColor = Color.FromArgb(0, 126, 249);
            iconTextBox.Location = new Point(12, 75);
            iconTextBox.Name = "iconTextBox";
            iconTextBox.Size = new Size(236, 18);
            iconTextBox.TabIndex = 2;
            // 
            // buildButton
            // 
            buildButton.BackColor = Color.FromArgb(24, 30, 54);
            buildButton.Dock = DockStyle.Bottom;
            buildButton.FlatAppearance.BorderSize = 0;
            buildButton.FlatStyle = FlatStyle.Flat;
            buildButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buildButton.ForeColor = Color.FromArgb(0, 126, 249);
            buildButton.Location = new Point(0, 117);
            buildButton.Name = "buildButton";
            buildButton.Size = new Size(341, 23);
            buildButton.TabIndex = 4;
            buildButton.Text = "Build";
            buildButton.UseVisualStyleBackColor = false;
            buildButton.Click += buildButton_Click;
            // 
            // browseButton
            // 
            browseButton.BackColor = Color.FromArgb(37, 42, 64);
            browseButton.FlatAppearance.BorderSize = 0;
            browseButton.FlatStyle = FlatStyle.Flat;
            browseButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            browseButton.ForeColor = Color.FromArgb(0, 126, 249);
            browseButton.Location = new Point(254, 70);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 5;
            browseButton.Text = "Pick";
            browseButton.UseVisualStyleBackColor = false;
            browseButton.Click += browseButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(37, 42, 64);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(341, 27);
            panel1.TabIndex = 6;
            panel1.MouseDown += panel1_MouseDown;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(0, 126, 249);
            button1.Location = new Point(315, 0);
            button1.Name = "button1";
            button1.Size = new Size(26, 27);
            button1.TabIndex = 2;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // BuilderForm
            // 
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(341, 140);
            Controls.Add(panel1);
            Controls.Add(browseButton);
            Controls.Add(buildButton);
            Controls.Add(iconTextBox);
            Controls.Add(portNumericUpDown);
            Controls.Add(ipTextBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BuilderForm";
            Text = "Stub Builder";
            Load += BuilderForm_Load;
            ((System.ComponentModel.ISupportInitialize)portNumericUpDown).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Button browseButton;
        private Panel panel1;
        private Button button1;
    }
}
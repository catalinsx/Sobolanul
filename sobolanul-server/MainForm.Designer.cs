namespace sobolanul_server
{
    partial class MainForm
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
            clientsPanel = new FlowLayoutPanel();
            refreshButton = new Button();
            consolePanel = new Panel();
            consoleTextBox = new TextBox();
            toggleConsoleButton = new Button();
            StubBuilder = new Button();
            panel1 = new Panel();
            menuPanel = new Panel();
            button1 = new Button();
            consolePanel.SuspendLayout();
            panel1.SuspendLayout();
            menuPanel.SuspendLayout();
            SuspendLayout();
            // 
            // clientsPanel
            // 
            clientsPanel.AutoScroll = true;
            clientsPanel.BackColor = Color.FromArgb(37, 42, 64);
            clientsPanel.FlowDirection = FlowDirection.TopDown;
            clientsPanel.Location = new Point(132, 32);
            clientsPanel.Name = "clientsPanel";
            clientsPanel.Size = new Size(656, 301);
            clientsPanel.TabIndex = 0;
            clientsPanel.WrapContents = false;
            // 
            // refreshButton
            // 
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            refreshButton.ForeColor = Color.FromArgb(0, 126, 249);
            refreshButton.Location = new Point(3, 90);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(120, 39);
            refreshButton.TabIndex = 1;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            // 
            // consolePanel
            // 
            consolePanel.BackColor = Color.Black;
            consolePanel.Controls.Add(consoleTextBox);
            consolePanel.Dock = DockStyle.Bottom;
            consolePanel.Location = new Point(0, 250);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new Size(800, 200);
            consolePanel.TabIndex = 2;
            consolePanel.Visible = false;
            // 
            // consoleTextBox
            // 
            consoleTextBox.BackColor = Color.Black;
            consoleTextBox.BorderStyle = BorderStyle.None;
            consoleTextBox.Dock = DockStyle.Fill;
            consoleTextBox.ForeColor = Color.FromArgb(0, 192, 0);
            consoleTextBox.Location = new Point(0, 0);
            consoleTextBox.Multiline = true;
            consoleTextBox.Name = "consoleTextBox";
            consoleTextBox.ReadOnly = true;
            consoleTextBox.ScrollBars = ScrollBars.Vertical;
            consoleTextBox.Size = new Size(800, 200);
            consoleTextBox.TabIndex = 0;
            // 
            // toggleConsoleButton
            // 
            toggleConsoleButton.FlatAppearance.BorderSize = 0;
            toggleConsoleButton.FlatStyle = FlatStyle.Flat;
            toggleConsoleButton.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toggleConsoleButton.ForeColor = Color.FromArgb(0, 126, 249);
            toggleConsoleButton.Location = new Point(3, 147);
            toggleConsoleButton.Name = "toggleConsoleButton";
            toggleConsoleButton.Size = new Size(120, 39);
            toggleConsoleButton.TabIndex = 0;
            toggleConsoleButton.Text = "Console";
            toggleConsoleButton.UseVisualStyleBackColor = true;
            toggleConsoleButton.Click += toggleConsoleButton_Click;
            // 
            // StubBuilder
            // 
            StubBuilder.FlatAppearance.BorderSize = 0;
            StubBuilder.FlatStyle = FlatStyle.Flat;
            StubBuilder.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StubBuilder.ForeColor = Color.FromArgb(0, 126, 249);
            StubBuilder.Location = new Point(3, 33);
            StubBuilder.Name = "StubBuilder";
            StubBuilder.Size = new Size(120, 39);
            StubBuilder.TabIndex = 3;
            StubBuilder.Text = "Builder";
            StubBuilder.UseVisualStyleBackColor = true;
            StubBuilder.Click += StubBuilder_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(37, 42, 64);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 26);
            panel1.TabIndex = 4;
            panel1.MouseDown += panel1_MouseDown;
            // 
            // menuPanel
            // 
            menuPanel.BackColor = Color.FromArgb(24, 30, 54);
            menuPanel.Controls.Add(StubBuilder);
            menuPanel.Controls.Add(refreshButton);
            menuPanel.Controls.Add(toggleConsoleButton);
            menuPanel.Dock = DockStyle.Left;
            menuPanel.Location = new Point(0, 26);
            menuPanel.Name = "menuPanel";
            menuPanel.Size = new Size(126, 224);
            menuPanel.TabIndex = 5;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(0, 126, 249);
            button1.Location = new Point(771, 3);
            button1.Name = "button1";
            button1.Size = new Size(26, 23);
            button1.TabIndex = 0;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(800, 450);
            Controls.Add(menuPanel);
            Controls.Add(panel1);
            Controls.Add(consolePanel);
            Controls.Add(clientsPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            panel1.ResumeLayout(false);
            menuPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel clientsPanel;
        private Button refreshButton;
        private Panel consolePanel;
        private TextBox consoleTextBox;
        private Button toggleConsoleButton;
        private Button StubBuilder;
        private Panel panel1;
        private Panel menuPanel;
        private Button button1;
    }
}
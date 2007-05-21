namespace AntHill.NET
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
            this.components = new System.ComponentModel.Container();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.cbVisualize = new System.Windows.Forms.CheckBox();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.magnitudeBar = new System.Windows.Forms.TrackBar();
            this.magnitudeLabel = new System.Windows.Forms.Label();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.doTurnButton = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.simulationXMLopenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGLControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.cbVisualize);
            this.rightPanel.Controls.Add(this.buttonShowConfig);
            this.rightPanel.Controls.Add(this.magnitudeBar);
            this.rightPanel.Controls.Add(this.magnitudeLabel);
            this.rightPanel.Controls.Add(this.speedBar);
            this.rightPanel.Controls.Add(this.labelSpeed);
            this.rightPanel.Controls.Add(this.btnStop);
            this.rightPanel.Controls.Add(this.startButton);
            this.rightPanel.Controls.Add(this.doTurnButton);
            this.rightPanel.Controls.Add(this.btnReset);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Enabled = false;
            this.rightPanel.Location = new System.Drawing.Point(446, 24);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(143, 425);
            this.rightPanel.TabIndex = 0;
            // 
            // cbVisualize
            // 
            this.cbVisualize.AutoSize = true;
            this.cbVisualize.Checked = true;
            this.cbVisualize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisualize.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbVisualize.Location = new System.Drawing.Point(0, 226);
            this.cbVisualize.Name = "cbVisualize";
            this.cbVisualize.Size = new System.Drawing.Size(143, 17);
            this.cbVisualize.TabIndex = 6;
            this.cbVisualize.Text = "Visualize simulation";
            this.cbVisualize.UseVisualStyleBackColor = true;
            this.cbVisualize.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonShowConfig.Location = new System.Drawing.Point(0, 202);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(143, 24);
            this.buttonShowConfig.TabIndex = 0;
            this.buttonShowConfig.Text = "Show Config";
            this.buttonShowConfig.UseVisualStyleBackColor = true;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // magnitudeBar
            // 
            this.magnitudeBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.magnitudeBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.magnitudeBar.Location = new System.Drawing.Point(0, 160);
            this.magnitudeBar.Maximum = 1000;
            this.magnitudeBar.Name = "magnitudeBar";
            this.magnitudeBar.Size = new System.Drawing.Size(143, 42);
            this.magnitudeBar.TabIndex = 1;
            this.magnitudeBar.TickFrequency = 0;
            this.magnitudeBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.magnitudeBar.Value = 1000;
            this.magnitudeBar.Scroll += new System.EventHandler(this.magnitudeBar_Scroll);
            // 
            // magnitudeLabel
            // 
            this.magnitudeLabel.AutoSize = true;
            this.magnitudeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.magnitudeLabel.Location = new System.Drawing.Point(0, 147);
            this.magnitudeLabel.Name = "magnitudeLabel";
            this.magnitudeLabel.Size = new System.Drawing.Size(60, 13);
            this.magnitudeLabel.TabIndex = 3;
            this.magnitudeLabel.Text = "Magnitude:";
            // 
            // speedBar
            // 
            this.speedBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.speedBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.speedBar.LargeChange = 1;
            this.speedBar.Location = new System.Drawing.Point(0, 105);
            this.speedBar.Maximum = 1000;
            this.speedBar.Minimum = 10;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(143, 42);
            this.speedBar.TabIndex = 5;
            this.speedBar.TickFrequency = 0;
            this.speedBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.speedBar.Value = 750;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSpeed.Location = new System.Drawing.Point(0, 92);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelSpeed.TabIndex = 7;
            this.labelSpeed.Text = "Speed:";
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStop.Location = new System.Drawing.Point(0, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(143, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.startButton.Location = new System.Drawing.Point(0, 46);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(143, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // doTurnButton
            // 
            this.doTurnButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.doTurnButton.Location = new System.Drawing.Point(0, 23);
            this.doTurnButton.Name = "doTurnButton";
            this.doTurnButton.Size = new System.Drawing.Size(143, 23);
            this.doTurnButton.TabIndex = 3;
            this.doTurnButton.Text = "Do Turn";
            this.doTurnButton.UseVisualStyleBackColor = true;
            this.doTurnButton.Click += new System.EventHandler(this.doTurnButton_Click);
            // 
            // btnReset
            // 
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReset.Location = new System.Drawing.Point(0, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(143, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.Location = new System.Drawing.Point(430, 24);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 425);
            this.vScrollBar1.SmallChange = 2;
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.Scrolled);
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 433);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(430, 16);
            this.hScrollBar1.SmallChange = 2;
            this.hScrollBar1.TabIndex = 2;
            this.hScrollBar1.Value = 80;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.Scrolled);
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // timer
            // 
            this.timer.Interval = 350;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(589, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadDataToolStripMenuItem.Text = "Load Data";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadData);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openGLControl
            // 
            this.openGLControl.AccumBits = ((byte)(0));
            this.openGLControl.AutoCheckErrors = false;
            this.openGLControl.AutoFinish = false;
            this.openGLControl.AutoMakeCurrent = true;
            this.openGLControl.AutoSwapBuffers = true;
            this.openGLControl.BackColor = System.Drawing.Color.Black;
            this.openGLControl.ColorBits = ((byte)(32));
            this.openGLControl.DepthBits = ((byte)(32));
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.Location = new System.Drawing.Point(0, 24);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.Size = new System.Drawing.Size(430, 409);
            this.openGLControl.StencilBits = ((byte)(0));
            this.openGLControl.TabIndex = 4;
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.openGLControl_Paint);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 449);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "AntHill.NET";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Button doTurnButton;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox cbVisualize;
        private System.Windows.Forms.OpenFileDialog simulationXMLopenFileDialog;
        private System.Windows.Forms.Button buttonShowConfig;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TrackBar magnitudeBar;
        private System.Windows.Forms.Label magnitudeLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private Tao.Platform.Windows.SimpleOpenGlControl openGLControl;
    }
}


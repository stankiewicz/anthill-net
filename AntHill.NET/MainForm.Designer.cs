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
            this.simulationPanel = new System.Windows.Forms.Panel();
            this.panelDebug = new System.Windows.Forms.Panel();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.magnitudePanel = new System.Windows.Forms.Panel();
            this.magnitudeBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.doTurnButton = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.simulationXMLopenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.rightPanel.SuspendLayout();
            this.simulationPanel.SuspendLayout();
            this.panelDebug.SuspendLayout();
            this.magnitudePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.simulationPanel);
            this.rightPanel.Controls.Add(this.loadDataButton);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(509, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(80, 449);
            this.rightPanel.TabIndex = 0;
            // 
            // simulationPanel
            // 
            this.simulationPanel.Controls.Add(this.panelDebug);
            this.simulationPanel.Controls.Add(this.magnitudePanel);
            this.simulationPanel.Controls.Add(this.btnStop);
            this.simulationPanel.Controls.Add(this.checkBox1);
            this.simulationPanel.Controls.Add(this.startButton);
            this.simulationPanel.Controls.Add(this.doTurnButton);
            this.simulationPanel.Controls.Add(this.btnReset);
            this.simulationPanel.Controls.Add(this.speedBar);
            this.simulationPanel.Controls.Add(this.labelSpeed);
            this.simulationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simulationPanel.Enabled = false;
            this.simulationPanel.Location = new System.Drawing.Point(0, 68);
            this.simulationPanel.Name = "simulationPanel";
            this.simulationPanel.Size = new System.Drawing.Size(80, 381);
            this.simulationPanel.TabIndex = 8;
            // 
            // panelDebug
            // 
            this.panelDebug.Controls.Add(this.buttonShowConfig);
            this.panelDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDebug.Location = new System.Drawing.Point(0, 357);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.Size = new System.Drawing.Size(80, 24);
            this.panelDebug.TabIndex = 8;
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonShowConfig.Location = new System.Drawing.Point(0, 0);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(80, 24);
            this.buttonShowConfig.TabIndex = 0;
            this.buttonShowConfig.Text = "Show Config";
            this.buttonShowConfig.UseVisualStyleBackColor = true;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // magnitudePanel
            // 
            this.magnitudePanel.Controls.Add(this.magnitudeBar);
            this.magnitudePanel.Controls.Add(this.label1);
            this.magnitudePanel.Location = new System.Drawing.Point(2, 182);
            this.magnitudePanel.Name = "magnitudePanel";
            this.magnitudePanel.Size = new System.Drawing.Size(76, 134);
            this.magnitudePanel.TabIndex = 3;
            this.magnitudePanel.Visible = false;
            // 
            // magnitudeBar
            // 
            this.magnitudeBar.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.magnitudeBar.Location = new System.Drawing.Point(14, 3);
            this.magnitudeBar.Maximum = 10000;
            this.magnitudeBar.Minimum = 1000;
            this.magnitudeBar.Name = "magnitudeBar";
            this.magnitudeBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.magnitudeBar.Size = new System.Drawing.Size(45, 104);
            this.magnitudeBar.TabIndex = 3;
            this.magnitudeBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.magnitudeBar.Value = 1000;
            this.magnitudeBar.Scroll += new System.EventHandler(this.magnitudeBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Magnitude";
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStop.Location = new System.Drawing.Point(0, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(5, 159);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Show";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.startButton.Location = new System.Drawing.Point(0, 46);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(80, 23);
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
            this.doTurnButton.Size = new System.Drawing.Size(80, 23);
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
            this.btnReset.Size = new System.Drawing.Size(80, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // speedBar
            // 
            this.speedBar.LargeChange = 1;
            this.speedBar.Location = new System.Drawing.Point(2, 111);
            this.speedBar.Maximum = 5;
            this.speedBar.Minimum = 1;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(75, 45);
            this.speedBar.TabIndex = 5;
            this.speedBar.Value = 1;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(2, 95);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelSpeed.TabIndex = 7;
            this.labelSpeed.Text = "Speed:";
            // 
            // loadDataButton
            // 
            this.loadDataButton.Location = new System.Drawing.Point(0, 40);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(80, 22);
            this.loadDataButton.TabIndex = 0;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadDataButton_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.Location = new System.Drawing.Point(493, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 449);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 433);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(493, 16);
            this.hScrollBar1.TabIndex = 2;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 449);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.rightPanel);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "AntHill";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.rightPanel.ResumeLayout(false);
            this.simulationPanel.ResumeLayout(false);
            this.simulationPanel.PerformLayout();
            this.panelDebug.ResumeLayout(false);
            this.magnitudePanel.ResumeLayout(false);
            this.magnitudePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magnitudeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Button doTurnButton;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel simulationPanel;
        private System.Windows.Forms.OpenFileDialog simulationXMLopenFileDialog;
        private System.Windows.Forms.Panel panelDebug;
        private System.Windows.Forms.Button buttonShowConfig;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TrackBar magnitudeBar;
        private System.Windows.Forms.Panel magnitudePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
    }
}


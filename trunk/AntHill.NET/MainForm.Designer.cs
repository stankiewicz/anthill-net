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
            this.rightPanel = new System.Windows.Forms.Panel();
            this.simulationPanel = new System.Windows.Forms.Panel();
            this.pauseButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.doTurnButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.simulationXMLopenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.rightPanel.SuspendLayout();
            this.simulationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.simulationPanel);
            this.rightPanel.Controls.Add(this.loadDataButton);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(603, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(80, 437);
            this.rightPanel.TabIndex = 0;
            // 
            // simulationPanel
            // 
            this.simulationPanel.Controls.Add(this.pauseButton);
            this.simulationPanel.Controls.Add(this.checkBox1);
            this.simulationPanel.Controls.Add(this.startButton);
            this.simulationPanel.Controls.Add(this.doTurnButton);
            this.simulationPanel.Controls.Add(this.stopButton);
            this.simulationPanel.Controls.Add(this.speedBar);
            this.simulationPanel.Controls.Add(this.labelSpeed);
            this.simulationPanel.Enabled = false;
            this.simulationPanel.Location = new System.Drawing.Point(0, 28);
            this.simulationPanel.Name = "simulationPanel";
            this.simulationPanel.Size = new System.Drawing.Size(80, 183);
            this.simulationPanel.TabIndex = 8;
            // 
            // pauseButton
            // 
            this.pauseButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pauseButton.Location = new System.Drawing.Point(0, 69);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(80, 23);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
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
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.stopButton.Location = new System.Drawing.Point(0, 0);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(80, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // speedBar
            // 
            this.speedBar.LargeChange = 1;
            this.speedBar.Location = new System.Drawing.Point(2, 111);
            this.speedBar.Maximum = 5;
            this.speedBar.Minimum = 1;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(75, 42);
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
            this.loadDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadDataButton.Location = new System.Drawing.Point(0, 0);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(80, 22);
            this.loadDataButton.TabIndex = 0;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadDataButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 437);
            this.Controls.Add(this.rightPanel);
            this.Name = "MainForm";
            this.Text = "AntHill";
            this.rightPanel.ResumeLayout(false);
            this.simulationPanel.ResumeLayout(false);
            this.simulationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Button doTurnButton;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel simulationPanel;
        private System.Windows.Forms.OpenFileDialog simulationXMLopenFileDialog;
    }
}


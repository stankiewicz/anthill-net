using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AntHill.NET.Forms;

namespace AntHill.NET
{
    public partial class MainForm : Form
    {
        private ConfigForm cf = null;
        
        bool scrolling = false;
        Point mousePos;
        Rectangle drawingRect;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                AHGraphics.Init();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Exit();
            }

            cf = new ConfigForm();
            drawingRect = new Rectangle();
        }

        private void loadData(object sender, EventArgs e)
        {
            this.Resize -= new EventHandler(UpdateMap);

            string name;
            if (simulationXMLopenFileDialog.ShowDialog() == DialogResult.OK)
            {
                name = simulationXMLopenFileDialog.FileName;
                XmlReaderWriter reader = new XmlReaderWriter();
                try
                {
                    reader.ReadMe(name);
                    Simulation.DeInit();
                    Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
                }
                catch (Exception exc)
                {
                    Invalidate();
                    rightPanel.Enabled = false;

                    MessageBox.Show(exc.Message);
                    return;
                }

                rightPanel.Enabled = true;

                doTurnButton.Enabled = true;
                startButton.Enabled = true;
                btnStop.Enabled = false;
                btnReset.Enabled = true;

                this.Resize += new EventHandler(UpdateMap);
                RecalculateUI();
                Invalidate();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            doTurnButton.Enabled = false;
            startButton.Enabled = false;
            btnStop.Enabled = true;
            btnReset.Enabled = true;

            timer.Start();
            //((ISimulationUser)Simulation.simulation).Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            doTurnButton.Enabled = true;
            startButton.Enabled = true;
            btnStop.Enabled = false;
            btnReset.Enabled = true;                        
        }

        private void doTurnButton_Click(object sender, EventArgs e)
        {
            //doTurnButton.Enabled = true;
            //startButton.Enabled = true;
            btnReset.Enabled = true;
            //btnStop.Enabled = false;

            if (((ISimulationUser)Simulation.simulation).DoTurn() == false)
            {
                Invalidate();
                MessageBox.Show(Properties.Resources.SimulationFinished);
            }
            
            Invalidate();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer.Stop();

            startButton.Enabled = true;
            btnReset.Enabled = true;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;

            //((ISimulationUser)Simulation.simulation).Pause();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (((ISimulationUser)Simulation.simulation).DoTurn() == false)
            {
                timer.Stop();
                MessageBox.Show(Properties.Resources.SimulationFinished);
            }
            Invalidate();
        }

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            cf.RefreshData();
            cf.Show();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Faster drawing
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.Clear(Color.Black);

            //Check whether we really need to draw the map
            if ((Simulation.simulation == null) ||
                !cbVisualize.Checked)
                return;

            Matrix mT = new Matrix();
            mT.Translate(0, menuStrip1.Height);
            g.Transform = mT;

            //Init variables
            float tileSize = AntHillConfig.tileSize,
                realWidth = Simulation.simulation.Map.Width * tileSize,
                realHeight = Simulation.simulation.Map.Height * tileSize,
                magnitude = AntHillConfig.curMagnitude,
                realTileSize = tileSize * magnitude,
                offX = hScrollBar1.Value, offY = vScrollBar1.Value;

            Simulation.simulation.Map.DrawMap(g, drawingRect,
                offX, offY, magnitude);

            g.SetClip(drawingRect);

            foreach (Food f in Simulation.simulation.food)
                AHGraphics.DrawElement(g, f, realTileSize, offX, offY);                

            foreach (Ant ant in Simulation.simulation.ants)
                AHGraphics.DrawElement(g, ant, realTileSize, offX, offY);

            foreach (Spider spider in Simulation.simulation.spiders)
                AHGraphics.DrawElement(g, spider, realTileSize, offX, offY);

            if (Simulation.simulation.queen != null)
                AHGraphics.DrawElement(g, Simulation.simulation.queen, realTileSize, offX, offY);

            if (Simulation.simulation.rain != null)
                AHGraphics.DrawElement(g, Simulation.simulation.rain, realTileSize, offX, offY);
        }        
        
        private void UpdateMap(object sender, EventArgs e)
        {
            RecalculateUI();
            Invalidate();
        }

        private void RecalculateUI()
        {
            float mapWidth = Simulation.simulation.Map.Width,
                mapHeight = Simulation.simulation.Map.Height,
                tileSize = AntHillConfig.tileSize,
                realWidth = mapWidth * tileSize,
                realHeight = mapHeight * tileSize,
                xRatio, yRatio, magnitude;

            drawingRect = new Rectangle(0, 0,
                                    rightPanel.Location.X - 1 - vScrollBar1.Width,
                                    ClientSize.Height - hScrollBar1.Height - menuStrip1.Height);

            xRatio = ((float)drawingRect.Width) / realWidth;
            yRatio = ((float)drawingRect.Height) / realHeight;
            magnitudeBar.Minimum = (int)(1000.0f * Math.Min(xRatio, yRatio));
            magnitudeBar.Maximum = (int)(Math.Max(magnitudeBar.Minimum * 2, 2000));
            if (AntHillConfig.curMagnitude < ((float)magnitudeBar.Minimum) / 1000.0f)
                AntHillConfig.curMagnitude = ((float)magnitudeBar.Minimum) / 1000.0f;
            else
                if (AntHillConfig.curMagnitude > ((float)magnitudeBar.Maximum) / 1000.0f)
                    AntHillConfig.curMagnitude = ((float)magnitudeBar.Maximum) / 1000.0f;
            magnitude = AntHillConfig.curMagnitude;

            hScrollBar1.Enabled = (realWidth * magnitude > drawingRect.Width);
            vScrollBar1.Enabled = (realHeight * magnitude > drawingRect.Height);

            hScrollBar1.Maximum = (int)Math.Max(realWidth * magnitude - drawingRect.Width, 0);
            vScrollBar1.Maximum = (int)Math.Max(realHeight * magnitude - drawingRect.Height, 0);
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = speedBar.Value;
        }

        private void magnitudeBar_Scroll(object sender, EventArgs e)
        {
            AntHillConfig.curMagnitude = ((float)magnitudeBar.Value) / 1000.0f;
            cf.RefreshData();
            RecalculateUI();
            Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Simulation.DeInit();
            Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

            startButton.Enabled = true;
            //btnReset.Enabled = true;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;

            Invalidate();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mousePos = e.Location;
                scrolling = true;                
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            scrolling = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (scrolling)
            {
                int hVal = hScrollBar1.Value - (e.X - mousePos.X),
                    vVal = vScrollBar1.Value - (e.Y - mousePos.Y);
                hScrollBar1.Value = Math.Min(hScrollBar1.Maximum, Math.Max(0, hVal));
                vScrollBar1.Value = Math.Min(vScrollBar1.Maximum, Math.Max(0, vVal));
                mousePos = e.Location;
            }
        }

        private void Scrolled(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AntHill.NET
{
    public partial class MainForm : Form
    {
        private ConfigForm cf = null;

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
        }

        private void loadDataButton_Click(object sender, EventArgs e)
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
                    MessageBox.Show(exc.Message);
                    Invalidate();
                    return;
                }

                simulationPanel.Enabled = true;
                startButton.Enabled = true;
                btnReset.Enabled = false;
                doTurnButton.Enabled = true;
                btnStop.Enabled = false;

                this.Resize += new EventHandler(UpdateMap);
                RecalculateUI();
                Invalidate();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            btnReset.Enabled = true;
            doTurnButton.Enabled = false;
            btnStop.Enabled = true;

            timer.Start();
            //((ISimulationUser)Simulation.simulation).Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            btnReset.Enabled = false;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;
            
        }

        private void doTurnButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            btnReset.Enabled = true;
            doTurnButton.Enabled = true;

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
            //Faster drawing
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.Clear(Color.Black);

            //Check whether we really need to draw the map
            if ((Simulation.simulation == null) ||
                !checkBox1.Checked)
                return;

            //Init variables
            float mapWidth = Simulation.simulation.Map.Width,
                mapHeight = Simulation.simulation.Map.Height,
                tileSize = AntHillConfig.tileSize,
                realWidth = mapWidth * tileSize,
                realHeight = mapHeight * tileSize,
                magnitude = AntHillConfig.curMagnitude,
                realTileSize = tileSize * magnitude,
                offX = hScrollBar1.Value, offY = vScrollBar1.Value;
                

            Point drawingRect = new Point(rightPanel.Location.X - 1 - ((vScrollBar1.Visible)?vScrollBar1.Width:0),
                ClientSize.Height - ((hScrollBar1.Visible)?hScrollBar1.Height:0));
            
            Simulation.simulation.Map.DrawMap(e.Graphics, drawingRect.X, drawingRect.Y,
                offX, offY, magnitude);

            //show food
            foreach (Food food in Simulation.simulation.food)
                e.Graphics.DrawImage(AHGraphics.GetFoodBitmap(),
                    food.Position.X * realTileSize - offX,
                    food.Position.Y * realTileSize - offY,
                    realTileSize, realTileSize);

            //show queen
            if (Simulation.simulation.queen != null)
            {
                e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.queen, Simulation.simulation.queen.Direction),
                                    Simulation.simulation.queen.Position.X * realTileSize - offX,
                                    Simulation.simulation.queen.Position.Y * realTileSize - offY,
                                    realTileSize, realTileSize); 
                if (Simulation.simulation.queen.FoodQuantity > 0)                                    
                    e.Graphics.DrawImage(AHGraphics.GetFoodBitmap(),
                                    Simulation.simulation.queen.Position.X * realTileSize - offX,
                                    Simulation.simulation.queen.Position.Y * realTileSize - offY,
                                    realTileSize, realTileSize);
            }
             //Show ants
            foreach (Ant ant in Simulation.simulation.ants)
            {
                if (ant is Warrior)
                    e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.warrior, ant.Direction),
                                         ant.Position.X * realTileSize - offX,
                                         ant.Position.Y * realTileSize - offY,
                                         realTileSize, realTileSize);
                else
                    e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.worker, ant.Direction),
                                         ant.Position.X * realTileSize - offX,
                                         ant.Position.Y * realTileSize - offY,
                                         realTileSize, realTileSize);
            }
                    
             //show spider
             foreach (Spider spider in Simulation.simulation.spiders)
                e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.spider, spider.Direction),
                                     spider.Position.X * realTileSize - offX,
                                     spider.Position.Y * realTileSize - offY,
                                     realTileSize, realTileSize);
            //show rain
            if (Simulation.simulation.rain != null)
                e.Graphics.DrawImage(AHGraphics.GetRainBitmap(),
                        (Simulation.simulation.rain.Position.X - AntHillConfig.rainWidth / 2) * realTileSize - offX,
                        (Simulation.simulation.rain.Position.Y - AntHillConfig.rainWidth / 2) * realTileSize - offY,
                        AntHillConfig.rainWidth * realTileSize, AntHillConfig.rainWidth * realTileSize);
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

            Point drawingRect = new Point(rightPanel.Location.X - 1 - ((vScrollBar1.Visible) ? vScrollBar1.Width : 0),
                ClientSize.Height - ((hScrollBar1.Visible) ? hScrollBar1.Height : 0));

            xRatio = ((float)drawingRect.X) / realWidth;
            yRatio = ((float)drawingRect.Y) / realHeight;
            magnitudeBar.Minimum = (int)(1000.0f * Math.Min(xRatio, yRatio));
            magnitudeBar.Maximum = (int)(Math.Max(magnitudeBar.Minimum * 2, 2000));
            if (AntHillConfig.curMagnitude < (float)magnitudeBar.Minimum / 1000.0f)
                AntHillConfig.curMagnitude = (float)magnitudeBar.Minimum / 1000.0f;
            else if (AntHillConfig.curMagnitude > (float)magnitudeBar.Maximum / 1000.0f)
                AntHillConfig.curMagnitude = (float)magnitudeBar.Minimum / 1000.0f;
            magnitude = AntHillConfig.curMagnitude;

            hScrollBar1.Visible = (realWidth * magnitude > drawingRect.X);
            vScrollBar1.Visible = (realHeight * magnitude > drawingRect.Y);

            hScrollBar1.Maximum = (int)Math.Max(realWidth * magnitude - drawingRect.X, 0);
            vScrollBar1.Maximum = (int)Math.Max(realHeight * magnitude - drawingRect.Y, 0);
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

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}

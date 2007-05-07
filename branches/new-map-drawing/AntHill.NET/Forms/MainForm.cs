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
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            string name;
            if (simulationXMLopenFileDialog.ShowDialog() == DialogResult.OK)
            {
                name = simulationXMLopenFileDialog.FileName;
                XmlReaderWriter reader = new XmlReaderWriter();
                try
                {
                    reader.ReadMe(name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(name + Properties.Resources.exceptionXmlNotValid + ex.ToString(), "Error");
                    return;
                }
                try
                {
                    Simulation.DeInit();
                    Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return;
                }

                simulationPanel.Enabled = true;
                startButton.Enabled = true;
                btnReset.Enabled = false;
                doTurnButton.Enabled = true;
                btnStop.Enabled = false;
                hScrollBar1.Maximum = AntHillConfig.mapColCount;
                vScrollBar1.Maximum = AntHillConfig.mapRowCount;
                hScrollBar1.Value = hScrollBar1.Maximum / 2;
                vScrollBar1.Value = vScrollBar1.Maximum / 2;
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

            if (((ISimulationUser)Simulation.simulation).DoTurn()==false)
            {
                MessageBox.Show("symulacja skonczona");
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

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            if (cf == null)
            {
                cf = new ConfigForm();
                cf.Show();
            }
            else
            {
                try
                {
                    cf.RefreshData();
                    cf.Show();
                }
                catch { }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            e.Graphics.Clear(Color.Black);
            if (!checkBox1.Checked) return;

            Point drawingRect = rightPanel.Location;
            drawingRect.X -= 1;
            drawingRect.Y = this.hScrollBar1.Location.Y + this.hScrollBar1.Size.Height - 1;

            if(Simulation.simulation == null)
                return;
            //Point center;
            if(Simulation.simulation.Map.Width * 128 > drawingRect.X)
            {
                hScrollBar1.Enabled = true;
                drawingRect.Y = this.hScrollBar1.Location.Y - 1;
                magnitudePanel.Visible = true;
            }
            else
                hScrollBar1.Enabled = false;            
            if(Simulation.simulation.Map.Height * 128 > drawingRect.Y)
            {
                vScrollBar1.Enabled = true;
                drawingRect.X = this.vScrollBar1.Location.X - 1;
                magnitudePanel.Visible = true;
            }
            else
                vScrollBar1.Enabled = false;            
            if(Simulation.simulation.Map.Width * 128 > drawingRect.X)
            {
                hScrollBar1.Enabled = true;
                drawingRect.Y = this.hScrollBar1.Location.Y - 1;
                magnitudePanel.Visible = true;
            }
            else
                hScrollBar1.Enabled = false;            
            if((!hScrollBar1.Visible)&&(!vScrollBar1.Visible))
                magnitudePanel.Visible = false;

            float magnitude;
            float mapScreenSizeX = 128 * Simulation.simulation.Map.Width;
            float mapScreenSizeY = 128 * Simulation.simulation.Map.Height;
            mapScreenSizeX /= drawingRect.X;
            mapScreenSizeY /= drawingRect.Y;
            if (mapScreenSizeX > mapScreenSizeY)
            {
                if (mapScreenSizeX > 1)
                {
                    magnitudeBar.Maximum = (int)Math.Ceiling(1000.0f * mapScreenSizeX);
                    magnitudeBar.Minimum = 1000;
                }
            }
            else
            {
                if (mapScreenSizeY > 1)
                {
                    magnitudeBar.Maximum = (int)Math.Ceiling(1000.0f * mapScreenSizeY);
                    magnitudeBar.Minimum = 1000;
                }
            }
            if (magnitudeBar.Maximum < 1000)
                magnitude = 1.0f;
            else
            {
                magnitudeBar.Minimum = 1000;
                magnitude = 1000.0f / magnitudeBar.Value;
            }
            
            int tmp2;
            int tmp21 = (int)Math.Floor((float)drawingRect.X / (magnitude * 128.0f));
            int tmp22 = (int)Math.Floor((float)drawingRect.Y / (magnitude * 128.0f));
            
            if (tmp21 < tmp22)
                tmp2 = tmp21;
            else
                tmp2 = tmp22;
            hScrollBar1.Maximum = AntHillConfig.mapColCount - tmp2;
            vScrollBar1.Maximum = AntHillConfig.mapRowCount - tmp2;
            hScrollBar1.Maximum = (int)((float)hScrollBar1.Maximum * 1.5f + 10);
            vScrollBar1.Maximum = (int)((float)vScrollBar1.Maximum * 1.5f + 10);
            if (hScrollBar1.Maximum < 0)
            {
                hScrollBar1.Maximum = 0;
                hScrollBar1.Minimum = 0;
            }
            if (vScrollBar1.Maximum < 0)
            {
                vScrollBar1.Maximum = 0;
                vScrollBar1.Minimum = 0;
            }
            int currentX = 0, currentY = 0, nextX = (int)(128.0f * magnitude), nextY = (int)(128.0f * magnitude);

            for (int y = vScrollBar1.Value; y < Simulation.simulation.Map.Height; y++)            
            {
                currentX = 0;
                nextX = (int)(128.0f * magnitude);
                for (int x = hScrollBar1.Value; x < Simulation.simulation.Map.Width; x++)
                {
                    e.Graphics.DrawImage(Simulation.simulation.Map.GetTile(x, y).GetBitmap() ,currentX,currentY,nextX - currentX, nextY - currentY);
                    currentX = nextX;
                    if(currentX > drawingRect.X)
                        break;
                    nextX += (int) (128.0f * magnitude);
                }
                currentY = nextY;
                if(currentY > drawingRect.Y)
                    break;
                nextY += (int) (128.0f * magnitude);
            }

            nextX=(int)(128.0f * magnitude);
            //show food
            foreach (Food food in Simulation.simulation.food)
                e.Graphics.DrawImage(AHGraphics.GetFoodBitmap(), (food.Position.X - hScrollBar1.Value) * nextX, (food.Position.Y- vScrollBar1.Value) * nextX, nextX, nextX);

            //show queen
            if (Simulation.simulation.queen != null)
                e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.queen, Simulation.simulation.queen.Direction),
             (Simulation.simulation.queen.Position.X - hScrollBar1.Value) * nextX, (Simulation.simulation.queen.Position.Y - vScrollBar1.Value) * nextX, nextX, nextX); 
 
             //Show ants
              foreach (Ant ant in Simulation.simulation.ants)
                   if(ant is Warrior)
                       e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.warrior, ant.Direction), (ant.Position.X - hScrollBar1.Value) * nextX, (ant.Position.Y - vScrollBar1.Value) * nextX, nextX, nextX);
                   else
                       e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.worker, ant.Direction), (ant.Position.X - hScrollBar1.Value) * nextX, (ant.Position.Y - vScrollBar1.Value) * nextX, nextX, nextX);
                    
             //show spider
             foreach (Spider spider in Simulation.simulation.spiders)
                e.Graphics.DrawImage(AHGraphics.GetCreature(CreatureType.spider, spider.Direction), (spider.Position.X - hScrollBar1.Value) * nextX, (spider.Position.Y - vScrollBar1.Value) * nextX, nextX, nextX);
            //show rain
            if (Simulation.simulation.rain != null)
            {
                e.Graphics.SetClip(new RectangleF(0, 0, Simulation.simulation.Map.Width * nextX, Simulation.simulation.Map.Height * nextX));
                e.Graphics.DrawImage(AHGraphics.GetRainBitmap(), (Simulation.simulation.rain.Position.X - AntHillConfig.rainWidth / 2 - hScrollBar1.Value) * nextX, (Simulation.simulation.rain.Position.Y - AntHillConfig.rainWidth / 2 - vScrollBar1.Value) * nextX, AntHillConfig.rainWidth * nextX, AntHillConfig.rainWidth * nextX);
                e.Graphics.ResetClip();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void magnitudeBar_Scroll(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (((ISimulationUser)Simulation.simulation).DoTurn() == false)
            {
                timer.Stop();
                MessageBox.Show("symulacja skonczona");
            }
            Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = speedBar.Value;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
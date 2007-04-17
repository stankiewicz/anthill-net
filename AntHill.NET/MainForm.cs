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
                stopButton.Enabled = false;
                doTurnButton.Enabled = true;
                pauseButton.Enabled = false;

                Invalidate();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            doTurnButton.Enabled = false;
            pauseButton.Enabled = true;
            ((ISimulationUser)Simulation.simulation).Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
            doTurnButton.Enabled = true;
            pauseButton.Enabled = false;
            ((ISimulationUser)Simulation.simulation).Reset();
        }

        private void doTurnButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            stopButton.Enabled = true;
            doTurnButton.Enabled = true;
            pauseButton.Enabled = false;
            ((ISimulationUser)Simulation.simulation).DoTurn();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            stopButton.Enabled = true;
            doTurnButton.Enabled = true;
            pauseButton.Enabled = false;
            ((ISimulationUser)Simulation.simulation).Pause();
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
                cf.RefreshData();
                cf.Show();
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            Point drawingRect = rightPanel.Location;
            drawingRect.X -= 1;
            drawingRect.Y = this.hScrollBar1.Location.Y + this.hScrollBar1.Size.Height - 1;

            if(Simulation.simulation == null)
                return;
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
            
            int currentX = 0, currentY = 0, nextX = (int)(128.0f * magnitude), nextY = (int)(128.0f * magnitude);

            for (int y = 0; y < Simulation.simulation.Map.Height; y++)            
            {
                currentX = 0;
                nextX = (int)(128.0f * magnitude);
                for (int x = 0; x < Simulation.simulation.Map.Width; x++)
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

            //this HAS to be corrected
            foreach (Ant ant in Simulation.simulation.ants)
                e.Graphics.DrawImage(ant.GetBitmap(), ant.Position);

            foreach(Spider spider in Simulation.simulation.spiders)
                e.Graphics.DrawImage(spider.GetBitmap(), spider.Position);

            if (Simulation.simulation.queen != null)
                e.Graphics.DrawImage(Simulation.simulation.queen.GetBitmap(),
                                     Simulation.simulation.queen.Position);
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
    }
}
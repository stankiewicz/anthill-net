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
                simulationPanel.Enabled = true;
                startButton.Enabled = true;
                stopButton.Enabled = false;
                doTurnButton.Enabled = true;
                pauseButton.Enabled = false;

                Simulation.DeInit();
                Simulation.Init(new Map(AntHillConfig.mapColCount,AntHillConfig.mapRowCount,AntHillConfig.tiles));
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
                hScrollBar1.Visible = true;
                drawingRect.Y = this.hScrollBar1.Location.Y - 1;
                magnitudePanel.Visible = true;
            }
            else            
                hScrollBar1.Visible = false;            
            if(Simulation.simulation.Map.Height * 128 > drawingRect.Y)
            {
                vScrollBar1.Visible = true;
                drawingRect.X = this.vScrollBar1.Location.Y - 1;
                magnitudePanel.Visible = true;
            }
            else            
                hScrollBar1.Visible = false;            
            if(Simulation.simulation.Map.Width * 128 > drawingRect.X)
            {
                hScrollBar1.Visible = true;
                drawingRect.Y = this.hScrollBar1.Location.Y - 1;
                magnitudePanel.Visible = true;
            }
            else            
                hScrollBar1.Visible = false;            
            if((!hScrollBar1.Visible)&&(!vScrollBar1.Visible))
                magnitudePanel.Visible = false;

            float magnitude = 1.0f;
            int currentX = 0, currentY = 0, nextX = 128, nextY = 128;

            for(int x = 0; x<Simulation.simulation.Map.Width; x++)
            {
                for(int y = 0; y<Simulation.simulation.Map.Height; y++)
                {
                    e.Graphics.DrawImage(Simulation.simulation.Map.GetTile(x, y).Image,currentX,currentY,nextX - currentX, nextY - currentY);
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
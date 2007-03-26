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
    }
}
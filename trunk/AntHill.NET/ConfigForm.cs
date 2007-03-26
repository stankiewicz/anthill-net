using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AntHill.NET
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            txtConfig.Text =
                "mapRowCount = " + AntHillConfig.mapRowCount + Environment.NewLine +
                "mapColCount = " + AntHillConfig.mapColCount + Environment.NewLine +
                "antMaxLife = " + AntHillConfig.antMaxLife + Environment.NewLine +
                "antMaxLifeWithoutFood = " + AntHillConfig.antMaxLifeWithoutFood + Environment.NewLine +
                "antTurnNumberToBecomeHungry = " + AntHillConfig.antTurnNumberToBecomeHungry + Environment.NewLine +
                "antMaxHealth = " + AntHillConfig.antMaxHealth + Environment.NewLine +
                "antStrength = " + AntHillConfig.antStrength + Environment.NewLine +
                "antForgettingTime = " + AntHillConfig.antForgettingTime + Environment.NewLine +
                "antSightRadius = " + AntHillConfig.antSightRadius + Environment.NewLine +
                "workerStartCount = " + AntHillConfig.workerStartCount + Environment.NewLine +
                "warriorStartCount = " + AntHillConfig.warriorStartCount + Environment.NewLine +
                "queenLayEggProbability = " + AntHillConfig.queenLayEggProbability + Environment.NewLine +
                "queenXPosition = " + AntHillConfig.queenXPosition + Environment.NewLine +
                "queenYPosition = " + AntHillConfig.queenYPosition + Environment.NewLine +
                "eggHatchTime = " + AntHillConfig.eggHatchTime + Environment.NewLine +
                "eggHatchWarriorProbability = " + AntHillConfig.eggHatchWarriorProbability + Environment.NewLine +
                "spiderMaxHealth = " + AntHillConfig.spiderMaxHealth + Environment.NewLine +
                "spiderFoodQuantityAfterDeath = " + AntHillConfig.spiderFoodQuantityAfterDeath + Environment.NewLine +
                "spiderProbability = " + AntHillConfig.spiderProbability + Environment.NewLine +
                "rainWidth = " + AntHillConfig.rainWidth + Environment.NewLine +
                "rainMaxDuration = " + AntHillConfig.rainMaxDuration + Environment.NewLine +
                "rainProbability = " + AntHillConfig.rainProbability + Environment.NewLine +
                "foodProbability = " + AntHillConfig.foodProbability + Environment.NewLine +
                "messageLifeTime = " + AntHillConfig.messageLifeTime + Environment.NewLine +
                "messageRadius = " + AntHillConfig.messageRadius;
        }
    }
}
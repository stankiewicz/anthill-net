using System;
using System.Collections.Generic;
using System.Text;

namespace AntHill.NET
{
    /// <summary>
    /// This is a configuration class containing simulation parameters.
    /// </summary>
    public static class AntHillConfig
    {
        #region Simulation_Parameters

        //World map
        static public int mapRowCount, mapColCount;

        //Ants
        static public int antMaxLife, antMaxLifeWithoutFood, antTurnNumberToBecomeHungry,
                    antMaxHealth, antStrength, antForgettingTime, antSightRadius,
                    workerStartCount, warriorStartCount;

        //Queen
        static public int queenLayEggProbability, queenXPosition, queenYPosition;

        //Egg
        static public int eggHatchWarriorProbability, eggHatchTime;

        //Spider
        static public int spiderMaxHealth, spiderProbability, spiderFoodQuantityAfterDeath;

        //Rain
        static public int rainWidth, rainProbability, rainMaxDuration;

        //Food
        static public int foodProbability;

        //Message (referred as 'signal' in config file)
        static public int messageLifeTime, messageRadius;

        #endregion 

        #region Simulation_Additional_Parameters

        //If we need to configure/store some settings then do it here

        #endregion
    }
}

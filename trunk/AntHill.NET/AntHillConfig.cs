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
        //World map
        static private int mapRowCount, mapColCount;

        //Ants
        static private int antMaxLife, antMaxLifeWithoutFood, antTurnNumberToBecomeHungry,
                    antMaxHealth, antStrength, antForgettingTime, antSightRadius,
                    workerStartCount, warriorStartCount;

        //Queen
        static private int queenLayEggProbability, queenXPosition, queenYPosition;

        //Egg
        static private int eggHatchWarriorProbability, eggHatchTime;

        //Spider
        static private int spiderMaxHealth, spiderProbability, spiderFoodQuantityAfterDeath;

        //Rain
        static private int rainWidth, rainProbability, rainMaxDuration;

        //Food
        static private int foodProbability;

        //Message (referred as 'signal' in config file)
        static private int messageLifeTime, messageRadius;
    }
}

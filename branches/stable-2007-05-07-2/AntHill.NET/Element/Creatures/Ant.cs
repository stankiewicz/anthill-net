using System;
using System.Drawing;

namespace AntHill.NET
{
    public abstract class Ant : Creature
    {
        private int turnsWithoutFood;
        private int turnsToBecomeHungry;

        public Ant(Point pos):base(pos)
        {
            health = AntHillConfig.antMaxLife;
            turnsToBecomeHungry = AntHillConfig.antTurnNumberToBecomeHungry;
            turnsWithoutFood = AntHillConfig.antMaxLifeWithoutFood;
        }

        public int TurnsWithoutFood
        {
            get { return turnsWithoutFood; }
            set { turnsWithoutFood = value; }
        }

        
        public int TurnsToBecomeHungry
        {
            get
            {
                return turnsToBecomeHungry;
            }
            set
            {
                turnsToBecomeHungry = value;
            }
        }

        public virtual bool IsAlive()
        {
            if (turnsToBecomeHungry > 0)
            {
                turnsToBecomeHungry--;
                return true;
            }

            if (turnsWithoutFood > 0)
            {
                turnsWithoutFood--;
                return true;
            }
            
            return false;
        }

        public virtual void Eat()
        {
            TurnsToBecomeHungry = AntHillConfig.antTurnNumberToBecomeHungry;
            turnsWithoutFood = AntHillConfig.antMaxLifeWithoutFood;
        }
    }
}
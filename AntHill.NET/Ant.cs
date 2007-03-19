using System;
using System.Drawing;

namespace AntHill.NET
{
    public abstract class Ant : Creature
    {
        public Ant(Point pos):base(pos)
        {

        }
        private int turnsWithoutFood;

        public int TurnsWithoutFood
        {
            get { return turnsWithoutFood; }
            set { turnsWithoutFood = value; }
        }

        private int turnsToBecomeHungry;
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

        public virtual void Eat() { }
    }
}
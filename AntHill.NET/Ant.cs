using System;

namespace AntHill.NET
{
    public abstract class Ant : Creature
    {
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
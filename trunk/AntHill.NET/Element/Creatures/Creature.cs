using System;
using System.Drawing;

namespace AntHill.NET
{
    public enum Direction { S, W, E, N }
    public enum State { Alive }

    public abstract class Creature : Element
    {
        public Creature(Point pos):base(pos)
        {

        }
        
        private int life;
        private Direction direction;
        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
    }
}
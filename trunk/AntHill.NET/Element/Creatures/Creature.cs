using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public enum Direction { S, W, E, N }
    public enum State { Alive }
    

    public abstract class Creature : Element
    {
        
        public Creature(Point pos):base(pos)
        {

        }
        public List<KeyValuePair<int, int>> path;
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

        protected bool MoveOrRotate(KeyValuePair<int, int> pos)
        {
            return true;
        }
    }
}
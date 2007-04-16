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
            if (Position.X == pos.Key)
            {
                if (Position.Y == pos.Value + 1) //ant 1 tile above
                {
                    if (Direction == Direction.N)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Direction.N;
                    }
                }
                else
                {
                    if (Direction == Direction.S)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Direction.S;
                    }
                }
            }
            else
            {
                if (Position.X == pos.Key + 1) //ant 1 tile left
                {
                    if (Direction == Direction.W)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Direction.W;
                    }
                }
                else
                {
                    if (Direction == Direction.E)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Direction.E;
                    }
                }

            }
            return false;
        }
    }
}
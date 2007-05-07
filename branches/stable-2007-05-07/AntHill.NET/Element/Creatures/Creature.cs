using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public enum Dir { S, W, E, N }
    public enum CreatureType { queen, warrior, spider, worker }
    public enum AddsType { rain=0,food=1 }
    class DeathException : Exception
    {
    }

    public abstract class Creature : Element
    {
        public List<KeyValuePair<int, int>> path=new List<KeyValuePair<int,int>>();
        protected int health;
        protected Dir direction;

        public int Health
        {
            get { return health; }
            set
            {
                if ((health = value) <= 0)
                    throw new DeathException();
            }
        }

        public Creature(Point pos):base(pos)
        {
            int i = Randomizer.Next(Enum.GetValues(typeof(Dir)).Length);
            direction = (Dir)i;

            path = new List<KeyValuePair<int, int>>();
        }
        
        public Dir Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public virtual Bitmap GetBitmap() { return new Bitmap(1, 1); }

        protected bool MoveOrRotate(KeyValuePair<int, int> pos)
        {
            if (Position.X == pos.Key)
            {
                if (Position.Y == pos.Value + 1) //ant 1 tile above
                {
                    if (Direction == Dir.N)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Dir.N;
                    }
                }
                else
                {
                    if (Direction == Dir.S)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Dir.S;
                    }
                }
            }
            else
            {
                if (Position.X == pos.Key + 1) //ant 1 tile left
                {
                    if (Direction == Dir.W)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Dir.W;
                    }
                }
                else
                {
                    if (Direction == Dir.E)
                    {
                        Move(pos);
                        return true;
                    }
                    else
                    {
                        Direction = Dir.E;
                    }
                }
            }
            return false;
        }
    }
}
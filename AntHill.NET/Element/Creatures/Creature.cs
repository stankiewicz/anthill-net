using System;
using System.Drawing;
using System.Collections.Generic;
using astar;

namespace AntHill.NET
{
    public enum Dir { S = 2, W = 3, E = 1, N = 0 }
    public enum CreatureType { queen, warrior, spider, worker }
    public enum AddsType { rain=0,food=1 }
    class DeathException : Exception
    {
    }

    public abstract class Creature : Element
    {
        public List<KeyValuePair<int, int>> path = new List<KeyValuePair<int,int>>();
        protected int health;
        protected int randomMovemenCount = 0;
        protected Dir direction;
        protected Point randomDestination = new Point(-1, 0);
        public int Health
        {
            get { return health; }
            set
            {
                if ((health = value) <= 0)
                    throw new DeathException();
            }
        }
        List<KeyValuePair<int, int>> currentTrail = new List<KeyValuePair<int,int>>();
        protected void MoveRandomly(ISimulationWorld isw)
        {            
            Point indoorDestination, outdoorDestination;
            randomMovemenCount++;
            try
            {
                if ((this.Position.X == currentTrail[randomMovemenCount].Key) && (this.Position.Y == currentTrail[randomMovemenCount].Value))
                    randomMovemenCount++;
            }
            catch { }

            if ((randomMovemenCount >= currentTrail.Count) || (randomDestination.X < 0))
            {
                randomMovemenCount = 0;
                indoorDestination = isw.GetMap().GetRandomTile(TileType.Indoor).Position;
                outdoorDestination = isw.GetMap().GetRandomTile(TileType.Outdoor).Position;
                if (Randomizer.Next(2) == 0)
                    randomDestination = outdoorDestination;
                else
                    randomDestination = indoorDestination;
                currentTrail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(randomDestination.X, randomDestination.Y), new AstarOtherObject());
            }            
            if (currentTrail == null)
            {
                randomDestination.X = -1;
                return;
            }
            if (currentTrail.Count <= 1)
            {
                randomDestination.X = -1;
                return;
            }
            if (!MoveOrRotate(currentTrail[randomMovemenCount]))
                randomMovemenCount--;
            if (randomMovemenCount >= 10)            
                randomDestination.X = -1;            
        }
        public void MoveRotateOrAttack(Creature aggresor, Creature prey, ISimulationWorld isw)
        {
            int distance = Distance(aggresor.Position, prey.Position);
            if (distance == 0)
            {
                isw.Attack(aggresor, prey);
            }
            if (distance == 1)
            {
                if (aggresor.Position.X == prey.Position.X)
                {
                    if (aggresor.Position.Y == prey.Position.Y + 1) //ant 1 tile above
                    {
                        if (aggresor.Direction == Dir.N)
                        {
                            isw.Attack(aggresor, prey);
                            return;
                        }
                        else
                        {
                            aggresor.Direction = Dir.N;
                        }
                    }
                    else
                    {
                        if (aggresor.Direction == Dir.S)
                        {
                            isw.Attack(aggresor, prey);
                            return;
                        }
                        else
                        {
                            aggresor.Direction = Dir.S;
                        }
                    }
                }
                else
                {
                    if (aggresor.Position.X == prey.Position.X + 1) //ant 1 tile left
                    {
                        if (aggresor.Direction == Dir.W)
                        {
                            isw.Attack(aggresor, prey);
                            return;
                        }
                        else
                        {
                            aggresor.Direction = Dir.W;
                        }
                    }
                    else
                    {
                        if (aggresor.Direction == Dir.E)
                        {
                            isw.Attack(aggresor, prey);
                            return;
                        }
                        else
                        {
                            aggresor.Direction = Dir.E;
                        }
                    }

                }
                return;
            }
            if (distance > 1)
            {

                List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(aggresor.Position.X, aggresor.Position.Y), new KeyValuePair<int, int>(prey.Position.X, prey.Position.Y), new AstarOtherObject());
                if (trail == null)
                    return;
                if (trail.Count <= 1)
                    return;
                MoveOrRotate(trail[1]);                
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
        
        protected bool IsMoveOrRotate(KeyValuePair<int, int> pos)
        {
            if (Position.X == pos.Key)
            {
                if (Position.Y == pos.Value + 1) //ant 1 tile above
                {
                    if (Direction == Dir.N)
                    {
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
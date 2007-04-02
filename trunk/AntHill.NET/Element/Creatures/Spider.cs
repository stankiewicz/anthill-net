using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Spider : Creature
    {
        //bool queenSpotted = false;// searching, 1 moving to ant, 2 fighting, 10 attack queen        
        public Spider(Point pos):base(pos) {}
//        List<KeyValuePair<int><int>>
        private int Distance(Point p1, Point p2)
        {          
            int x = Math.Abs(p1.X - p2.X);
            int y = Math.Abs(p1.Y - p2.Y);
            if (x > y)
                return x;
            return y;
        }
        private Ant FindNearestAnt()
        {            
           // if(queenSpotted)
             //   return Simulation.simulation.queen;
            if (AntHillConfig.antSightRadius <= Distance(Simulation.simulation.queen.Position, this.Position))
            {                                
                //queenSpotted = true;
                return Simulation.simulation.queen;
            }
            
            List<Ant> ants = Simulation.simulation.ants;
            if (ants.Count == 0)
                return null;
            int minDistance = Distance(ants[0].Position, Position);

            int index = 0;
            int distance, bestIndex = 0, bestDistance = int.MaxValue;
            foreach (Ant ant in ants)
            {
                if ((distance = Distance(this.Position, ant.Position)) < minDistance)
                {
                    bestIndex = index;
                    bestDistance = distance;
                }
                index++;
            }
            if (bestDistance <= AntHillConfig.antSightRadius)
                return ants[bestIndex];
            return null;
        }

        private void Move(KeyValuePair<int, int> position)
        {
            this.Position = new Point(position.Key, position.Value);
        }
        public override void Maintain(ISimulationWorld isw)
        {
            Ant ant = FindNearestAnt();
            if (ant == null)
            {
                Random random = new Random();
                Point destination = new Point(random.Next(AntHillConfig.mapColCount - 1),random.Next(AntHillConfig.mapRowCount - 1));
                astar.Astar astar = new astar.Astar(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);
                List<KeyValuePair<int, int>> trail = astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(destination.X, destination.Y),new AstarOtherObject());
                if (trail == null)
                    return;
                if (trail.Count <= 1)
                    return;
                Move(trail[1]);                
                return;
            }

            int distance = Distance(ant.Position, this.Position);
            if (distance == 0)
            {
                isw.Attack(this, ant);
                return;
            }
            if (distance == 1)
            {
                if (Position.X == ant.Position.X)
                {
                    if (Position.Y == ant.Position.Y + 1) //ant 1 tile above
                    {
                        if (Direction == Direction.N)
                        {
                            isw.Attack(this, ant);
            
                            return;
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
                            isw.Attack(this, ant);
                            return;
                        }
                        else
                        {
                            Direction = Direction.S;
                        }
                    }
                }
                else
                {
                    if (Position.X == ant.Position.X + 1) //ant 1 tile left
                    {
                        if (Direction == Direction.W)
                        {
                            isw.Attack(this, ant);
                            return;
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
                            isw.Attack(this, ant);
                            return;
                        }
                        else
                        {
                            Direction = Direction.E;
                        }
                    }
                }
                return;
            }
            if (distance > 1)
            {
                astar.Astar astar = new astar.Astar(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);
                List<KeyValuePair<int, int>> trail = astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(ant.Position.X, ant.Position.Y), new AstarOtherObject());
                if (trail == null)
                    return;
                if (trail.Count <= 1)
                    return;
                Move(trail[1]);
                return;
            }

            //throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
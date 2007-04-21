using System;
using System.Drawing;
using System.Collections.Generic;
using astar;

namespace AntHill.NET
{
    public class Spider : Creature
    {
        //bool queenSpotted = false;// searching, 1 moving to ant, 2 fighting, 10 attack queen        
        public Spider(Point pos):base(pos) {
        }
//        List<KeyValuePair<int><int>>
        
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

      
        public override bool Maintain(ISimulationWorld isw)
        {//TODO czy na pewno dobrze znajduje droge? (pobieranie astar) czy spider nie szuka krolowej?
            Ant ant = FindNearestAnt();
            if (ant == null)
            {
                Point destination = new Point(Randomizer.Next(AntHillConfig.mapColCount), Randomizer.Next(AntHillConfig.mapRowCount));
                List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(destination.X, destination.Y),new AstarOtherObject());
                if (trail == null)
                    return true;
                if (trail.Count <= 1)
                    return true;
                Move(trail[1]);                
                return true;
            }

            int distance = Distance(ant.Position, this.Position);
            if (distance == 0)
            {
                isw.Attack(this, ant);
                return true; 
            }
            if (distance == 1)
            {
                if (Position.X == ant.Position.X)
                {
                    if (Position.Y == ant.Position.Y + 1) //ant 1 tile above
                    {
                        if (Direction == Dir.N)
                        {
                            isw.Attack(this, ant);
            
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
                            isw.Attack(this, ant);
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
                    if (Position.X == ant.Position.X + 1) //ant 1 tile left
                    {
                        if (Direction == Dir.W)
                        {
                            isw.Attack(this, ant);
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
                            isw.Attack(this, ant);
                            return true; 
                        }
                        else
                        {
                            Direction = Dir.E;
                        }
                    }
                }
                return true;
            }
            if (distance > 1)
            {
                
                List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(ant.Position.X, ant.Position.Y), new AstarOtherObject());
                if (trail == null)
                    return true;
                if (trail.Count <= 1)
                    return true;
                Move(trail[1]);
                return true;
            }

            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.spider, this.Direction);
        }
    }
}
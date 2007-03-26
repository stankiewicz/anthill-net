using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Spider : Creature
    {
        bool queenSpotted = false;// searching, 1 moving to ant, 2 fighting, 10 attack queen        
        public Spider(Point pos):base(pos) {}
//        List<KeyValuePair<int><int>>
        private int Distance(Point p1, Point p2)
        {
            if (target.X == -1)
                return -1;
            int x = Math.Abs(p1.X - p2.X);
            int y = Math.Abs(p1.Y - p2.Y);
            if (x > y)
                return x;
            return y;
        }
        private Ant FindNearestAnt()
        {
            if(queenSpotted)
                return Simulation.simulation.queen;
            if (AntHillConfig.antSightRadius <= Distance(Simulation.simulation.queen.Position, this.Position))
            {                                
                queenSpotted = true;
                return Simulation.simulation.queen;
            }
            
            List<Ant> ants = Simulation.simulation.ants;
            if (ants.Count == 0)
                return null;
            int minDistance = Distance(ants[0].Position, Position);
            int index = 0, distance, bestIndex, bestDistance;
           
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
        public override void Maintain(ISimulationWorld isw)
        {
            Ant ant = FindNearestAnt();
            if (ant == null)
            {
                //move to random place
                return;
            }

            int distance = Distance(ant.Position, this.Position);
            if (distance == 0)
            {                
                // kill the bitch
                return;
            }
            if (distance == 1)
            {
                //rotate
                // kill the bitch
                return;
            }
            if (distance > 1)
            {
                //move
            }

            //throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
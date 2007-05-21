using System;
using System.Drawing;
using System.Collections.Generic;
using astar;

namespace AntHill.NET
{
    public class Spider : Creature
    {        
        //bool queenSpotted = false;// searching, 1 moving to ant, 2 fighting, 10 attack queen        
        public Spider(Point pos):base(pos)
        {
            this.health = AntHillConfig.spiderMaxHealth;
        }
//        List<KeyValuePair<int><int>>
        
        private Ant FindNearestAnt()
        {
            if (Simulation.simulation.queen == null)
                return null;
            if (AntHillConfig.antSightRadius >= Distance(Simulation.simulation.queen.Position, this.Position))                                          
                return Simulation.simulation.queen;
            
            List<Ant> ants = Simulation.simulation.GetVisibleAnts(this);
            if (ants == null)
                return null;
            if (ants.Count == 0)
                return null;
            int minDistance = Distance(ants[0].Position, Position);

            int index = 0;
            int distance, bestIndex = 0;
            foreach (Ant ant in ants)
            {
                if ((distance = Distance(this.Position, ant.Position)) < minDistance)
                {
                    bestIndex = index;
                    minDistance = distance;
                }
                index++;
            }
            if (minDistance <= AntHillConfig.antSightRadius)
                return ants[bestIndex];
            return null;
        }

      
        public override bool Maintain(ISimulationWorld isw)
        {//TODO czy na pewno dobrze znajduje droge? (pobieranie astar) czy spider nie szuka krolowej?
            Ant ant = FindNearestAnt();
            if (ant == null)
            {
                MoveRandomly(isw);
                return true;
            }
            randomDestination.X = -1;
            MoveRotateOrAttack(this, ant, isw);
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

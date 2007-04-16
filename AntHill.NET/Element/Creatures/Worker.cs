using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Worker : Citizen
    {
        private int foodQuantity;

        public int FoodQuantity
        {
            get
            {
                return foodQuantity;
            }
            set
            {
                foodQuantity = value;
            }
        }
        public Worker(Point pos):base(pos)
        {

        }
        public void Dig()
        {//TODO gdzie kopie

        }

        public void LoadFood()
        {//TODO skad zabiera?

        }

        public void UnloadFood()
        {//TODO gdzie rozladowuje

        }

        public override void Maintain(ISimulationWorld isw)
        {//TODO sciezka moze nulla zwrocic
            if (base.IsAlive())
            {
                if (this.TurnsToBecomeHungry == 0)
                    if (this.foodQuantity > 0)
                    {
                        if (path.Count == 0)
                        {
                            astar.Astar astar = new astar.Astar(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);
                            base.path =
                            astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y),
                            new KeyValuePair<int, int>(150, 150), new AstarOtherObject());
                            base.path.RemoveAt(0);
                        }
                        if (path.Count > this.TurnsWithoutFood)
                        {
                            this.Eat();
                            this.foodQuantity--;
                        }
                    }
                if (path.Count > 0)
                {
                    Move(path[0]);
                    path.RemoveAt(0);
                }
            }
            else
                isw.DeleteAnt(this);
           
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
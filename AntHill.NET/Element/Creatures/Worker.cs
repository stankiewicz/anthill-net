using System;
using System.Drawing;
using System.Collections.Generic;
using astar;

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
        public Worker(Point pos):base(pos) {}

        public void Dig(ISimulationWorld isw)
        {//TODO gdzie kopie

        }

        public void LoadFood(ISimulationWorld isw)
        {//TODO skad zabiera?

        }

        public void UnloadFood(ISimulationWorld isw)
        {//TODO gdzie rozladowuje

        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (base.IsAlive())
            {
                SpreadSignal(isw);
                if (this.TurnsToBecomeHungry == 0)
                    if (this.foodQuantity > 0)
                    {
                        if (path.Count == 0)
                        {
                            base.path =
                            Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y),
                            new KeyValuePair<int, int>(150, 150), new AstarOtherObject());
                            // pobranie z visible food albo  jesli nie ma to z sygnalu..
                            base.path.RemoveAt(0);
                        }
                        if (path.Count > this.TurnsWithoutFood)
                        {
                            foodQuantity--;
                            this.Eat();
                        }
                    }
                if (path.Count > 0)
                {
                    if (MoveOrRotate(path[1]))
                        path.RemoveAt(0);
                }
            }
            else
            {
                
                return false;
            }
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.worker, this.Direction);
        }

    }
}
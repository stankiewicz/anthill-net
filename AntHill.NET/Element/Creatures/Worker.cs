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

        public void Dig()
        {//TODO gdzie kopie

        }

        public void LoadFood()
        {//TODO skad zabiera?

        }

        public void UnloadFood()
        {//TODO gdzie rozladowuje

        }

        public override bool Maintain(ISimulationWorld isw)
        {//TODO sciezka moze nulla zwrocic
            //i zwraca - Kamil :-P
            if (base.IsAlive())
            {
                if (this.TurnsToBecomeHungry == 0)
                    if (this.foodQuantity > 0)
                    {
                        if (path.Count == 0)
                        {
                            base.path =
                            Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y),
                            new KeyValuePair<int, int>(150, 150), new AstarOtherObject());
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
                    if(MoveOrRotate(path[1]))
                        path.RemoveAt(0);
                }
            }
            else
                isw.DeleteAnt(this);
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
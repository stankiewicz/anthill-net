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
            get { return foodQuantity; }
            set { foodQuantity = value; }
        }

        public Worker(Point pos):base(pos) {}

        public void Dig(ISimulationWorld isw, Point pos)
        {
            isw.GetMap().DestroyWall(isw.GetMap().GetTile(pos.X, pos.Y));
        }

        public void LoadFood(ISimulationWorld isw, Food f)
        {
            isw.DeleteFood(f);
            foodQuantity += f.GetQuantity;
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (!base.IsAlive()) return false;
            
            SpreadSignal(isw);
            List<Message> msg = isw.GetVisibleMessages(this);
            for(int i=0; i<msg.Count; i++)
                this.AddToSet(msg[i], msg[i].GetPoint(this.Position).Intensity);

            if (this.TurnsToBecomeHungry == 0)
                if (this.foodQuantity > 0)
                {

                }
            if (path.Count > 0)
            {
                if (MoveOrRotate(path[1]))
                    path.RemoveAt(0);
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

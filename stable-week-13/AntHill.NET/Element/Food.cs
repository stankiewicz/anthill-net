using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Food : Element
    {
        private int quantity;

        public Food(Point pos, int quantity) : base(pos)
        {
            this.quantity = quantity;
        }

        public int GetQuantity
        {
            get { return quantity; }
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            quantity--;
            if (quantity <= 0)
            {
                isw.DeleteFood(this);
                return false;
            }
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
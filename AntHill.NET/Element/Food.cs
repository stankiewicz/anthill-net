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

        public override void Maintain(ISimulationWorld isw)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy(ISimulationWorld isw)
        {
            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
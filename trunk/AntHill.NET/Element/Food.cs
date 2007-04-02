using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Food : Element
    {
        private int quantity;

        public Food(Point pos):base(pos)
        {

        }
        virtual public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public override void Maintain(ISimulationWorld isw)
        {
            //throw new Exception("The method or operation is not implemented.");

        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
using System;

namespace AntHill.NET
{
    public class Food : Element
    {
        private int quantity;

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
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
using System;
using System.Drawing;

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
        {

        }

        public void LoadFood()
        {

        }

        public void UnloadFood()
        {

        }

        public override void Maintain(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
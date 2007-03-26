using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Spider : Creature
    {
        public Spider(Point pos):base(pos)
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
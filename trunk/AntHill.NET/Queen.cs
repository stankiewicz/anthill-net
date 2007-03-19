using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Queen : Ant
    {
        public Queen(Point pos):base(pos)
        {

        }
        public void LayEgg()
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AntHill.NET
{
    class Warrior: Citizen
    {
        public Warrior(Point pos):base(pos)
        {

        }
        public override void Maintain(ISimulationWorld isw)
        {//TODO malo:)
            if (!base.IsAlive())
                isw.DeleteAnt(this);
        }

        

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

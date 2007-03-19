using System;
using System.Collections.Generic;
using System.Text;

namespace AntHill.NET
{
    class Warrior: Citizen
    {
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

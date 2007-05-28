using System;
using System.Drawing;

namespace AntHill.NET
{

    public class Egg : Element
    {
        private int timeToHatch;	
        
        public Egg(Point pos):base(pos)
        {
            timeToHatch = AntHillConfig.eggHatchTime;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isw"></param>
        public override bool Maintain(ISimulationWorld isw)
        {
            if (--timeToHatch < 0)
            {
                isw.CreateAnt(this.Position);
                return false;
            }
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            isw.DeleteEgg(this);
        }
    }
}
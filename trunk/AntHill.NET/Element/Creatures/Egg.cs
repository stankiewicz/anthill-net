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
        public override void Maintain(ISimulationWorld isw)
        {
            if (--timeToHatch == 0)
            {
                isw.CreateAnt(this.Position);
                isw.DeleteEgg(this);
            }
        }

        public override void Destroy(ISimulationWorld isw)
        {
            isw.DeleteEgg(this);
        }
    }
}
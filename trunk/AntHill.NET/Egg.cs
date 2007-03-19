using System;

namespace AntHill.NET
{

    public class Egg : Element
    {
        private int timeToHatch;

        public override void Maintain(ISimulationWorld isw)
        {
            if (--timeToHatch == 0)
            {
                //create ant
                //destroy egg

                isw.CreateAnt(this.Position);
                isw.DeleteEgg(this);

            }
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
using System;

namespace AntHill.NET
{

    public class Egg : Element
    {
        private int timeToHatch;

        public override void Maintain()
        {
            if (--timeToHatch == 0)
            {
                //create ant
                //destroy egg
            }
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
using System;

namespace AntHill.NET
{
    public class Spider : Creature, IFightableCreature
    {
        public override void Maintain()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region IFightableCreature Members

        void IFightableCreature.Attack()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
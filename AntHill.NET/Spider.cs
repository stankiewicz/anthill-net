using System;
public class Spider: Creature, IFightableCreature, IMovableCreature
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

    #region IMovableCreature Members

    void IMovableCreature.Move()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #endregion
}
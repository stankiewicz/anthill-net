using System;


public class Rain : Element
{
	public virtual void  Raining()
	{

	}

    public override void Maintain()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void Destroy()
    {
        throw new Exception("The method or operation is not implemented.");
    }
}
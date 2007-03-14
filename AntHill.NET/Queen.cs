using System;

public class Queen : Ant
{
	private int layEggProbability;
	
	public virtual void  LayEgg()
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
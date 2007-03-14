using System;

public abstract class Citizen : Ant, IMovableCreature
{
	public Citizen()
	{
	}

    public virtual void  SpreadSignal()
	{
	}
	
	public virtual void  ReadMessage()
	{
	}
	
	public virtual void  Move()
	{
	}
}
using System;
public class Spider: Creature, IFightableCreature, IMovableCreature
{
	virtual public int Life
	{
		get
		{

			return life;

		}
		
		set
		{

			life = value;

		}
		
	}
	virtual public int ProbabilityOfCreation
	{
		get
		{

			return probabilityOfCreation;

		}
		
		set
		{

			probabilityOfCreation = value;

		}
		
	}
	virtual public int FoodAfterDeath
	{
		get
		{

			return foodAfterDeath;

		}
		
		set
		{

			foodAfterDeath = value;

		}
		
	}
	private int life;
	
	private int probabilityOfCreation;
	
	private int foodAfterDeath;
	
	public virtual void  Attack()
	{
		
	}
	
	public virtual void  Move()
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
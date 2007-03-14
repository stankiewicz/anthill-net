using System;

public abstract class Ant : Creature
{
	public virtual int MaxLifeWithoutFood
	{
		get
		{
			return maxLifeWithoutFood;
		}
		
		set
		{
			maxLifeWithoutFood = value;
		}
		
	}

    virtual public int TurnNumberToBecomeHungry
	{
		get
		{
			return turnNumberToBecomeHungry;
		}
		
		set
		{
			turnNumberToBecomeHungry = value;
		}
		
	}

    private int maxLifeWithoutFood;
	private int turnNumberToBecomeHungry;
	private int LifeLeft;

    public virtual int getLifeLeft()
	{
		return LifeLeft;
	}
	
	public virtual void  setLifeLeft(int theLifeLeft)
	{
		LifeLeft = theLifeLeft;
	}
	
	public virtual void  Eat()
	{
	}
}
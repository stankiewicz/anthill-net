using System;

public abstract class Ant : Creature
{

    public int TurnNumberToBecomeHungry
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

	private int turnNumberToBecomeHungry;

    public virtual void Eat() { }
}
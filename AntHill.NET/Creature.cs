using System;

public abstract class Creature : Element
{
    private int life;

	virtual public int FieldOfView
	{
		get
		{
			return fieldOfView;
		}
		
		set
		{
			fieldOfView = value;
		}
		
	}

    virtual public int Direction
	{
		get
		{
			return direction;
		}
		
		set
		{
			direction = value;
		}
		
	}
	
    virtual public State[] State
	{
		get
		{
			return state;
		}
		
		set
		{
			state = value;
		}
		
	}
	private int fieldOfView;
	private int direction;
	private State[] state = new State[3];
	
}
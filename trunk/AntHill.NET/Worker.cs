using System;

public class Worker : Citizen
{
    private int foodQuantity;
	public int FoodQuantity
	{
		get
		{
			return foodQuantity;
		}		
		set
		{
			foodQuantity = value;
		}		
	}
	
	
	public void  Dig()
	{
		
	}
	
	public void  LoodFood()
	{
		
	}
	
	public void  UnloadFood()
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
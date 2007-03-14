using System;

public class Food : Element
{
    private int quantity;

	virtual public int Quantity
	{
		get
		{
			return quantity;
		}
		
		set
		{
			quantity = value;
		}
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
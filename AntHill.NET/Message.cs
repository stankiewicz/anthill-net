using System;
using System.Collections.Generic;

public enum MessageType
{
    QueenIsHungry,
    QueenInDanger,
    FoodLocalization,
    SpiderLocalization
}

public class Message : Element
{
    private MessageType type;

	public Message()
	{
	}

	public MessageType GetMessageType
	{
		get
		{
			return type;
		}
	}
	
	public List<PointWithIntensity> points;	
    public void AddPoint(List<PointWithIntensity> newPoint) { }
	
    public override void Maintain()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void Destroy()
    {
        throw new Exception("The method or operation is not implemented.");
    }
}
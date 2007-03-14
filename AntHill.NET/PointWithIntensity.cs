using System;
using System.Drawing;

public class PointWithIntensity
{
    private Point position;
    public Point Position
    {
        get { return position; }
        set { position = value; }
    }

    private int intensity;
    public int Intensity
    {
        get { return intensity; }
        set { intensity = value; }
    }


	public PointWithIntensity()
	{
	}

	private System.Collections.Generic.List < Message > message;
	
	public System.Collections.Generic.List < Message > getMessage(){return null;}

    public void setMessage(System.Collections.Generic.List<Message> theMessage) { }
	
	public virtual void  CountDown()
	{
		
	}
}
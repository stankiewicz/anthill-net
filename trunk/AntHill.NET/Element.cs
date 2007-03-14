using System;
using System.Drawing;
using System.Collections.Generic;

public abstract class Element
{
    private Point position;
	public Point Position
	{
		get
        {
			return position;
		}
		set
        {
			position = value;
		}
	}

    public abstract void Maintain();
	public abstract void Destroy();
}
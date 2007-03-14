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
}
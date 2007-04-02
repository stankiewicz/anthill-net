using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public abstract class Element
    {
        public Element(Point pos)
        {
            position = pos;
        }
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
        public int Distance(Point p1, Point p2)
        {
            int x = Math.Abs(p1.X - p2.X);
            int y = Math.Abs(p1.Y - p2.Y);
            if (x > y)
                return x;
            return y;
        }
        public abstract void Maintain(ISimulationWorld isw);
        public abstract void Destroy(ISimulationWorld isw);
    }
}
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

        public abstract void Maintain(ISimulationWorld isw);
        public abstract void Destroy(ISimulationWorld isw);
    }
}
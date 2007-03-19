using System;
using System.Drawing;

namespace AntHill.NET
{

    public abstract class Citizen : Ant
    {

        public Citizen(Point pos):base(pos)
        {
        }

        public virtual void SpreadSignal()
        {
        }

        public virtual void ReadMessage()
        {
        }
    }
}
using System;

namespace AntHill.NET
{

    public abstract class Citizen : Ant
    {

        public Citizen()
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
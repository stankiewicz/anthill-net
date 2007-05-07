using System;

namespace AntHill.NET
{
    public interface ISimulationUser
    {
        bool DoTurn();
        void Reset();
        void Start();
        void Stop();
    }
}
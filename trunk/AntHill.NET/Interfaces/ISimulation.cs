using System;

namespace AntHill.NET
{
    public interface ISimulationUser
    {
        void DoTurn();
        void Reset();
        void Start();
        void Stop();
    }
}
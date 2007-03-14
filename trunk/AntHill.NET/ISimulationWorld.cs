using System;
using System.Collections.Generic;
using System.Text;

namespace AntHill.NET
{
    /// <summary>
    /// This is a subject to change - particularly functions' arguments
    /// </summary>
    interface ISimulationWorld
    {
        void CreateAnt();
        void CreateSpider();
        void CreateFood();
        void CreateMessage();
        void Attack(Creature cA, Creature cB);
    }
}

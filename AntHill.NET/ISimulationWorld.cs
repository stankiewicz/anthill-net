using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AntHill.NET
{
    /// <summary>
    /// This interface will be send to objects in Maintain() function,
    /// this is how creatures will interact with environment.
    /// This is a subject to change - particularly functions' arguments
    /// </summary>
    public interface ISimulationWorld
    {
        void CreateAnt(Point position);
        void CreateSpider();    //This might be unnecessary because only Simulation creates Spiders
        void CreateFood();      //This might be used in Ants' Destroy() function, or at Simulation level -  after creature's death
        void CreateMessage();
        void Attack(Creature cA, Creature cB);

        //'Creature c' is a creature that wants to get some info
        List<Ant> GetVisibleAnts(Creature c);
        List<Food> GetVisibleFood(Creature c);
        List<Spider> GetVisibleSpiders(Creature c);
        List<Message> GetVisibleMessages(Creature c);

        void DeleteEgg(Egg egg);
    }
}

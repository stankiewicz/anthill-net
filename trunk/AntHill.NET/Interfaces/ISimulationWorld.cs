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
        void CreateWarrior(Point position);
        void CreateWorker(Point position);
        void CreateSpider(Point position);    //This might be unnecessary because only Simulation creates Spiders
        void CreateFood(Point position, int quantity);      //This might be used in Ants' Destroy() function, or at Simulation level -  after creature's death
        void CreateMessage(Point pos, MessageType mt);
        void CreateEgg(Point pos);
        void Attack(Creature cA, Creature cB);

        //'Creature c' is a creature that wants to get some info
        List<Ant> GetVisibleAnts(Element c);
        List<Food> GetVisibleFood(Element c);
        List<Spider> GetVisibleSpiders(Element c);
        List<Message> GetVisibleMessages(Element c);

        void DeleteEgg(Egg egg);
        void DeleteRain();
        void DeleteFood(Food food);
        void DeleteAnt(Ant ant);
        void DeleteSpider(Spider spider);
        Map GetMap();
    }
}

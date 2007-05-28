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
        bool CreateAnt(Point position);
        bool CreateWarrior(Point position);
        bool CreateWorker(Point position);
        bool CreateSpider(Point position);    //This might be unnecessary because only Simulation creates Spiders
        bool CreateFood(Point position, int quantity);      //This might be used in Ants' Destroy() function, or at Simulation level -  after creature's death
        bool CreateMessage(Point pos, MessageType mt, Point location);
        bool CreateEgg(Point pos);
        bool Attack(Creature cA, Creature cB);
        bool FeedQueen(Worker w);

        //'Creature c' is a creature that wants to get some info
        LIList<Ant> GetVisibleAnts(Element c);
        LIList<Food> GetVisibleFood(Element c);
        LIList<Spider> GetVisibleSpiders(Element c);
        LIList<Message> GetVisibleMessages(Element c);

        bool DeleteEgg(Egg egg);
        bool DeleteRain();
        bool DeleteFood(Food food);
        bool DeleteAnt(Ant ant);
        bool DeleteSpider(Spider spider);
        Map GetMap();
    }
}

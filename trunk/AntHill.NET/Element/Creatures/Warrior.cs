using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using astar;

namespace AntHill.NET
{
    class Warrior: Citizen
    {
        public Warrior(Point pos):base(pos)
        {
            
        }

        private bool MaintainSignals(MessageType mT)
        {
            Message m = ReadMessage(mT);
            if (m != null)
            {
                if (Distance(this.Position, m.Location) >= 0)
                {
                    List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(m.Location.X, m.Location.Y), new AstarOtherObject());
                    if (trail == null)
                        return true;
                    if (trail.Count <= 1)
                        return true;
                    MoveOrRotate(trail[1]);
                    return true;
                }
            }
            return false;
        }
        private Spider lastSpider = null;
        private Food lastFood = null;
        public override bool Maintain(ISimulationWorld isw)
        {//TODO malo:)
            if (!base.IsAlive())
            {                
                return false;
            }
            SpreadSignal(isw);
            LIList<Message>.Enumerator msg = isw.GetVisibleMessages(this).GetEnumerator();
            while(msg.MoveNext())
                this.AddToSet(msg.Current, msg.Current.GetPoint(this.Position).Intensity);

            LIList<Spider> spiders;
            if ((spiders = isw.GetVisibleSpiders(this)).Count != 0)
            {
                Spider spider = GetNearestSpider(spiders);
                if (spider != lastSpider)
                {
                    if (!FindEqualSignal(MessageType.SpiderLocalization, spider.Position))
                    {
                        isw.CreateMessage(this.Position, MessageType.SpiderLocalization, spider.Position);
                        lastSpider = spider;
                    }
                }
                MoveRotateOrAttack(this, spider, isw);
                randomDestination.X = -1;
                return true;
            }
            if (MaintainSignals(MessageType.QueenInDanger))
            {
                randomDestination.X = -1;
                return true;
            }
            if (MaintainSignals(MessageType.SpiderLocalization))
            {
                randomDestination.X = -1;
                return true;
            }

            // teraz wcinamy

            LIList<Food> foods = isw.GetVisibleFood(this);
            if (foods.Count != 0)
            {
                Food food = GetNearestFood(foods);
                int distance = Distance(this.Position, food.Position);
                if (food != lastFood)
                {
                    if (!FindEqualSignal(MessageType.FoodLocalization, food.Position))
                    {
                        isw.CreateMessage(this.Position, MessageType.FoodLocalization, food.Position);
                        lastFood = food;
                    }
                }
                if (this.TurnsToBecomeHungry <= 0)
                {
                    if (distance == 0)
                    {
                        food.Maintain(isw);
                        this.Eat();


                        randomDestination.X = -1;
                        return true;
                    }
                    List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(food.Position.X, food.Position.Y), new AstarOtherObject());
                    if (trail == null)
                    {
                        randomDestination.X = -1;
                        return true;
                    }
                    if (trail.Count <= 1)
                    {
                        randomDestination.X = -1;
                        return true;
                    }
                    MoveOrRotate(trail[1]);
                    randomDestination.X = -1;
                    return true;
                }
            }
            else
            {
                if (MaintainSignals(MessageType.FoodLocalization))
                {
                    randomDestination.X = -1;
                    return true;
                }
            }

            MoveRandomly(isw);
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

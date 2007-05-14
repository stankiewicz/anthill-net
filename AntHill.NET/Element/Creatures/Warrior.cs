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

        Spider GetNearestSpider(List<Spider> spiders)
        {
            int i=0;
            int min=Int32.MaxValue;
            int tmp;
            for (int j = 0; j < spiders.Count; j++)
			{
                if ((tmp = Distance(this.Position, spiders[i].Position)) < min)
                {
                    i = j;
                    min = tmp;
                }
			}
            return spiders[i];
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
        
        public override bool Maintain(ISimulationWorld isw)
        {//TODO malo:)
            if (!base.IsAlive())
            {
                isw.DeleteAnt(this);
                return false;
            }
            SpreadSignal(isw);
            List<Message> msg = isw.GetVisibleMessages(this);
            for (int i = 0; i < msg.Count; i++)
                this.AddToSet(msg[i], msg[i].GetPoint(this.Position).Intensity);

            List<Spider> spiders;
            if ((spiders=isw.GetVisibleSpiders(this)).Count!=0)
            {                
                Spider spider = GetNearestSpider(spiders);
                isw.CreateMessage(this.Position, MessageType.SpiderLocalization, spider.Position);
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
            if (this.TurnsToBecomeHungry <= 0)
            {
                List<Food> foods = isw.GetVisibleFood(this);
                if (foods.Count != 0)
                {
                    Food food = GetNearestFood(foods);
                    isw.CreateMessage(this.Position, MessageType.FoodLocalization, food.Position);
                    int distance = Distance(this.Position, food.Position);
                    if (distance == 0)
                    {
                        food.Maintain(isw);                        
                        this.Eat();
                        if(food.GetQuantity > 0)
                            isw.CreateMessage(this.Position, MessageType.FoodLocalization, food.Position);
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
                else
                {
                    if (MaintainSignals(MessageType.FoodLocalization))
                    {
                        randomDestination.X = -1;
                        return true;
                    }
                }
            }
            MoveRandomly(isw);
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.warrior, this.Direction);
        }
    }
}

using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Rain : Element
    {
        Point rainPos;
        private int timeToLive;

        public int TimeToLive
        {
            get { return timeToLive; }
            set { timeToLive = value; }
        }


        public Rain(Point pos):base(pos)
        {
            rainPos = pos;
            timeToLive = Randomizer.Next(AntHillConfig.rainMaxDuration + 1);
        }

        public bool IsRainOver(int x, int y)
        {
            if ((x - Position.X) < AntHillConfig.rainWidth &&
                (x - Position.X) >= 0 &&
                (y - Position.Y) < AntHillConfig.rainWidth &&
                (y - Position.Y) >= 0)
                return true;
            return false;
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (--timeToLive < 0)
            {
                isw.DeleteRain();
                return false;
            }

            LIList<Food> lFood = isw.GetVisibleFood(this);
            LIList<Ant> lAnt = isw.GetVisibleAnts(this);
            LIList<Spider> lSpider = isw.GetVisibleSpiders(this);
            LIList<Message> lMessage = isw.GetVisibleMessages(this);

            while(lFood.Count > 0)
            {
                isw.DeleteFood(lFood.First.Value);
                lFood.RemoveFirst();
            }

            while (lAnt.Count > 0)
            {
                isw.DeleteAnt(lAnt.First.Value);
                lAnt.RemoveFirst();
            }
            while (lSpider.Count > 0)
            {
                isw.DeleteSpider(lSpider.First.Value);
                lSpider.RemoveFirst();
            }

            Map map = isw.GetMap();

            if (lMessage!=null)
            {
                LinkedListNode<Message> enumMsg = lMessage.First;
                LinkedListNode<PointWithIntensity> enumPwI, enumPwItemp;
                while (enumMsg != null)
                {
                    enumPwI = enumMsg.Value.Points.First;
                    while (enumPwI != null)
                    {
                        if ((enumPwI.Value.Position.X - rainPos.X) < AntHillConfig.rainWidth &&
                            (enumPwI.Value.Position.X - rainPos.X) >= 0
                            && (enumPwI.Value.Position.Y - rainPos.Y) < AntHillConfig.rainWidth
                            && (enumPwI.Value.Position.Y - rainPos.Y) >= 0)
                        {
                            map.RemoveMessage(enumMsg.Value.GetMessageType, enumPwI.Value.Position);
                            enumPwItemp = enumPwI;
                            enumPwI = enumPwI.Next;
                            enumMsg.Value.Points.Remove(enumPwItemp);
                        }
                        else
                            enumPwI = enumPwI.Next;
                    }
                    enumMsg = enumMsg.Next;
                } 
            }            

            // Rain is always on the map
            for (int i = 0; i < AntHillConfig.rainWidth; i++) // && i+this.Position.X < map.Width; i++)
            {
                for (int j = 0; j < AntHillConfig.rainWidth; j++) // && j+this.Position.Y < map.Height; j++)
                {
                    map.GetTile(this.Position.X + i, this.Position.Y + j).messages.Clear();
                }
            }
            return true;
        }
         
        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

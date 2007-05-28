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
            if ((x - Position.X) <= AntHillConfig.rainWidth  &&
                (x - Position.X) >=0 &&
                (y - Position.Y) <= AntHillConfig.rainWidth
                && (y - Position.Y) >=0)
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

            for(int i = 0; i < lFood.Count;++i)
                isw.DeleteFood(lFood[i]);

            for (int i = 0; i < lAnt.Count; ++i)
                isw.DeleteAnt(lAnt[i]);

            for (int i = 0; i < lSpider.Count; ++i)
                isw.DeleteSpider(lSpider[i]);

            
            if (lMessage!=null)
            {
                LinkedListNode<Message> enumMsg = lMessage.First;
                LinkedListNode<PointWithIntensity> enumPwI, enumPwItemp;
                while (enumMsg != null)
                {
                    enumPwI = enumMsg.Value.Points.First;
                    while (enumPwI != null)
                    {
                        if ((enumPwI.Value.Tile.Position.X - rainPos.X) <= AntHillConfig.rainWidth &&
                            (enumPwI.Value.Tile.Position.X - rainPos.X) >= 0
                            && (enumPwI.Value.Tile.Position.Y - rainPos.Y) <= AntHillConfig.rainWidth
                            && (enumPwI.Value.Tile.Position.Y - rainPos.Y) >= 0)
                        {
                            isw.GetMap().RemoveMessage(enumMsg.Value.GetMessageType, enumPwI.Value.Tile.Position);
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

            Map map = isw.GetMap();

            for (int i = 0; i < AntHillConfig.rainWidth && i+this.Position.X <isw.GetMap().Width; i++)
            {
                for (int j = 0; j < AntHillConfig.rainWidth && j+this.Position.Y < isw.GetMap().Height; j++)
                {
                    map.GetTile(i, j).messages.Clear();
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

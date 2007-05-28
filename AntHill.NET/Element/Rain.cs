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

            List<Food> lFood = isw.GetVisibleFood(this);
            List<Ant> lAnt = isw.GetVisibleAnts(this);
            List<Spider> lSpider = isw.GetVisibleSpiders(this);
            List<Message>lMessage = isw.GetVisibleMessages(this);

            for(int i = 0; i < lFood.Count;++i)
                isw.DeleteFood(lFood[i]);

            for (int i = 0; i < lAnt.Count; ++i)
                isw.DeleteAnt(lAnt[i]);

            for (int i = 0; i < lSpider.Count; ++i)
                isw.DeleteSpider(lSpider[i]);


            if (lMessage!=null)
            {
                foreach (Message m in lMessage)
                {
                    for (int j = 0; j < m.Points.Count; )
                    {
                        if ((m.Points[j].Tile.Position.X - rainPos.X) <= AntHillConfig.rainWidth &&
                            (m.Points[j].Tile.Position.X - rainPos.X) >=0 
                            && (m.Points[j].Tile.Position.Y - rainPos.Y) <= AntHillConfig.rainWidth
                            && (m.Points[j].Tile.Position.Y - rainPos.Y) >=0)
                        {
                            isw.GetMap().RemoveMessage(m.GetMessageType, m.Points[j].Tile.Position);
                            m.Points.RemoveAt(j);
                        }
                        else
                            j++;
                    }
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

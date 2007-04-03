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
            Random rand = new Random();
            timeToLive = rand.Next(AntHillConfig.rainMaxDuration+1);
        }

        public bool isRainingAt(int x, int y)
        {
            if (x >= Position.X && y >= Position.Y && x < Position.X + AntHillConfig.rainWidth &&
                y < Position.Y + AntHillConfig.rainWidth)
                return true;
            return false;
        }

        public override void Maintain(ISimulationWorld isw)
        {
            if (--timeToLive <= 0)
            {
                isw.DeleteRain();
                return;
            }

            List<Food> lFood = isw.GetVisibleFood(this);
            List<Ant> lAnt = isw.GetVisibleAnts(this);
            List<Spider> lSpider = isw.GetVisibleSpiders(this);
            List<Message>lMessage = isw.GetVisibleMessages(this);

            for(int i = 0;lFood!=null&& i < lFood.Count;++i)
                isw.DeleteFood(lFood[i]);

            for (int i = 0;lAnt!=null && i < lAnt.Count; ++i)
                isw.DeleteAnt(lAnt[i]);

            for (int i = 0;lSpider!=null && i < lSpider.Count; ++i)
                isw.DeleteSpider(lSpider[i]);


            if (lMessage!=null)
            {
                foreach (Message m in lMessage)
                {
                    for (int j = 0; j < m.points.Count; )
                    {
                        if (Math.Abs(m.points[j].Tile.Position.X - rainPos.X) <= AntHillConfig.rainWidth
                            && Math.Abs(m.points[j].Tile.Position.Y - rainPos.Y) <= AntHillConfig.rainWidth)
                            m.points.RemoveAt(j);
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
        }
         
        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

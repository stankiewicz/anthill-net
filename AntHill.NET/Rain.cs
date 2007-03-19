using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Rain : Element
    {
        Point rainPos;
        int timeToLive;


        public Rain(Point pos):base(pos)
        {
            rainPos = pos;
            Random rand = new Random();
            timeToLive = rand.Next(AntHillConfig.rainMaxDuration+1);
        }

        public override void Maintain(ISimulationWorld isw)
        {
            if (--timeToLive == 0)
            {
                isw.DeleteRain();
                return;
            }

            List<Food> lFood = isw.GetVisibleFood(this);
            List<Ant> lAnt = isw.GetVisibleAnts(this);
            List<Spider> lSpider = isw.GetVisibleSpiders(this);
            List<Message>lMessage=isw.GetVisibleMessages(this);

            for(int i = 0; i < lFood.Count;++i)
                isw.DeleteFood(lFood[i]);

            for (int i = 0; i < lAnt.Count; ++i)
                isw.DeleteAnt(lAnt[i]);

            for (int i = 0; i < lSpider.Count; ++i)
                isw.DeleteSpider(lSpider[i]);


            foreach(Message m in lMessage)
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

            Map map = isw.GetMap();

            for (int i = 0; i < AntHillConfig.rainWidth; i++)
            {
                for (int j = 0; j < AntHillConfig.rainWidth; j++)
                {
                    map.GetTile(i, j).messages.Clear();
                }
            }

            throw new Exception("The method or operation is not implemented.");
        }
         
        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
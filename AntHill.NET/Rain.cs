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

            foreach(Food f in lFood)
                isw.DeleteFood(f);
           
            foreach(Ant a in lAnt)
                isw.DeleteAnt(a);
            
            foreach(Spider s in lSpider)
                isw.DeleteSpider(s);

            foreach (Message m in lMessage)
            {
                
                for (int j = 0; j < m.points.Count; j++)
                {
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
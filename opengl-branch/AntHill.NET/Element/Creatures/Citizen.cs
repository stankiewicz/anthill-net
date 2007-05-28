using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{

    public abstract class Citizen : Ant
    {
        // odpowiednie Wiadomosci i ich intensywnosci
        List<Message> remembered=new List<Message>();
        List<int> rememberedIntensities=new List<int>();
        List<int> forgetting = new List<int>();
        public Citizen(Point pos):base(pos)
        {

        }

        public virtual void AddToSet(Message m, int intensity)
        {
            if (!remembered.Contains(m))
            {
                remembered.Add(m);
                
                rememberedIntensities.Add(intensity);
                forgetting.Add(AntHillConfig.antForgettingTime);
            }
        }
        
        public virtual void SpreadSignal(ISimulationWorld isw)
        {
            for (int i = 0; i < remembered.Count; i++)
            {
                if (--rememberedIntensities[i] <= 0 || --forgetting[i]<=0)
                {
                    remembered.RemoveAt(i);
                    rememberedIntensities.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < remembered.Count; ++i)
            {
                remembered[i].Spread(isw, this.Position, rememberedIntensities[i]);
            }
        }

        protected Food GetNearestFood(List<Food> foods)
        {
            if (foods.Count == 0) return null;

            int i = 0;
            int min = Int32.MaxValue;
            int tmp;
            for (int j = 0; j < foods.Count; j++)
            {
                if ((tmp = Distance(this.Position, foods[j].Position)) < min)
                {
                    i = j;
                    min = tmp;
                }
            }
            return foods[i];
        }

        public virtual Message ReadMessage(MessageType mt)
        {
            int which=-1;
            int max = -1;
            for (int i = 0; i < remembered.Count; i++)
            {
                if (remembered[i].GetMessageType == mt)
                {
                    if (rememberedIntensities[i] > max)
                    {
                        max = rememberedIntensities[i];
                        which = i;
                    }
                }
            }
            if (which == -1) return null;
            return remembered[which];
        }
    }
}
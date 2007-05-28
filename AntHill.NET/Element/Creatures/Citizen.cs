using System;
using System.Drawing;
using System.Collections.Generic;

namespace AntHill.NET
{

    public abstract class Citizen : Ant
    {
        // odpowiednie Wiadomosci i ich intensywnosci
        LIList<Message> remembered = new LIList<Message>();
        LIList<int> rememberedIntensities = new LIList<int>();
        LIList<int> forgetting = new LIList<int>();
        public Citizen(Point pos):base(pos)
        {

        }

        public virtual void AddToSet(Message m, int intensity)
        {
            if (!remembered.Contains(m))
            {
                remembered.AddLast(m);
                
                rememberedIntensities.AddLast(intensity);
                forgetting.AddLast(AntHillConfig.antForgettingTime);
            }
        }
        
        public virtual void SpreadSignal(ISimulationWorld isw)
        {
            LinkedListNode<int> msg1 = rememberedIntensities.First;
            LinkedListNode<int> msg1T;
            LinkedListNode<int> msg2 = forgetting.First;
            LinkedListNode<int> msg2T;
            LinkedListNode<Message> msg3 = remembered.First;
            LinkedListNode<Message> msg3T;
            while (msg3 != null)
            {
                if (msg3.Value.Empty)
                {
                    msg1T = msg1;
                    msg2T = msg2;
                    msg3T = msg3;
                    msg1 = msg1.Next;
                    msg2 = msg2.Next;
                    msg3 = msg3.Next;
                    rememberedIntensities.Remove(msg1T);
                    forgetting.Remove(msg2T);
                    remembered.Remove(msg3T);
                }
                else
                {
                    msg3.Value.Spread(isw, this.Position, msg1.Value);
                    msg3 = msg3.Next;
                    msg2 = msg2.Next;
                    msg1 = msg1.Next;
                }
            }
            //for (int i = 0; i < remembered.Count; i++)
            //{
            //    if (--rememberedIntensities[i] <= 0 || --forgetting[i]<=0)
            //    {
            //        remembered.RemoveAt(i);
            //        rememberedIntensities.RemoveAt(i);
            //        i--;
            //    }
            //}
            //for (int i = 0; i < remembered.Count; ++i)
            //{
            //    remembered[i].Spread(isw, this.Position, rememberedIntensities[i]);
            //}
        }

        protected Food GetNearestFood(LIList<Food> foods)
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

       protected virtual Message ReadMessage(MessageType mt)
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
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{

    public enum MessageType
    {
        QueenIsHungry,
        QueenInDanger,
        FoodLocalization,
        SpiderLocalization
    }

    public class Message : Element
    {
        private MessageType type;
        public List<PointWithIntensity> points;
        public Message(Point pos, MessageType mt ):base(pos)
        {
            type = mt;
            points = new List<PointWithIntensity>();
        }
        public void AddPoint(List<PointWithIntensity> newPoint)
        {
            points.AddRange(newPoint);
        }

        public bool Empty { get { return points.Count == 0; } }
        public MessageType GetMessageType
        {
            get { return type; }
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (--points[i].Intensity == 0)
                {
                    points[i].Tile.messages.Remove(this);
                    points.RemoveAt(i);
                    i--;
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
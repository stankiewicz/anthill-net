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
        public Message(Point pos):base(pos)
        {

        }
        public void AddPoint(List<PointWithIntensity> newPoint)
        {
        }


        public bool Empty { get { return points.Count == 0; } }
        public MessageType GetMessageType
        {
            get { return type; }
        }

        public override void Maintain(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
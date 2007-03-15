using System;
using System.Collections.Generic;

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

        public void AddPoint(List<PointWithIntensity> newPoint)
        {
        }

        public MessageType GetMessageType
        {
            get { return type; }
        }

        public override void Maintain(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Destroy()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
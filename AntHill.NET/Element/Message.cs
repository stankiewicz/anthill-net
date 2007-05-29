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
        private Point location;

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        private LIList<PointWithIntensity> points;
        public LIList<PointWithIntensity> Points
        {
            get { return points; }
        }
        public void AddPoint(Tile t, int intensity, Map map)
        {
            points.AddLast(new PointWithIntensity(t, intensity));
            map.AddMessage(this.GetMessageType, t.Position);
        }

        public Message(Point pos, MessageType mt ):base(pos)
        {
            type = mt;
            points = new LIList<PointWithIntensity>();
        }
        public Message(Point pos, MessageType mt, Point location)
            : base(pos)
        {
            type = mt;
            points = new LIList<PointWithIntensity>();
            this.location = location;
        }
        //public void AddPoint(LIList<PointWithIntensity> newPoint)
        //{
        //    points.AddLast(newPoint);
        //}

        public bool Empty { get { return points.Count == 0; } }
        public MessageType GetMessageType
        {
            get { return type; }
        }

        public PointWithIntensity GetPoint(Point p)
        {
            LIList<PointWithIntensity>.Enumerator e = points.GetEnumerator();
            while(e.MoveNext())            
                if (e.Current.Tile.Position == p) return e.Current;            
            return null;
        }
        private int GetId(int x, int y)
        {           
            LIList<PointWithIntensity>.Enumerator e = points.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                Tile t = e.Current.Tile;
                if (t.Position.X == x && t.Position.Y == y)
                    return i;
                i++;
            }            
            return -1;
        }
        private PointWithIntensity GetPoint(int x, int y)
        {
            LIList<PointWithIntensity>.Enumerator e = points.GetEnumerator();           
            while (e.MoveNext())
            {
                Tile t = e.Current.Tile;
                if (t.Position.X == x && t.Position.Y == y)
                    return e.Current;
            }
            return null;
        }
        public override bool Maintain(ISimulationWorld isw)
        {
            LinkedListNode<PointWithIntensity> msg = points.First;
            LinkedListNode<PointWithIntensity> msgT;
            while (msg != null)
            {
                if (--msg.Value.Intensity <= 0)
                {
                    isw.GetMap().RemoveMessage(this.GetMessageType, msg.Value.Tile.Position);
                    msg.Value.Tile.messages.Remove(this);

                    msgT = msg;
                    msg = msg.Next;
                    points.Remove(msgT);
                }
                else
                {
                    msg = msg.Next;
                }

            }
            return true;
        }
        public void Spread(ISimulationWorld isw, Point point, int intensity)
        {
            int radius = AntHillConfig.messageRadius, radius2 = radius * radius;
            int x, y;
            Map map = isw.GetMap();
            PointWithIntensity PwI;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    if (i * i + j * j <= radius2)
                    {
                        x = i + point.X;
                        y = j + point.Y;
                        if (map.Inside(x, y))
                        {// czy wogole w srodku
                            if (map.GetTile(x, y).TileType == TileType.Wall) continue;
                            if (map.GetTile(x, y).messages.Contains(this))
                            {// czy zawiera                                
                                if ((PwI = GetPoint(x, y)) != null)// this.points.Contains(new PointWithIntensity(map.GetTile(i + point.X, j + point.Y), 0)))
                                {// czy punkt istnieje                                    
                                    if (PwI.Intensity < intensity)
                                    {// czy zwiekszyc intensywnosc
                                        PwI.Intensity = intensity;
                                    }
                                }
                                else
                                {// nie ma punktu?? w sumie takiej sytuacji nie powinno byc
                                    this.points.AddLast(new PointWithIntensity(map.GetTile(x, y), intensity));
                                    //update map
                                    map.AddMessage(this.GetMessageType, new Point(x, y));
                                }
                            }
                            else
                            {// nie ma w danym tile - wrzucamy
                                map.GetTile(x, y).messages.AddLast(this);
                                this.points.AddLast(new PointWithIntensity(map.GetTile(x, y), intensity));
                                //update map
                                map.AddMessage(this.GetMessageType, new Point(x, y));
                            }
                        }
                    }
                }
            }
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

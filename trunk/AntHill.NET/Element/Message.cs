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

        private List<PointWithIntensity> points;
        public List<PointWithIntensity> Points
        {
            get { return points; }
        }
        public void AddPoint(Tile t, int intensity, Map map)
        {
            points.Add(new PointWithIntensity(t, intensity));
            map.AddMessage(this.GetMessageType, t.Position);
        }

        public Message(Point pos, MessageType mt ):base(pos)
        {
            type = mt;
            points = new List<PointWithIntensity>();
        }
        public Message(Point pos, MessageType mt, Point location)
            : base(pos)
        {
            type = mt;
            points = new List<PointWithIntensity>();
            this.location = location;
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

        public PointWithIntensity GetPoint(Point p)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Tile.Position == p) return points[i];
            }
            return null;
        }
        private int GetId(int x, int y)
        {
            int id = -1;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Tile.Position.X==x && points[i].Tile.Position.Y== y)
                    return i;
            }
            return id;
        }
        public override bool Maintain(ISimulationWorld isw)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (--points[i].Intensity == 0)
                {
                    isw.GetMap().RemoveMessage(this.GetMessageType, points[i].Tile.Position);
                    points[i].Tile.messages.Remove(this);
                    points.RemoveAt(i);
                    i--;
                }
            }
            return true;
        }
        public void Spread(ISimulationWorld isw,Point point,int intensity)
        {
            Map map = isw.GetMap();
            for (int i = -AntHillConfig.messageRadius; i < AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j < AntHillConfig.messageRadius; j++)
                {
                    if (i * i + j * j < AntHillConfig.messageRadius * AntHillConfig.messageRadius)
                    {
                        if (map.Inside(i+point.X, j+point.Y))
                        {// czy wogole w srodku
                            if (map.GetTile(i + point.X, j + point.Y).TileType == TileType.Wall) continue;
                            if (map.GetTile(i + point.X, j + point.Y).TileType == TileType.Wall) continue;
                            if (map.GetTile(i + point.X, j + point.Y).messages.Contains(this))
                            {// czy zawiera
                                int idx;
                                if ((idx = GetId(i + point.X, j + point.Y))!=-1)// this.points.Contains(new PointWithIntensity(map.GetTile(i + point.X, j + point.Y), 0)))
                                {// czy punkt istnieje
                                    //int idx = points.IndexOf(new PointWithIntensity(map.GetTile(i + point.X, j + point.Y), 0));
                                    if (points[idx].Intensity < intensity)
                                    {// czy zwiekszyc intensywnosc
                                        points[idx].Intensity = intensity;
                                    }

                                }
                                else
                                {// nie ma punktu?? w sumie takiej sytuacji nie powinno byc
                                    this.points.Add(new PointWithIntensity(map.GetTile(i + point.X, j + point.Y), intensity));
                                    //update map
                                    map.AddMessage(this.GetMessageType, new Point(i + point.X, j + point.Y));
                                }
                            }
                            else
                            {// nie ma w danym tile - wrzucamy
                                map.GetTile(i + point.X, j + point.Y).messages.Add(this);
                                this.points.Add(new PointWithIntensity(map.GetTile(i + point.X, j + point.Y), intensity));
                                //update map
                                map.AddMessage(this.GetMessageType, new Point(i + point.X, j + point.Y));
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
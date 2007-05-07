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

        public PointWithIntensity GetPoint(Point p)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Tile.Position == p) return points[i];
            }
            return null;
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
        public void Spread(ISimulationWorld isw,Point point,int intensity)
        {
            Map map = isw.GetMap();
            for (int i = -AntHillConfig.messageRadius; i < AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j < AntHillConfig.messageRadius; j++)
                {
                    if (i * i + j * j < AntHillConfig.messageRadius * AntHillConfig.messageRadius)
                    {
                        if (map.Inside(i, j))
                        {// czy wogole w srodku
                            if (map.GetTile(i, j).messages.Contains(this))
                            {// czy zawiera

                                if (this.points.Contains(new PointWithIntensity(map.GetTile(i, j), 0)))
                                {// czy punkt istnieje
                                    int idx = points.IndexOf(new PointWithIntensity(map.GetTile(i, j), 0));
                                    if (points[idx].Intensity < intensity)
                                    {// czy zwiekszyc intensywnosc
                                        points[idx].Intensity = intensity;
                                    }

                                }
                                else
                                {// nie ma punktu?? w sumie takiej sytuacji nie powinno byc
                                    this.points.Add(new PointWithIntensity(map.GetTile(i, j), intensity));
                                }
                            }
                            else
                            {// nie ma w danym tile - wrzucamy
                                map.GetTile(i, j).messages.Add(this);
                                this.points.Add(new PointWithIntensity(map.GetTile(i, j), intensity));
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
using System;
using System.Drawing;
using AntHill.NET;

namespace AntHill.NET
{
    public class PointWithIntensity
    {
        public PointWithIntensity(Tile t, int i)
        {
            tile = t;
            intensity = i;
        }

        private Tile tile;
        public Tile Tile
        {
            get { return tile; }
            set { tile = value; }
        }

        private int intensity;
        public int Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }
    }
}
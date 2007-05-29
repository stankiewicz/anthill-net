using System;
using System.Drawing;
using AntHill.NET;

namespace AntHill.NET
{
    public class PointWithIntensity
    {
        private Tile tile;

        public PointWithIntensity(Tile t, int i)
        {
            tile = t;
            intensity = i;
        }

        public Point Position
        {
            get { return this.tile.Position; }
        }
        
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
        public override bool Equals(object obj)
        {
            if (obj is PointWithIntensity)
            {
                //LOL... a wyj¹tki? ----------------------------\/ tu siê bêdzie sypaæ :-P
                if (this.tile.Equals(((PointWithIntensity)obj).Tile)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
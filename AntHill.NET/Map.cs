using System;

namespace AntHill.NET
{
    public class Map
    {
        private int width;
        private int height;
        private Tile[,] tiles;

        public Map(int w, int h, Tile[,] t)
        {
            width = w;
            height = h;
            tiles = t;
        }
        public bool Inside(int x,int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }


        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Tile GetTile(int x, int y) { return tiles[x, y]; }
    }
}
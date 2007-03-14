using System;

namespace AntHill.NET
{

    public class Map
    {
        private int width;
        private int height;
        private Tile[][] tiles;

        public Map() { }

        public int GetWidth
        {
            get { return width; }
        }

        public int GetHeight
        {
            get { return height; }
        }

        public Tile GetTile(int x, int y) { return tiles[x][y]; }
    }
}
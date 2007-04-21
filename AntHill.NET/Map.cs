using System;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Map
    {
        private int width;
        private int height;
        private Tile[,] tiles;

        private List<Tile> indoorTiles, outdoorTiles, wallTiles;

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

        public Map(int w, int h, Tile[,] tiles)
        {
            width = w;
            height = h;
            this.tiles = tiles;

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            foreach (Tile t in tiles)
            {
                switch (t.TileType)
                {
                    case TileType.Wall:
                        wallTiles.Add(t);
                        break;
                    case TileType.Outdoor:
                        outdoorTiles.Add(t);
                        break;
                    case TileType.Indoor:
                        indoorTiles.Add(t);
                        break;
                    default:
                        break;
                }
            }
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

        public Tile GetRandomTile(TileType tt)
        {
            switch (tt)
            {
                case TileType.Wall:
                    if (wallTiles.Count == 0) return null;
                    return wallTiles[Randomizer.Next(wallTiles.Count)];
                case TileType.Outdoor:
                    if (outdoorTiles.Count == 0) return null;
                    return outdoorTiles[Randomizer.Next(outdoorTiles.Count)];
                case TileType.Indoor:
                    if (indoorTiles.Count == 0) return null;
                    return indoorTiles[Randomizer.Next(indoorTiles.Count)];
                default:
                    return null;
            }
        }

        public void DestroyWall(Tile t)
        {
            t.TileType = TileType.Outdoor;
            wallTiles.Remove(t);
            indoorTiles.Add(t);
        }
    }
}
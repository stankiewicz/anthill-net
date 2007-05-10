using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntHill.NET
{
    public class Map
    {
        private int width;
        private int height;
        private Tile[,] tiles;

        private Bitmap bmpOutdoorIndoor, bmpWall;
        private Region rWall;

        private List<Tile> indoorTiles, outdoorTiles, wallTiles;

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

        public Map(int w, int h, Tile[,] tiles)
        {
            width = w;
            height = h;
            //this.tiles = (Tile[,])tiles.Clone();
            this.tiles = new Tile[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    this.tiles[x, y] = new Tile(tiles[x, y].TileType, tiles[x, y].Position);

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            //Initialize bitmaps & tiles' lists
            bmpOutdoorIndoor = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            bmpWall = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            (rWall = new Region()).MakeEmpty();

            Tile t;
            Graphics gWall = Graphics.FromImage(bmpWall),
                    gOutdoorIndoor = Graphics.FromImage(bmpOutdoorIndoor);
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    Rectangle rect = new Rectangle(x * AntHillConfig.tileSize,
                     y * AntHillConfig.tileSize,
                     AntHillConfig.tileSize,
                     AntHillConfig.tileSize);

                    switch ((t = tiles[x, y]).TileType)
                    {
                        //Indoor&Outdoor are on the same bitmap
                        //because they're not overlapping
                        case TileType.Outdoor:
                            outdoorTiles.Add(t);
                            gOutdoorIndoor.DrawImage(AHGraphics.GetTile(TileType.Outdoor), rect);
                            break;
                        case TileType.Indoor:
                            indoorTiles.Add(t);
                            gOutdoorIndoor.DrawImage(AHGraphics.GetTile(TileType.Indoor), rect);
                            break;
                        //Wall's on a separate, clipped bitmap;
                        //We also have to draw indoor under the wall
                        case TileType.Wall:
                            wallTiles.Add(t);
                            rWall.Union(rect);
                            gWall.DrawImage(AHGraphics.GetTile(TileType.Wall), rect);
                            gOutdoorIndoor.DrawImage(AHGraphics.GetTile(TileType.Indoor), rect);
                            break;

                    }
                }
            }
        }

        public void DrawMap(Graphics g, Rectangle drawingRect, float middleX, float middleY, float magnitude)
        {
            int width = drawingRect.Width, height = drawingRect.Height;
            Matrix m = new Matrix();
            m.Scale(magnitude, magnitude);
            float mXt = middleX,
                  mYt = middleY;

            Region r = rWall.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);

            g.SetClip(drawingRect);
            g.DrawImage(bmpOutdoorIndoor, -mXt, -mYt, bmpOutdoorIndoor.Width * magnitude, bmpOutdoorIndoor.Height * magnitude);
            g.SetClip(r, System.Drawing.Drawing2D.CombineMode.Intersect);
            g.DrawImage(bmpWall, -mXt, -mYt, bmpWall.Width * magnitude, bmpWall.Height * magnitude);
        }

        public bool Inside(int x, int y)
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
            wallTiles.Remove(t);
            t.TileType = TileType.Indoor;
            
            indoorTiles.Add(t);
            Rectangle rec = new Rectangle(t.Position.X * AntHillConfig.tileSize,
                                         t.Position.Y * AntHillConfig.tileSize,
                                         AntHillConfig.tileSize,
                                         AntHillConfig.tileSize);
            this.rWall.Exclude(rec);
        }
    }
}
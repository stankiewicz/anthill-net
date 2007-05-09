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

        private Bitmap bmpOutdoor, bmpIndoor, bmpWall;
        private Region rOutdoor, rIndoor, rWall;

        private List<Tile> indoorTiles, outdoorTiles, wallTiles;

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

        public Map(int w, int h, Tile[,] tiles)
        {
            width = w;
            height = h;
            this.tiles = (Tile[,])tiles.Clone();

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            //Initialize bitmaps & tiles' lists
            bmpIndoor = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            bmpOutdoor = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            bmpWall = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            (rIndoor = new Region()).MakeEmpty();
            (rOutdoor = new Region()).MakeEmpty();
            (rWall = new Region()).MakeEmpty();

            Tile t;
            List<Tile> tmpTile;
            Graphics gWall = Graphics.FromImage(bmpWall),
                    gIndoor = Graphics.FromImage(bmpIndoor),
                    gOutdoor = Graphics.FromImage(bmpOutdoor),
                    gTmp;
            Region r;
            Rectangle rect;
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    switch ((t = tiles[x, y]).TileType)
                    {
                        case TileType.Wall:
                            tmpTile = wallTiles;
                            gTmp = gWall;
                            r = rWall;
                            break;
                        case TileType.Outdoor:
                            tmpTile = outdoorTiles;
                            gTmp = gOutdoor;
                            r = rOutdoor;
                            break;
                        case TileType.Indoor:
                        default:
                            tmpTile = indoorTiles;
                            gTmp = gIndoor;
                            r = rIndoor;
                            break;
                    }
                    rect = new Rectangle(x * AntHillConfig.tileSize,
                                         y * AntHillConfig.tileSize,
                                         AntHillConfig.tileSize,
                                         AntHillConfig.tileSize);
                    tmpTile.Add(t);
                    gTmp.DrawImage(t.GetBitmap(), rect);
                    r.Union(rect);
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

            Region r;
            //todo:
            //r translate to 0,0
            //r zoom
            //r translate to dest
            r = rOutdoor.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);
            g.SetClip(drawingRect);
            g.SetClip(r, System.Drawing.Drawing2D.CombineMode.Intersect);
            g.DrawImage(bmpOutdoor, -mXt, -mYt, bmpOutdoor.Width * magnitude, bmpOutdoor.Height * magnitude);
            r = rIndoor.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);
            g.SetClip(drawingRect);
            g.SetClip(r, System.Drawing.Drawing2D.CombineMode.Intersect);
            g.DrawImage(bmpIndoor, -mXt, -mYt, bmpIndoor.Width * magnitude, bmpIndoor.Height * magnitude);
            r = rWall.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);
            g.SetClip(drawingRect);
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
            t.TileType = TileType.Outdoor;
            wallTiles.Remove(t);
            indoorTiles.Add(t);
        }
    }
}
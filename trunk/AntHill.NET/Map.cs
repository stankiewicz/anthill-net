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
        private MessageCount[,] messagesCount;
        struct MessageCount
        {
            int queenInDanger,
                queenIsHungry,
                spiderLocation,
                foodLocation;

            public int GetCount(MessageType mt)
            {
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        return queenIsHungry;
                    case MessageType.QueenInDanger:
                        return queenInDanger;
                    case MessageType.FoodLocalization:
                        return foodLocation;
                    case MessageType.SpiderLocalization:
                        return spiderLocation;
                }
                return -1;
            }

            public void SetCount(MessageType mt, int count)
            {
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        queenIsHungry = count;
                        break;
                    case MessageType.QueenInDanger:
                        queenInDanger = count;
                        break;
                    case MessageType.FoodLocalization:
                        foodLocation = count;
                        break;
                    case MessageType.SpiderLocalization:
                        spiderLocation = count;
                        break;
                }
            }

            public bool IncreaseCount(MessageType mt)
            {
                bool flag = false;
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        if (queenIsHungry == 0) flag = true;
                        queenIsHungry++;
                        break;
                    case MessageType.QueenInDanger:
                        if (queenInDanger == 0) flag = true;
                        queenInDanger++;
                        break;
                    case MessageType.FoodLocalization:
                        if (foodLocation == 0) flag = true;
                        foodLocation++;
                        break;
                    case MessageType.SpiderLocalization:
                        if (spiderLocation == 0) flag = true;
                        spiderLocation++;
                        break;
                }
                return flag;
            }

            public bool LowerCount(MessageType mt)
            {
                bool flag = false;
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        if (--queenIsHungry == 0) flag = true;
                        break;
                    case MessageType.QueenInDanger:
                        if (--queenInDanger == 0) flag = true;
                        break;
                    case MessageType.FoodLocalization:
                        if (--foodLocation == 0) flag = true;
                        break;
                    case MessageType.SpiderLocalization:
                        if (--spiderLocation == 0) flag = true;
                        break;
                }
                return flag;
            }
        }

        private Bitmap bmpOutdoorIndoor, bmpWall, bmpMessages;
        private Region rWall, rMessages;
        private Rectangle rcQueenInDanger, rcQueenIsHungry, rcSpiderLocation, rcFoodLocation;

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

            messagesCount = new MessageCount[width, height];

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            //Initialize bitmaps & tiles' lists
            bmpOutdoorIndoor = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            bmpWall = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            (rWall = new Region()).MakeEmpty();
            bmpMessages = new Bitmap(width * AntHillConfig.tileSize, height * AntHillConfig.tileSize);
            (rMessages = new Region()).MakeEmpty();
            //(rSignals = new Region()).MakeInfinite();

            rcFoodLocation = new Rectangle(AntHillConfig.tileSize / 2, 0, AntHillConfig.tileSize/2, AntHillConfig.tileSize/2);
            rcQueenInDanger = new Rectangle(0, 0, AntHillConfig.tileSize/2, AntHillConfig.tileSize/2);
            rcQueenIsHungry = new Rectangle(AntHillConfig.tileSize / 2, AntHillConfig.tileSize / 2, AntHillConfig.tileSize/2, AntHillConfig.tileSize/2);
            rcSpiderLocation = new Rectangle(0, AntHillConfig.tileSize / 2, AntHillConfig.tileSize/2, AntHillConfig.tileSize/2);
            
            Tile t;
            Graphics gWall = Graphics.FromImage(bmpWall),
                    gOutdoorIndoor = Graphics.FromImage(bmpOutdoorIndoor),
                    gSignals = Graphics.FromImage(bmpMessages);
            
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

                    gSignals.DrawImage(AHGraphics.GetMessagesBitmap(), rect);
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

            r = rMessages.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);
            g.SetClip(drawingRect);
            g.SetClip(r, CombineMode.Intersect);
            g.DrawImage(bmpMessages, -mXt, -mYt, bmpMessages.Width * magnitude, bmpMessages.Height * magnitude);
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

        private Rectangle GetMessageRect(MessageType mt)
        {
            switch (mt)
            {
                case MessageType.QueenIsHungry:
                    return rcQueenIsHungry;
                case MessageType.QueenInDanger:
                    return rcQueenInDanger;
                case MessageType.FoodLocalization:
                    return rcFoodLocation;
                case MessageType.SpiderLocalization:
                    return rcSpiderLocation;
            }
            return new Rectangle();
        }

        public void AddMessage(MessageType mt, Point pos)
        {
            int x = pos.X, y = pos.Y;            
            
            if (messagesCount[x, y].IncreaseCount(mt) == true)
            {
                float magnitude = AntHillConfig.curMagnitude;
                Matrix m = new Matrix();
                m.Scale(magnitude, magnitude);

                Region r = new Region(GetMessageRect(mt));
                r.Transform(m);
                r.Translate(x * AntHillConfig.tileSize * magnitude, y * AntHillConfig.tileSize * magnitude);
                rMessages.Union(r);
            }
        }

        public void RemoveMessage(MessageType mt, Point pos)
        {
            int x = pos.X, y = pos.Y;

            if (messagesCount[x, y].LowerCount(mt) == true)
            {
                float magnitude = AntHillConfig.curMagnitude;
                Matrix m = new Matrix();
                m.Scale(magnitude, magnitude);

                Region r = new Region(GetMessageRect(mt));
                r.Transform(m);
                r.Translate(x * AntHillConfig.tileSize * magnitude, y * AntHillConfig.tileSize * magnitude);
                rMessages.Exclude(r);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntHill.NET
{
    public class Map
    {
        private int width, height, realWidth, realHeight;
        private Tile[,] tiles;
        private MessageCount[,] messagesCount;

        private Bitmap bmpOutdoorIndoor, bmpWall, bmpMessages;
        private Region rWall, rMessages;
        private Rectangle rcQueenInDanger, rcQueenIsHungry, rcSpiderLocation, rcFoodLocation;

        private List<Tile> indoorTiles, outdoorTiles, wallTiles;

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

        public Map(int w, int h, Tile[,] tiles)
        {
            int tilesSize = AntHillConfig.tileSize;

            this.width = w;
            this.height = h;
            realWidth = tilesSize * width;
            realHeight = tilesSize * height;

            this.tiles = new Tile[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    this.tiles[x, y] = new Tile(tiles[x, y].TileType, tiles[x, y].Position);

            messagesCount = new MessageCount[width, height];

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            //Initialize bitmaps & tiles' lists
            bmpOutdoorIndoor = new Bitmap(realWidth, realHeight);
            bmpWall = new Bitmap(realWidth, realHeight);
            bmpMessages = new Bitmap(realWidth, realHeight);

            (rWall = new Region()).MakeEmpty();
            (rMessages = new Region()).MakeEmpty();

            int halfTile = AntHillConfig.tileSize >> 1;
            rcFoodLocation = new Rectangle(halfTile, 0,
                                           halfTile, halfTile);
            rcQueenInDanger = new Rectangle(0, 0,
                                            halfTile, halfTile);
            rcQueenIsHungry = new Rectangle(halfTile, halfTile,
                                            halfTile, halfTile);
            rcSpiderLocation = new Rectangle(0, halfTile,
                                             halfTile, halfTile);
            
            Tile t;
            Graphics gWall = Graphics.FromImage(bmpWall),
                    gOutdoorIndoor = Graphics.FromImage(bmpOutdoorIndoor),
                    gMessages = Graphics.FromImage(bmpMessages);
            
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    Rectangle rect = new Rectangle(x * tilesSize, y * tilesSize,
                                                   tilesSize, tilesSize);

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

                    gMessages.DrawImage(AHGraphics.GetMessagesBitmap(), rect);
                }
            }

            if (outdoorTiles.Count == 0)
                throw new Exception(Properties.Resources.noOutdoorTilesError);
            if (indoorTiles.Count == 0)
                throw new Exception(Properties.Resources.noIndoorTilesError);
        }

        public void DrawMap(Graphics g, Rectangle drawingRect, float mXt, float mYt, float magnitude)
        {
            //int width = drawingRect.Width, height = drawingRect.Height;
            int magnitudedMapWidth = (int)(bmpMessages.Width * magnitude), /* can be any bitmap or map.width * tileSize */
                magnitudedMapHeight = (int)(bmpMessages.Height * magnitude);
            Matrix m = new Matrix();
            m.Scale(magnitude, magnitude);
           
            Region r = rWall.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);

            g.SetClip(drawingRect);
            g.DrawImage(bmpOutdoorIndoor, -mXt, -mYt, magnitudedMapWidth, magnitudedMapHeight);
            g.SetClip(r, System.Drawing.Drawing2D.CombineMode.Intersect);
            g.DrawImage(bmpWall, -mXt, -mYt, magnitudedMapWidth, magnitudedMapHeight);

            r = rMessages.Clone();
            r.Transform(m);
            r.Translate(-mXt, -mYt);
            g.SetClip(drawingRect);
            g.SetClip(r, CombineMode.Intersect);
            g.DrawImage(bmpMessages, -mXt, -mYt, magnitudedMapWidth, magnitudedMapHeight);
        }

        public bool Inside(int x, int y)
        {
            return (x >= 0) && (x < width) && (y >= 0) && (y < height);
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
                                         AntHillConfig.tileSize, AntHillConfig.tileSize);
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
                Region r = new Region(GetMessageRect(mt));
                r.Translate(x * AntHillConfig.tileSize,
                            y * AntHillConfig.tileSize);
                rMessages.Union(r);
            }
        }

        public void RemoveMessage(MessageType mt, Point pos)
        {
            int x = pos.X, y = pos.Y;

            if (messagesCount[x, y].LowerCount(mt) == true)
            {
                Region r = new Region(GetMessageRect(mt));
                r.Translate(x * AntHillConfig.tileSize,
                            y * AntHillConfig.tileSize);
                rMessages.Exclude(r);
            }
        }

        #region MessageCount
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
        #endregion
    }
}
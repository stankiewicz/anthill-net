using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace AntHill.NET
{
    public class Map
    {
        private int width, height;
        private Tile[,] tiles;
        private MessageCount[,] messagesCount;

        private LIList<Tile> indoorTiles, wallTiles, outdoorTiles;
        

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

        public MessageCount[,] MsgCount
        {
            get { return messagesCount; }
        }

        public Map(int w, int h, Tile[,] tiles)
        {
            int tilesSize = AntHillConfig.tileSize;

            this.width = w;
            this.height = h;

            this.tiles = new Tile[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    this.tiles[x, y] = new Tile(tiles[x, y].TileType, tiles[x, y].Position);

            messagesCount = new MessageCount[width, height];

            indoorTiles = new LIList<Tile>();
            outdoorTiles = new LIList<Tile>();
            wallTiles = new LIList<Tile>();

            Tile t;
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    switch ((t = tiles[x, y]).TileType)
                    {
                        case TileType.Outdoor:
                            outdoorTiles.AddLast(t);
                            break;
                        case TileType.Indoor:
                            indoorTiles.AddLast(t);
                            break;
                        case TileType.Wall:
                            wallTiles.AddLast(t);
                            break;
                    }
                }
            }

            if (outdoorTiles.Count == 0)
                throw new Exception(Properties.Resources.noOutdoorTilesError);
            if (indoorTiles.Count == 0)
                throw new Exception(Properties.Resources.noIndoorTilesError);
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
        public Tile GetTile(Point pos) { return tiles[pos.X, pos.Y]; }

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

        public Tile GetRandomIndoorOrOutdoorTile()
        {
            int c = Randomizer.Next(outdoorTiles.Count + indoorTiles.Count);
            if (c < outdoorTiles.Count)
                return outdoorTiles[c];
            return indoorTiles[c - outdoorTiles.Count];
        }

        public void DestroyWall(Tile t)
        {
            wallTiles.Remove(t);
            t.TileType = TileType.Indoor;
            
            indoorTiles.AddLast(t);
        }

        public void AddMessage(MessageType mt, Point pos)
        {
            messagesCount[pos.X, pos.Y].IncreaseCount(mt);
        }

        public void RemoveMessage(MessageType mt, Point pos)
        {
            messagesCount[pos.X, pos.Y].LowerCount(mt);
        }

        #region MessageCount
        public struct MessageCount
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

            public void IncreaseCount(MessageType mt)
            {
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        queenIsHungry++;
                        break;
                    case MessageType.QueenInDanger:
                        queenInDanger++;
                        break;
                    case MessageType.FoodLocalization:
                        foodLocation++;
                        break;
                    case MessageType.SpiderLocalization:
                        spiderLocation++;
                        break;
                }
            }

            public void LowerCount(MessageType mt)
            {
                switch (mt)
                {
                    case MessageType.QueenIsHungry:
                        --queenIsHungry;
                        break;
                    case MessageType.QueenInDanger:
                        --queenInDanger;
                        break;
                    case MessageType.FoodLocalization:
                        --foodLocation;
                        break;
                    case MessageType.SpiderLocalization:
                        --spiderLocation;
                        break;
                }
            }
        }
        #endregion
    }
}
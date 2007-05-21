using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntHill.NET
{
    public class Map
    {
        private int width, height;
        private Tile[,] tiles;
        private MessageCount[,] messagesCount;

        private List<Tile> indoorTiles, outdoorTiles, wallTiles;

        public int GetIndoorCount { get { return indoorTiles.Count; } }
        public int GetOutdoorCount { get { return outdoorTiles.Count; } }
        public int GetWallCount { get { return wallTiles.Count; } }

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

            indoorTiles = new List<Tile>();
            outdoorTiles = new List<Tile>();
            wallTiles = new List<Tile>();

            Tile t;
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    switch ((t = tiles[x, y]).TileType)
                    {
                        case TileType.Outdoor:
                            outdoorTiles.Add(t);
                            break;
                        case TileType.Indoor:
                            indoorTiles.Add(t);
                            break;
                        case TileType.Wall:
                            wallTiles.Add(t);
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
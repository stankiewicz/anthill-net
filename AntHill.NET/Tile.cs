using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public enum TileType { Wall, Outdoor, Indoor };

    static class TileBitmap
    {
        public static bool Init()
        {
            try
            {
                wall = new Bitmap("Graphics/stoneTile.png");
                indoor = new Bitmap("Graphics/sandTile.png");
                outdoor = new Bitmap("Graphics/grassTile.png");
            }
            catch
            {
                wall = indoor = outdoor = null;
                return false;
            }

            return true;
        }

        static public Bitmap wall, indoor, outdoor;
    }


    public class Tile
    {
        private TileType tileType;
        public TileType TileType
        {
            get { return tileType; }
        }

        /*
         * List of references to messages active on this tile.
         * We can use this to speed-up searching for messages,
         * at the cost of memory.
         */
        public List<Message> messages;

        public Tile(TileType ttype)
        {
            tileType = ttype;
            messages = new List<Message>();
        }

        private Point position;
        public Point Position { get { return position; } }

        public Image GetImage
        {
            get
            {
                if (tileType == TileType.Indoor)
                    return TileBitmap.indoor;
                if (tileType == TileType.Outdoor)
                    return TileBitmap.outdoor;
                return TileBitmap.wall;
            }
        }
    }
}
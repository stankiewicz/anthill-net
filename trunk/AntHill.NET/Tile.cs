using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public enum TileType { Wall, Outdoor, Indoor };

    class TileBitmap
    {
        static public Bitmap wall = new Bitmap("Graphics/stoneTile.png");
        static public Bitmap indoor = new Bitmap("Graphics/sandTile.png");
        static public Bitmap outdoor = new Bitmap("Graphics/grassTile.png");
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

        private System.Drawing.Point position;
        public Point Position { get { return position; } }
        public Image Image
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
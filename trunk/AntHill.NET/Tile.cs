using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public enum TileType { Wall, Outdoor, Indoor };

    public class Tile
    {
        private TileType tileType;
        public TileType GetTileType
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
    }
}
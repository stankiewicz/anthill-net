using System;
using System.Collections.Generic;

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
    }
}
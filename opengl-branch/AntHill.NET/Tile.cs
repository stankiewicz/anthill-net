using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public enum TileType { Wall, Outdoor, Indoor };

    public class Tile
    {
        private TileType tileType;
        public TileType TileType
        {
            get { return tileType; }
            set { tileType = value; }
        }
        
        /*
         * List of references to messages active on this tile.
         * We can use this to speed-up searching for messages,
         * at the cost of memory.
         */
        public List<Message> messages;
        private Point position;
        public Tile(TileType ttype, Point pos)
        {
            position = pos;
            tileType = ttype;
            messages = new List<Message>();
        }
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int GetTexture()
        {
            return AHGraphics.GetTileTexture(this.TileType);
        }
    }
}
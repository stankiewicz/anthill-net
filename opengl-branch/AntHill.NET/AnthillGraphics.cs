using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using AntHill.NET.Properties;

namespace AntHill.NET
{
    static class AHGraphics
    {
        static Bitmap outdoorTile, indoorTile, wallTile;
        public static Bitmap rainBitmap, foodBitmap, messagesBitmap,
                             bmpQueen, bmpWarrior, bmpWorker, bmpSpider;
        
        public static int textureQueen = 1;
        public static int textureWorker = 2;
        public static int textureWarrior = 3;
        public static int textureSpider = 4;
        public static int textureOutdoor = 5;
        public static int textureWall = 6;
        public static int textureIndoor = 7;
        public static int textureRain = 8;
        public static int textureFood =9;
        public static int textureMessageQueenInDanger = 10;
        public static int textureMessageQueenIsHungry = 11;
        public static int textureMessageSpiderLocation = 12;
        public static int textureMessageFoodLocation =13;

        static public void Init()
        {
            Create32bBitmap(ref indoorTile, Path.Combine(Resources.GraphicsPath, Resources.indoorTileBmp));
            Create32bBitmap(ref outdoorTile, Path.Combine(Resources.GraphicsPath, Resources.outdoorTileBmp));
            Create32bBitmap(ref wallTile, Path.Combine(Resources.GraphicsPath, Resources.wallTileBmp));

            Create32bBitmap(ref rainBitmap, Path.Combine(Resources.GraphicsPath, Resources.rainBmp));
            Create32bBitmap(ref foodBitmap, Path.Combine(Resources.GraphicsPath, Resources.foodBmp));
            Create32bBitmap(ref messagesBitmap, Path.Combine(Resources.GraphicsPath, Resources.messagesBmp));
            //For unknown (new) objects:

            Create32bBitmap(ref bmpQueen, Path.Combine(Resources.GraphicsPath, Resources.antQueenBmp));
            Create32bBitmap(ref bmpWarrior, Path.Combine(Resources.GraphicsPath, Resources.antWarriorBmp));
            Create32bBitmap(ref bmpWorker, Path.Combine(Resources.GraphicsPath, Resources.antWorkerBmp));
            Create32bBitmap(ref bmpSpider, Path.Combine(Resources.GraphicsPath, Resources.spiderBmp));
        }

        private static void Create32bBitmap(ref Bitmap bmp, string filename)
        {
            Graphics g;
            Image img = Image.FromFile(filename);
            bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(img, 0, 0);
        }

        static public Bitmap GetTile(TileType tt)
        {
            switch (tt)
            {
                case TileType.Wall:
                    return wallTile;
                case TileType.Outdoor:
                    return outdoorTile;
                case TileType.Indoor:
                    return indoorTile;
                default:
                    return indoorTile;
            }
        }

        static public Bitmap GetCreature(CreatureType ct)
        {
            return bmpQueen;
        }

        static public Bitmap GetRainBitmap()
        {
            return rainBitmap;
        }

        static public Bitmap GetFoodBitmap()
        {
            return foodBitmap;
        }

        static public Bitmap GetMessagesBitmap()
        {
            return messagesBitmap;
        }

        static public CreatureType GetType(Creature c)
        {
            if (c is Worker) return CreatureType.worker;
            if (c is Warrior) return CreatureType.warrior;
            if (c is Spider) return CreatureType.spider;
            // Default:
            return CreatureType.queen;            
        }

        static public Bitmap GetElementBitmap(Element e)
        {
            if (e is Food) return foodBitmap;
            if (e is Creature) return GetCreature(GetType((Creature)e));
            if (e is Rain) return rainBitmap;
            return bmpQueen;
        }

        static public void DrawElement(Graphics g, Element e, float realTileSize, float offX, float offY)
        {
            if (e is Queen)
                if (((Queen)e).FoodQuantity > 0)
                    g.DrawImage(foodBitmap,
                        e.Position.X * realTileSize - offX,
                        e.Position.Y * realTileSize - offY,
                        realTileSize, realTileSize);

            if (!(e is Rain))
            {
                g.DrawImage(GetElementBitmap(e),
                        e.Position.X * realTileSize - offX,
                        e.Position.Y * realTileSize - offY,
                        realTileSize, realTileSize);
                return;
            }

            g.DrawImage(rainBitmap,
                    (e.Position.X - AntHillConfig.rainWidth / 2) * realTileSize - offX,
                    (e.Position.Y - AntHillConfig.rainWidth / 2) * realTileSize - offY,
                    AntHillConfig.rainWidth * realTileSize, AntHillConfig.rainWidth * realTileSize);
        }
    }
}

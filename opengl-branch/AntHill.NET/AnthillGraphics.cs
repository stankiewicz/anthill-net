using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using AntHill.NET.Properties;
using Tao.OpenGl;
using Tao.Platform;
using System.Drawing.Imaging;

namespace AntHill.NET
{
    static class AHGraphics
    {
        /*
        static Bitmap outdoorTile, indoorTile, wallTile,
                      rainBitmap, foodBitmap, messagesBitmap,
                      bmpQueen, bmpWarrior, bmpWorker, bmpSpider;
         */

        public enum Texture
        {
            Queen = 1, Worker, Warrior, Spider,
            Outdoor, Wall, Indoor, Rain,
            Food, MessageQueenInDanger, MessageQueenIsHungry,
            MessageSpiderLocation, MessageFoodLocation
        };

        static public void Init()
        {
            Create32bTexture(Texture.Indoor, Path.Combine(Resources.GraphicsPath, Resources.indoorTileBmp));
            Create32bTexture(Texture.Outdoor, Path.Combine(Resources.GraphicsPath, Resources.outdoorTileBmp));
            Create32bTexture(Texture.Wall, Path.Combine(Resources.GraphicsPath, Resources.wallTileBmp));

            Create32bTexture(Texture.Rain, Path.Combine(Resources.GraphicsPath, Resources.rainBmp));
            Create32bTexture(Texture.Food, Path.Combine(Resources.GraphicsPath, Resources.foodBmp));
            Create32bTexture(Texture.MessageFoodLocation, Path.Combine(Resources.GraphicsPath, Resources.bmpMessageFoodLocation));
            Create32bTexture(Texture.MessageSpiderLocation, Path.Combine(Resources.GraphicsPath, Resources.bmpMessageSpiderLocation));
            Create32bTexture(Texture.MessageQueenInDanger, Path.Combine(Resources.GraphicsPath, Resources.bmpMessageQueenInDanger));
            Create32bTexture(Texture.MessageQueenIsHungry, Path.Combine(Resources.GraphicsPath, Resources.bmpMessageQueenIsHungry));

            Create32bTexture(Texture.Queen, Path.Combine(Resources.GraphicsPath, Resources.antQueenBmp));
            Create32bTexture(Texture.Warrior, Path.Combine(Resources.GraphicsPath, Resources.antWarriorBmp));
            Create32bTexture(Texture.Worker, Path.Combine(Resources.GraphicsPath, Resources.antWorkerBmp));
            Create32bTexture(Texture.Spider, Path.Combine(Resources.GraphicsPath, Resources.spiderBmp));
        }

        private static void Create32bTexture(Texture t, string filename)
        {
            /*
            Graphics g;
            Image img = Image.FromFile(filename);
            bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(img, 0, 0);
             */
            Bitmap bitmap = new Bitmap(filename);

            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, (int)t);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA8, bitmap.Width, bitmap.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            bitmap.UnlockBits(bitmapData);
            bitmap.Dispose();           
        }

        static public CreatureType GetType(Creature c)
        {
            if (c is Worker) return CreatureType.worker;
            if (c is Warrior) return CreatureType.warrior;
            if (c is Spider) return CreatureType.spider;
            // Default:
            return CreatureType.queen;            
        }

        static public int GetElementTexture(Element e)
        {
            if (e is Food) return (int)Texture.Food;
            if (e is Creature) return GetCreatureTexture(GetType((Creature)e));
            if (e is Rain) return (int)Texture.Rain;
            return -1;
        }

        public static int GetCreatureTexture(CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.queen:
                    return (int)Texture.Queen;
                case CreatureType.warrior:
                    return (int)Texture.Warrior;
                case CreatureType.spider:
                    return (int)Texture.Spider;
                case CreatureType.worker:
                    return (int)Texture.Worker;
                default:
                    return -1;
            }
        }

        internal static int GetTile(TileType tileType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        internal static int GetTileTexture(TileType tileType)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

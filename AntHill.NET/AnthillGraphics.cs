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
        static Bitmap[,] creatureBitmaps;

        static public void Init()
        {
            //Create rectangular table creatures x directions
            creatureBitmaps = new Bitmap[Enum.GetValues(typeof(CreatureType)).Length,
                                         Enum.GetValues(typeof(Dir)).Length];
            indoorTile = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.indoorTileBmp));
            outdoorTile = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.outdoorTileBmp));
            wallTile = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.wallTileBmp));

            creatureBitmaps[(int)CreatureType.queen, (int)Dir.N] = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.antQueenBmp));
            creatureBitmaps[(int)CreatureType.warrior, (int)Dir.N] = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.antWarriorBmp));
            creatureBitmaps[(int)CreatureType.worker, (int)Dir.N] = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.antWorkerBmp));
            creatureBitmaps[(int)CreatureType.spider, (int)Dir.N] = new Bitmap(Path.Combine(Resources.GraphicsPath, Resources.spiderBmp));

            (creatureBitmaps[(int)CreatureType.queen, (int)Dir.E] = new Bitmap(creatureBitmaps[(int)CreatureType.queen, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (creatureBitmaps[(int)CreatureType.queen, (int)Dir.S] = new Bitmap(creatureBitmaps[(int)CreatureType.queen, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (creatureBitmaps[(int)CreatureType.queen, (int)Dir.W] = new Bitmap(creatureBitmaps[(int)CreatureType.queen, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate270FlipNone);

            (creatureBitmaps[(int)CreatureType.warrior, (int)Dir.E] = new Bitmap(creatureBitmaps[(int)CreatureType.warrior, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (creatureBitmaps[(int)CreatureType.warrior, (int)Dir.S] = new Bitmap(creatureBitmaps[(int)CreatureType.warrior, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (creatureBitmaps[(int)CreatureType.warrior, (int)Dir.W] = new Bitmap(creatureBitmaps[(int)CreatureType.warrior, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate270FlipNone);

            (creatureBitmaps[(int)CreatureType.worker, (int)Dir.E] = new Bitmap(creatureBitmaps[(int)CreatureType.worker, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (creatureBitmaps[(int)CreatureType.worker, (int)Dir.S] = new Bitmap(creatureBitmaps[(int)CreatureType.worker, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (creatureBitmaps[(int)CreatureType.worker, (int)Dir.W] = new Bitmap(creatureBitmaps[(int)CreatureType.worker, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate270FlipNone);

            (creatureBitmaps[(int)CreatureType.spider, (int)Dir.E] = new Bitmap(creatureBitmaps[(int)CreatureType.spider, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (creatureBitmaps[(int)CreatureType.spider, (int)Dir.S] = new Bitmap(creatureBitmaps[(int)CreatureType.spider, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (creatureBitmaps[(int)CreatureType.spider, (int)Dir.W] = new Bitmap(creatureBitmaps[(int)CreatureType.spider, (int)Dir.N])).RotateFlip(RotateFlipType.Rotate270FlipNone);
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

        static public Bitmap GetCreature(CreatureType ct, Dir d)
        {
            return creatureBitmaps[(int)ct, (int)d];
        }
    }
}

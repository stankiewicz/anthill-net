using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AntHill.NET
{
    static class AnthillGraphics
    {
        static public Bitmap ant= new Bitmap("Graphics/ant.png");

        static public Bitmap antQueenN;
        //static public Bitmap antQueenE = new Bitmap("Graphics/antQueen.png");
        static public Bitmap antQueenE;
        static public Bitmap antQueenS;
        static public Bitmap antQueenW;

        static public Bitmap antWarriorN;
        static public Bitmap antWarriorE;
        static public Bitmap antWarriorS;
        static public Bitmap antWarriorW;


        static public Bitmap antWorkerN;
        static public Bitmap antWorkerE;
        static public Bitmap antWorkerS;
        static public Bitmap antWorkerW;

        static public Bitmap spider;

        static public  void Init()
        {
            try
            {
                antQueenN = new Bitmap("Graphics/antQueen.png");
                antWarriorN = new Bitmap("Graphics/antWarrior.png");
                antWorkerN = new Bitmap("Graphics/antWorker.png");
                spider = new Bitmap("Graphics/spider.png");
            }
            catch (Exception e)
            {
                throw e;
            }

            (antQueenE = new Bitmap(antQueenN)).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (antQueenS = new Bitmap(antQueenN)).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (antQueenW = new Bitmap(antQueenN)).RotateFlip(RotateFlipType.Rotate270FlipNone);

            (antWarriorE = new Bitmap(antWarriorN)).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (antWarriorS = new Bitmap(antWarriorN)).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (antWarriorW = new Bitmap(antWarriorN)).RotateFlip(RotateFlipType.Rotate270FlipNone);

            (antWorkerE = new Bitmap(antWorkerN)).RotateFlip(RotateFlipType.Rotate90FlipNone);
            (antWorkerS = new Bitmap(antWorkerN)).RotateFlip(RotateFlipType.Rotate180FlipNone);
            (antWorkerW = new Bitmap(antWorkerN)).RotateFlip(RotateFlipType.Rotate270FlipNone);

        }
    }
}

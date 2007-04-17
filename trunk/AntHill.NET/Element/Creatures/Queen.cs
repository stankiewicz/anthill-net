using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Queen : Ant
    {
        public Queen(Point pos):base(pos)
        {
            Random rnd = new Random();
            Array a = Enum.GetValues(typeof(Dir));
            int i = rnd.Next(a.Length);
            this.Direction = (Dir)i;
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (!IsAlive())
            {
                //isw.DeleteAnt(this);
                return false;
            }
            Random rnd = new Random();
            if (rnd.NextDouble() > AntHillConfig.queenLayEggProbability)
            {
                isw.CreateEgg(this.Position);
            }
            if (this.TurnsToBecomeHungry == 0)
            {
                if (isw.GetVisibleFood(this).Count != 0)
                {
                    this.Eat();
                    this.TurnsToBecomeHungry = AntHillConfig.antTurnNumberToBecomeHungry;
                }
                else
                {
                    isw.CreateMessage(this.Position, MessageType.QueenIsHungry);
                }
            }
            if (isw.GetVisibleSpiders(this).Count != 0)
            {
                isw.CreateMessage(this.Position, MessageType.QueenInDanger);
            }
            return true;
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.queen, this.Direction);
        }
    }
}
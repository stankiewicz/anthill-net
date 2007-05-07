using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Queen : Ant
    {
        public Queen(Point pos):base(pos)
        {
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (!IsAlive())
            {
                return false;
            }
            if (Randomizer.NextDouble() > AntHillConfig.queenLayEggProbability)
            {
                isw.CreateEgg(this.Position);
            }
            if (this.TurnsToBecomeHungry == 0)
            {
                System.Collections.Generic.List<Food> food = isw.GetVisibleFood(this);
                if (food.Count != 0)
                {
                    this.Eat();
                    food[0].Maintain(isw);
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
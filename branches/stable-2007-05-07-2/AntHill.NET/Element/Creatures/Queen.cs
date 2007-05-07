using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Queen : Ant
    {
        public Queen(Point pos):base(pos)
        {
        }
        private int foodQuantity;

        public int FoodQuantity
        {
            get { return foodQuantity; }
            set { foodQuantity = value; }
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

                if (foodQuantity != 0)
                {
                    this.Eat();
                    foodQuantity--;
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
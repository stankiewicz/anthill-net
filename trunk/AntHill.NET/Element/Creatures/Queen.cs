using System;
using System.Drawing;

namespace AntHill.NET
{
    public class Queen : Ant
    {
        public Queen(Point pos):base(pos)
        {

        }
        public void LayEgg()
        {
        }

        public override void Maintain(ISimulationWorld isw)
        {//TODO nie do konca zrobiona logika
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
                    // wysylam sygnal ze glodny
                    // zaczynam odliczac do smierci
                }
            }
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
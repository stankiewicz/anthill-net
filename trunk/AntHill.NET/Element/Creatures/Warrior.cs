using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using astar;

namespace AntHill.NET
{
    class Warrior: Citizen
    {
        public Warrior(Point pos):base(pos)
        {
            
        }

        Spider GetNearestSpider(List<Spider> spiders)
        {
            int i=0;
            int min=Int32.MaxValue;
            int tmp;
            for (int j = 0; j < spiders.Count; j++)
			{
                if ((tmp = Distance(this.Position, spiders[i].Position)) < min)
                {
                    i = j;
                    min = tmp;
                }
			}
            return spiders[i];
        }

        

        public override bool Maintain(ISimulationWorld isw)
        {//TODO malo:)
            if (!base.IsAlive())
            {
                isw.DeleteAnt(this);
                return false;
            }
            SpreadSignal(isw);
            List<Spider> spiders;
            if ((spiders=isw.GetVisibleSpiders(this)).Count!=0)
            {
                randomMovemenCount = 0;
                Spider spider = GetNearestSpider(spiders);                
                MoveRotateOrAttack(this, spider, isw);
                return true;
            }
            // teraz wcinamy
            if (this.TurnsToBecomeHungry == 0)
            {
                randomMovemenCount = 0;
                List<Food> foods = isw.GetVisibleFood(this);
                if (foods.Count != 0)
                {
                    Food food = GetNearestFood(foods);
                    int distance = Distance(this.Position, food.Position);
                    if (distance == 0)
                    {
                        isw.DeleteFood(food);
                        this.Eat();
                        return true;
                    }
                    List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(food.Position.X, food.Position.Y), new AstarOtherObject());
                    if (trail == null)
                        return true;
                    if (trail.Count <= 1)
                        return true;
                    MoveOrRotate(trail[1]);
                }
                return true;
            }
            MoveRandomly(isw);            
            return true;
        }


        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.warrior, this.Direction);
        }
    }
}

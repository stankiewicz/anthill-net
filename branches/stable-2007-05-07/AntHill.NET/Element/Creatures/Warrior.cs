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

        int GetNearestSpider(List<Spider> spiders)
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
            return i;
        }

        int GetNearestFood(List<Food> foods)
        {
            int i = 0;
            int min = Int32.MaxValue;
            int tmp;
            for (int j = 0; j < foods.Count; j++)
            {
                if ((tmp = Distance(this.Position, foods[i].Position)) < min)
                {
                    i = j;
                    min = tmp;
                }
            }
            return i;
        }

        public override bool Maintain(ISimulationWorld isw)
        {//TODO malo:)
            if (!base.IsAlive())
            {
                isw.DeleteAnt(this);
                return false;
            }
            List<Spider> list;
            if ((list=isw.GetVisibleSpiders(this)).Count!=0)
            {
                int i = GetNearestSpider(list);
                int distance = Distance(this.Position, list[i].Position);
                if (distance == 0)
                {
                    isw.Attack(this,list[i]);
                }
                if (distance == 1)
                {
                    if (Position.X == list[i].Position.X)
                    {
                        if (Position.Y == list[i].Position.Y + 1) //ant 1 tile above
                        {
                            if (Direction == Dir.N)
                            {
                                isw.Attack(this, list[i]);

                                return true;
                            }
                            else
                            {
                                Direction = Dir.N;
                            }
                        }
                        else
                        {
                            if (Direction == Dir.S)
                            {
                                isw.Attack(this, list[i]);
                                return true;
                            }
                            else
                            {
                                Direction = Dir.S;
                            }
                        }
                    }
                    else
                    {
                        if (Position.X == list[i].Position.X + 1) //ant 1 tile left
                        {
                            if (Direction == Dir.W)
                            {
                                isw.Attack(this, list[i]);
                                return true;
                            }
                            else
                            {
                                Direction = Dir.W;
                            }
                        }
                        else
                        {
                            if (Direction == Dir.E)
                            {
                                isw.Attack(this, list[i]);
                                return true;
                            }
                            else
                            {
                                Direction = Dir.E;
                            }
                        }

                    }
                    return true;
                }
                if (distance > 1)
                {

                    List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(list[i].Position.X, list[i].Position.Y), new AstarOtherObject());
                    if (trail == null)
                        return true;
                    if (trail.Count <= 1)
                        return true;
                    MoveOrRotate(trail[1]);
                    return true;
                }
                return true;
            }
            // teraz wcinamy
            if (this.TurnsToBecomeHungry == 0)
            {
                List<Food> foods = isw.GetVisibleFood(this);
                if (foods.Count != 0)
                {
                    int nearest = GetNearestFood(foods);
                    int distance = Distance(this.Position, foods[nearest].Position);
                    if (distance == 0)
                    {
                        isw.DeleteFood(foods[nearest]);
                        this.Eat();
                        return true;
                    }
                    List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(foods[nearest].Position.X, foods[nearest].Position.Y), new AstarOtherObject());
                    if (trail == null)
                        return true;
                    if (trail.Count <= 1)
                        return true;
                    MoveOrRotate(trail[1]);
                }
            }

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

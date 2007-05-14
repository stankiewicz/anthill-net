using System;
using System.Drawing;
using System.Collections.Generic;
using astar;

namespace AntHill.NET
{
    public class Worker : Citizen
    {
        private int foodQuantity;
        public int FoodQuantity
        {
            get { return foodQuantity; }
            set { foodQuantity = value; }
        }

        public Worker(Point pos):base(pos) {}

        public void Dig(ISimulationWorld isw, Point pos)
        {
            isw.GetMap().DestroyWall(isw.GetMap().GetTile(pos.X, pos.Y));
        }

        public void LoadFood(ISimulationWorld isw, Food f)
        {
            isw.DeleteFood(f);
            foodQuantity += f.GetQuantity;
        }

        public override bool Maintain(ISimulationWorld isw)
        {
            if (!base.IsAlive())
            {
                isw.DeleteAnt(this);
                return false;
            }
            
            SpreadSignal(isw);
            List<Food> food;
            List<Message> msg = isw.GetVisibleMessages(this);
            for(int i=0; i<msg.Count; i++)
                this.AddToSet(msg[i], msg[i].GetPoint(this.Position).Intensity);

            if (this.TurnsToBecomeHungry <= 0)
                if (this.foodQuantity > 0)
                {
                    foodQuantity--;
                    Eat();
                }

            if (this.foodQuantity == 0) //search for food
            {
                path = null;
                food = isw.GetVisibleFood(this);

                if (food.Count != 0)
                {// idzie do jedzenia
                    Food nearestFood = this.GetNearestFood(food);
                    int dist = this.Distance(this.Position, nearestFood.Position);
                    if (dist == 0)
                    {
                        this.FoodQuantity = nearestFood.GetQuantity;
                        isw.DeleteFood(nearestFood);
                    }
                    else
                    {
                        isw.CreateMessage(this.Position, MessageType.FoodLocalization,nearestFood.Position);
                        // znajdujemy t¹ krótk¹ œcie¿kê - wyliczane co 'maintain'
                        List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(nearestFood.Position.X, nearestFood.Position.Y), new AstarOtherObject());
                        if (trail.Count >= 2)
                        {
                            MoveOrRotateOrDig(isw,trail[1]);
                            return true;
                        }
                    }
                }
                else
                {// nie widzi
                    Message m = ReadMessage(MessageType.FoodLocalization);
                    if (m != null)
                    {
                        // ma sygnal o najwiekszej intensywnosci
                        List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(m.Position.X, m.Position.Y), new AstarWorkerObject());
                        if (trail.Count >= 2)
                        {
                            MoveOrRotateOrDig(isw,trail[1]);
                            return true;
                        }
                    }
                }
            }
            else
            {
                int dist = this.Distance(this.Position, Simulation.simulation.queen.Position);
                if (dist == 0)
                {
                    isw.FeedQueen(this);
                    path = null;
                }
                else
                {
                    if (path == null || path.Count < 2)
                    {
                        path = Astar.Search(new KeyValuePair<int, int>(this.Position.X, this.Position.Y), new KeyValuePair<int, int>(Simulation.simulation.queen.Position.X, Simulation.simulation.queen.Position.Y), new AstarWorkerObject());
                    }
                    if (path.Count >= 2)
                    {
                        if (MoveOrRotateOrDig(isw,path[1]))
                            path.RemoveAt(0);
                        return true;
                    }
                }
            }

            MoveRandomly(isw);

            return true;
        }
        bool MoveOrRotateOrDig(ISimulationWorld isw,KeyValuePair<int, int> where)
        {// nie chce mi sie obrotu zrobic do kopania.. 
            Tile t =isw.GetMap().GetTile(where.Key, where.Value);
            if (t.TileType == TileType.Wall)
            {// destroy wall nie ma zabawy z regionami
                if(this.IsMoveOrRotate(where))isw.GetMap().DestroyWall(t);
                return false;
            }
            return MoveOrRotate(where);
        }

        public override void Destroy(ISimulationWorld isw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Bitmap GetBitmap()
        {
            return AHGraphics.GetCreature(CreatureType.worker, this.Direction);
        }

    }
}

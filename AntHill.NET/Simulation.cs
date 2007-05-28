using System;
using System.Collections.Generic;
using System.Drawing;
using astar;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        //public event EventHandler afterTurn = null;

        public static Simulation singletonInstance = null;

        public static bool Init(Map map)
        {
            try
            {
                if (singletonInstance == null)
                    singletonInstance = new Simulation(map);
            }
            catch (Exception exc) //Simulation(map) can throw an exception
            {
                throw exc;
            }

            Astar.Init(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);

            return true;
        }

        public static void DeInit()
        {
            singletonInstance = null;
        }

        public static Simulation simulation
        {
            get { return singletonInstance; }
        }
        int turn = 0;
        private Map map;
        public List<Egg> eggs;
        public List<Message> messages;
        public List<Food> food;
        public List<Spider> spiders;
        public List<Ant> ants;
        public Rain rain;
        public Queen queen;

        private void Initialize()
        {
            map = null;
            eggs = new List<Egg>();
            eggs.Clear();
            messages = new List<Message>();
            messages.Clear();
            food = new List<Food>();
            food.Clear();
            spiders = new List<Spider>();
            spiders.Clear();
            ants = new List<Ant>();
            ants.Clear();
            rain = null;
            queen = new Queen(new Point(AntHillConfig.queenXPosition, AntHillConfig.queenYPosition));
        }

	    public Simulation(Map map)
        {
            Initialize();

            if (map.GetIndoorCount == 0)
                throw new Exception(Properties.Resources.noIndoorTilesError);
            if (map.GetOutdoorCount == 0)
                throw new Exception(Properties.Resources.noOutdoorTilesError);

            this.map = map;

            for (int i = AntHillConfig.workerStartCount; i > 0; i--)
                this.CreateWorker(Map.GetRandomTile(TileType.Indoor).Position);

            for (int i = AntHillConfig.warriorStartCount; i > 0; i--)
                this.CreateWarrior(Map.GetRandomTile(TileType.Indoor).Position);
        }

        public Map Map
        {
            get { return map; }
        }

        bool CheckIfExists(TileType tt)
        {
            for (int i = 0; i < Map.Height; i++)
                for (int j = 0; j < Map.Width; j++)
                    if (Map.GetTile(j, i).TileType == tt)
                        return true;

            return false;
        }

        Point GetRandomPoint()
        {
            return new Point(Randomizer.Next(Map.Width), Randomizer.Next(Map.Height));
        }

	    #region ISimulation Members

        /// <summary>
        /// This is the most important function - activity diagram
        /// </summary>
        bool ISimulationUser.DoTurn()
        {
            if (queen == null) return false;
            turn++;
            if (Randomizer.NextDouble() <= AntHillConfig.spiderProbability)
                    this.CreateSpider(Map.GetRandomTile(TileType.Outdoor).Position);

            if (Randomizer.NextDouble() <= AntHillConfig.foodProbability)
                this.CreateFood(Map.GetRandomTile(TileType.Outdoor).Position, GetRandomFoodQuantity());

            if ((rain == null) && (Randomizer.NextDouble() <= AntHillConfig.rainProbability))
                this.CreateRain(GetRandomPoint());

            if (rain != null)
                rain.Maintain(this);

            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].Maintain(this);
                if (messages[i].Empty)
                {
                    messages.RemoveAt(i);
                    --i;
                }
            }

            for (int i = 0; i < ants.Count; i++)
            {
                if (!ants[i].Maintain(this))
                {
                    --i;
                }
            }

            for (int i = 0; i < spiders.Count; i++)
            {
                if (!spiders[i].Maintain(this))
                    --i;
            }

            for (int i = 0; i < eggs.Count; ++i)
            {
                if (!eggs[i].Maintain(this))
                    --i;
            }
            if (queen!=null && !queen.Maintain(this))
            {
                queen = null;
                return false;
            }
            return true;
        }

        void ISimulationUser.Reset()
        {
            Initialize();
            turn = 0;
        }

        void ISimulationUser.Start()
        {

        }

        void ISimulationUser.Stop()
        {
            
        }

        #endregion

        #region ISimulationWorld Members

        private bool CreateRain(Point point)
        {
            rain = new Rain(point);
            return true;
        }

        public bool CreateFood(Point point, int quantity)
        {
            food.Add(new Food(point, quantity));
            return true;
        }

        public bool CreateSpider(Point point)
        {
            spiders.Add(new Spider(point));
            return true;
        }


        public bool CreateWarrior(Point pos)
        {
            ants.Add(new Warrior(pos));
            return true;
        }

        public bool CreateWorker(Point pos)
        {
            ants.Add(new Worker(pos));
            return true;
        }

        public bool CreateSpider()
        {
            spiders.Add(new Spider(Map.GetRandomTile(TileType.Outdoor).Position));
            return true;
        }


        //returns true if cD is killed
        public bool Attack(Creature cA, Creature cD)
        {
            if (cA is Spider)
            {
                if (cD is Queen) queen = null;
                else if (cD is Ant)
                    this.DeleteAnt((Ant)cD);

                return true;
            }
            else if (cA is Warrior && cD is Spider)
            {
                try
                {
                    ((Spider)cD).Health -= AntHillConfig.antStrength;
                }
                catch (Exception)
                {
                    this.DeleteSpider((Spider)cD);
                    return true;
                }
            }

            return false;
        }

        private bool IsVisible(Point p1, Point p2)
        {
            //not ready
            return true;
        }

        private bool IsInRect(Point pt, Point pos, int width, int height)
        {
            //not ready
            return true;
        }

        public List<Ant> GetVisibleAnts(Element c)
        {
            List<Ant> res_ants = new List<Ant>();
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                for (int i = 0; i < ants.Count; i++)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(ants[i].Position.X - c.Position.X) <= radius &&
                        Math.Abs(ants[i].Position.Y - c.Position.Y) <= radius)
                        res_ants.Add(ants[i]);
                }
            }
            else if (c is Rain)
            {
                for (int i = 0; i < ants.Count; i++)
                {
                    if (map.GetTile(ants[i].Position.X, ants[i].Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(ants[i].Position.X, ants[i].Position.Y))
                        res_ants.Add(ants[i]);
                }
            }
                
            return res_ants;
        }

        public List<Food> GetVisibleFood(Element c)
        {
            List<Food> res_food = new List<Food>();
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                for (int i = 0; i < food.Count; i++)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(food[i].Position.X - c.Position.X) <= radius &&
                        Math.Abs(food[i].Position.Y - c.Position.Y) <= radius)
                        res_food.Add(food[i]);
                }
            }
            else if (c is Rain)
            {
                for (int i = 0; i < food.Count; i++)
                {
                    if (map.GetTile(food[i].Position.X, food[i].Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(food[i].Position.X, food[i].Position.Y))
                        res_food.Add(food[i]);
                }
            }
            return res_food;
        }

        public List<Spider> GetVisibleSpiders(Element c)
        {
            List<Spider> res_spiders = new List<Spider>();
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                for (int i = 0; i < spiders.Count; i++)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(spiders[i].Position.X - c.Position.X) <= radius &&
                        Math.Abs(spiders[i].Position.Y - c.Position.Y) <= radius)
                        res_spiders.Add(spiders[i]);
                }
            }
            else if (c is Rain)
            {
                for (int i = 0; i < spiders.Count; i++)
                {
                    if (map.GetTile(spiders[i].Position.X, spiders[i].Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(spiders[i].Position.X, spiders[i].Position.Y))
                        res_spiders.Add(spiders[i]);
                }
            }

            return res_spiders;
        }

        public List<Message> GetVisibleMessages(Element c)
        {
            List<Message> res_messages =  new List<Message>();
            if (c is Ant)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    for (int j = 0; j < messages[i].Points.Count; j++)
                    {
                        if (c.Position == messages[i].Points[j].Tile.Position)
                        {
                            res_messages.Add(messages[i]);
                            break;
                        }
                    }
                }
            }
            else if (c is Rain)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    for (int j = 0; j < messages[i].Points.Count; j++)
                    {
                        if (messages[i].Points[j].Tile.TileType == TileType.Outdoor &&
                            c.Position == messages[i].Points[j].Tile.Position)
                        {
                            res_messages.Add(messages[i]);
                            break;
                        }
                    }
                }
            }

            return res_messages;
        }

        public bool FeedQueen(Worker w)
        {
            if (w.Position != queen.Position) return false;
            queen.FoodQuantity += w.FoodQuantity;
            w.FoodQuantity = 0;
            return true;
        }

        public bool CreateAnt(System.Drawing.Point position)
        {
            if (Randomizer.NextDouble() < AntHillConfig.eggHatchWarriorProbability)
                ants.Add(new Warrior(position));
            else
                ants.Add(new Worker(position));

            return true;
        }

        public bool DeleteEgg(Egg egg)
        {
            eggs.Remove(egg);
            return true;
        }

        public bool DeleteRain()
        {
            rain = null;
            return true;
        }

        public bool CreateEgg(Point pos)
        {
            eggs.Add(new Egg(pos));
            return true;
        }

        public bool DeleteFood(Food food)
        {
            this.food.Remove(food);
            return true;
        }

        public bool DeleteAnt(Ant ant)
        {
            this.food.Add(new Food(ant.Position, AntHillConfig.antFoodQuantityAfterDeath));
            ants.Remove(ant);
            return true;
        }

        public bool DeleteSpider(Spider spider)
        {
            this.food.Add(new Food(spider.Position, AntHillConfig.spiderFoodQuantityAfterDeath));
            spiders.Remove(spider);
            return true;
        }

        public Map GetMap()
        {
            return map;
        }

        
        public bool CreateMessage(Point pos, MessageType mt, Point location)
        {
            Message ms = new Message(pos, mt,location);
            for (int i = -AntHillConfig.messageRadius; i < AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j < AntHillConfig.messageRadius; j++)
                {
                    if (i * i + j * j < AntHillConfig.messageRadius * AntHillConfig.messageRadius)
                    {
                        if (map.Inside(i+pos.X, j+pos.Y))
                        {
                            if(map.GetTile(i+pos.X,j+pos.Y).TileType!= TileType.Wall)
                            ms.AddPoint(map.GetTile(i + pos.X, j + pos.Y), AntHillConfig.messageLifeTime, map);
                        }
                    }
                }
            }
            this.messages.Add(ms);

            return true;
        }

        private int GetRandomFoodQuantity()
        {
            return Randomizer.Next(AntHillConfig.foodRandomMaxQuantity) + 1;
        }

        #endregion

        #region ISimulationUser Members


        public int GetNAnts()
        {
            return (ants!=null)?ants.Count:0;
        }

        public int GetNSignals()
        {
            return (messages!=null)?messages.Count:0;
        }

        public int GetNTurns()
        {
            return turn;
        }

        public int GetNSpiders()
        {
            return (spiders!=null)?spiders.Count:0;
        }

        public bool testCheckIfExists(TileType tt)
        {
            return this.CheckIfExists(tt);
        }

        #endregion
    }
}

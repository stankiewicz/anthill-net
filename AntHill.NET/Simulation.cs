using System;
using System.Collections.Generic;
using System.Drawing;
using astar;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        public event EventHandler afterTurn = null;

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
            messages = new List<Message>();
            food = new List<Food>();
            spiders = new List<Spider>();
            ants = new List<Ant>();
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
            {
                this.CreateWorker(Map.GetRandomTile(TileType.Indoor).Position);
            }
            for (int i = AntHillConfig.warriorStartCount; i > 0; i--)
            {
                this.CreateWarrior(Map.GetRandomTile(TileType.Indoor).Position);
            }
        }

        public Map Map
        {
            get { return map; }
        }

        bool CheckIfExists(TileType tt)
        {
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    if (Map.GetTile(j, i).TileType == tt) return true;
                }
            }
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
            try
            {
                if (Randomizer.NextDouble() >= AntHillConfig.spiderProbability)
                    this.CreateSpider(Map.GetRandomTile(TileType.Outdoor).Position);
            }
            catch (Exception)
            {

            }

            try
            {
                if (Randomizer.NextDouble() >= AntHillConfig.foodProbability)
                    this.CreateFood(Map.GetRandomTile(TileType.Outdoor).Position, GetRandomFoodQuantity());
            }
            catch (Exception)
            {
                
            }

            if ((rain == null) && (Randomizer.NextDouble() >= AntHillConfig.rainProbability))
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

        private void CreateRain(Point point)
        {
            rain = new Rain(point);
        }

        public void CreateFood(Point point, int quantity)
        {
            food.Add(new Food(point, quantity));
        }

        public void CreateSpider(Point point)
        {
            spiders.Add(new Spider(point));
        }

        void ISimulationUser.Reset()
        {
            Initialize();
        }

        void ISimulationUser.Start()
        {

        }

        void ISimulationUser.Stop()
        {
            
        }

        #endregion

        #region ISimulationWorld Members

        public void CreateAnt()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void CreateWarrior(Point pos)
        {
            ants.Add(new Warrior(pos));
        }

        public void CreateWorker(Point pos)
        {
            ants.Add(new Worker(pos));
        }

        public void CreateSpider()
        {
            spiders.Add(new Spider(Map.GetRandomTile(TileType.Outdoor).Position));
        }

        public void CreateFood()
        {
            //throw new Exception("The method or operation is not implemented.");
        }
        
        public void Attack(Creature cA, Creature cB)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public List<Ant> GetVisibleAnts(Element c)
        {
            return new List<Ant>();
        }

        public List<Food> GetVisibleFood(Element c)
        {
            return new List<Food>();
        }

        public List<Spider> GetVisibleSpiders(Element c)
        {
            return new List<Spider>();
        }

        public List<Message> GetVisibleMessages(Element c)
        {
            return new List<Message>();
        }

        public void CreateAnt(System.Drawing.Point position)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteEgg(Egg egg)
        {
            eggs.Remove(egg);
        }

        public void DeleteRain()
        {
            rain = null;
        }

        public void CreateEgg(Point pos)
        {
            eggs.Add(new Egg(pos));
        }

        public void DeleteFood(Food food)
        {
            this.food.Remove(food);
        }

        public void DeleteAnt(Ant ant)
        {
            ants.Remove(ant);
        }

        public void DeleteSpider(Spider spider)
        {
            spiders.Remove(spider);
        }

        public Map GetMap()
        {
            return map;
        }

        public void CreateMessage(Point pos, MessageType mt)
        {
            Message ms = new Message(pos, mt);
            for (int i = -AntHillConfig.messageRadius; i < AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j < AntHillConfig.messageRadius; j++)
                {
                    if (i * i + j * j < AntHillConfig.messageRadius * AntHillConfig.messageRadius)
                    {
                        if (map.Inside(i, j))
                        {
                            ms.points.Add(new PointWithIntensity(map.GetTile(i, j), AntHillConfig.messageLifeTime));
                        }
                    }
                }

            }
            this.messages.Add(ms);
        }

        private int GetRandomFoodQuantity()
        {
            return Randomizer.Next(20);
        }

        #endregion
    }
}
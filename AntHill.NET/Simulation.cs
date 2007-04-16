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

        public static void Init(Map map)
        {
            if(singletonInstance == null)
                singletonInstance = new Simulation(map);
            Astar.Init(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);
        }

        public static void DeInit()
        {
            singletonInstance = null;
        }

        public static Simulation simulation
        {
            get { return singletonInstance; }
        }

        volatile bool pause;

        private Map map;
        public List<Egg> eggs;
        public List<Message> messages;
        public List<Food> food;
        public List<Spider> spiders;
        public List<Ant> ants;
        public Rain rain;
        public Queen queen;

        //These are used to update list<[elem]> after an item has been deleted
        bool isEggDeleted = false;
        bool isAntDeleted = false;
        bool isSpiderDeleted = false;

        private void Initialize()
        {
            pause = false;

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

            this.map = map;

            for (int i = AntHillConfig.workerStartCount; i > 0; i--)
            {
                //this.CreateAnt
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

        Point GetTile(TileType tt)
        {
            Random rnd = new Random();
            Tile t;
            // modlmy sie ze znajdzie szybko
            if (CheckIfExists(tt) == false) throw new Exception("no more " + tt.ToString() + " on map");
            while (true)
            {
                if ((t = map.GetTile(rnd.Next(map.Width), rnd.Next(map.Height))).TileType == tt)
                    return t.Position;
            }
        }

        Point GetRandomPoint()
        {
            Random rnd = new Random();
            return new Point(rnd.Next(map.Width), rnd.Next(map.Height));
        }

	    #region ISimulation Members

        /// <summary>
        /// This is the most important function - activity diagram
        /// </summary>
        void ISimulationUser.DoTurn()
        {
            Random rnd = new Random();
            try
            {
                if (rnd.NextDouble() >= AntHillConfig.spiderProbability)
                    this.CreateSpider(GetTile(TileType.Outdoor));
            }
            catch (Exception)
            {

            }

            try
            {
                if (rnd.NextDouble() >= AntHillConfig.foodProbability)
                    this.CreateFood(GetTile(TileType.Outdoor), GetRandomFoodQuantity());
            }
            catch (Exception)
            {
                
            }
            
            if ( (rain == null) && (rnd.NextDouble() >= AntHillConfig.rainProbability))
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
                ants[i].Maintain(this);
                if (isAntDeleted)
                {
                    --i;
                    isAntDeleted = false;
                }
            }

            for (int i = 0; i < spiders.Count; i++)
            {
                spiders[i].Maintain(this);
                if (isSpiderDeleted)
                {
                    --i;
                    isSpiderDeleted = false;
                }
            }

            for (int i = 0; i < eggs.Count; ++i)
            {
                eggs[i].Maintain(this);
                if (isEggDeleted)
                {
                    --i;
                    isEggDeleted = false;
                }
            }
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
            pause = false;

            while (!pause)
            {
                ((ISimulationUser)this).DoTurn();
                if (this.afterTurn != null)
                    afterTurn(null, null);
            }
        }

        void ISimulationUser.Pause()
        {
            pause = true;
        }

        #endregion

        #region ISimulationWorld Members

        public void CreateAnt()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void CreateWarrior(Point pos)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void CreateWorker(Point pos)
        {
            //throw
        }

        public void CreateSpider()
        {
            //throw new Exception("The method or operation is not implemented.");
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
            //throw new Exception("The method or operation is not implemented.");
            return null;
        }

        public List<Food> GetVisibleFood(Element c)
        {
            //throw new Exception("The method or operation is not implemented.");
            return null;
        }

        public List<Spider> GetVisibleSpiders(Element c)
        {
            //throw new Exception("The method or operation is not implemented.");
            return null;
        }

        public List<Message> GetVisibleMessages(Element c)
        {
            //throw new Exception("The method or operation is not implemented.");
            return null;
        }

        public void CreateAnt(System.Drawing.Point position)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteEgg(Egg egg)
        {
            isEggDeleted = true;
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
            //throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteAnt(Ant ant)
        {
           // throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteSpider(Spider spider)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public Map GetMap()
        {
            return map;
        }

        #endregion

        private int GetRandomFoodQuantity()
        {
            return new Random().Next(20);
        }

        #region ISimulationWorld Members


        public void CreateMessage(Point pos, MessageType mt)
        {
            Message ms = new Message(pos, mt);
            for (int i = -AntHillConfig.messageRadius; i < AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j <  AntHillConfig.messageRadius; j++)
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

        #endregion
    }
}
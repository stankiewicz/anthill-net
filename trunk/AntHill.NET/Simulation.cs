using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        public static Simulation singletonInstance = null;
        public static void Init(Map map)
        {
            if(singletonInstance == null)
                singletonInstance = new Simulation(map);
        }
        public static void DeInit()
        {
            singletonInstance = null;
        }
        public static Simulation simulation
        {
            get
            {
                return singletonInstance;
            }
        }

        private Map map;

        public List<Egg> eggs = new List<Egg>();
        public List<Message> messages = new List<Message>();
        public List<Food> food = new List<Food>();
        public List<Spider> spiders = new List<Spider>();
        public List<Ant> ants = new List<Ant>();
        public Rain rain;
        public Queen queen = null;
        bool isEggDeleted = false;
        bool isAntDeleted = false;
        bool isSpiderDeleted = false;

	    public Simulation(Map map)
        {
            this.map = map;
        }

        public Map Map
        {
            get { return map; }
        }

        Point GetTile(TileType tt)
        {
            Random rnd = new Random();
            Tile t;
            // modlmy sie ze znajdzie szybko
            while (true)
            {
                if ((t = map.GetTile(rnd.Next(map.GetWidth), rnd.Next(map.GetHeight))).GetTileType == tt)
                    return t.Position;
            }
        }

        Point GetRandomTile()
        {
            Random rnd = new Random();
            return new Point(rnd.Next(map.GetWidth), rnd.Next(map.GetHeight));
        }

	    #region ISimulation Members


        /// <summary>
        /// This is the most important function - activity diagram
        /// </summary>
        void ISimulationUser.DoTurn()
        {
            //throw new Exception("The method or operation is not implemented.");
            Random rnd = new Random();
            if (rnd.NextDouble() >= AntHillConfig.spiderProbability)
            {
                this.CreateSpider(GetTile(TileType.Outdoor));
            }
            
            if (rnd.NextDouble() >= AntHillConfig.foodProbability)
            {
                this.CreateFood(GetTile(TileType.Outdoor));
            }
            
            if (rain==null && rnd.NextDouble() >= AntHillConfig.rainProbability)
            {
                this.CreateRain(GetRandomTile());
            }

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
                if (isAntDeleted) { --i; isAntDeleted = false; }
            }

            for (int i = 0; i < spiders.Count; i++)
            {
                spiders[i].Maintain(this);
                if (isSpiderDeleted) { --i; isSpiderDeleted = false; }
            }

            for (int i = 0; i < eggs.Count; ++i)
            {
                eggs[i].Maintain(this);
                if (isEggDeleted) { --i; isEggDeleted = false; }
            }

        }

        

        private void CreateRain(Point point)
        {
            rain = new Rain(point);
        }

        private void CreateFood(Point point)
        {
            food.Add(new Food(point));
        }

        private void CreateSpider(Point point)
        {
            spiders.Add(new Spider(point));
        }

        void ISimulationUser.Reset()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void ISimulationUser.Start()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void ISimulationUser.Pause()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ISimulationWorld Members

        public void CreateAnt()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CreateSpider()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CreateFood()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CreateMessage()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Attack(Creature cA, Creature cB)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Ant> GetVisibleAnts(Element c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Food> GetVisibleFood(Element c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Spider> GetVisibleSpiders(Element c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Message> GetVisibleMessages(Element c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ISimulationWorld Members

        public void CreateAnt(System.Drawing.Point position)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteEgg(Egg egg)
        {
            isEggDeleted = true;
        }

        #endregion

        #region ISimulationWorld Members


        public void DeleteRain()
        {
            rain = null;
        }

        #endregion

        #region ISimulationWorld Members


        public void CreateEgg(Point pos)
        {
            eggs.Add(new Egg(pos));
        }


        public void DeleteFood(Food food)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteAnt(Ant ant)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteSpider(Spider spider)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion


        #region ISimulationWorld Members


        
        #endregion

       

        #region ISimulationWorld Members


        public Map GetMap()
        {
            return map;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        private Map map;

        public List<Message> messages = new List<Message>();
        public List<Food> food = new List<Food>();
        public List<Spider> spiders = new List<Spider>(), tempSpiders;
        public List<Ant> ants = new List<Ant>(), tempAnts;
        public Rain rain;
        public Queen queen = null;

	    public Simulation(Map map)
        {
            this.map = map;
        }

        public Map GetMap
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
            if (rnd.Next(101) >= AntHillConfig.spiderProbability)
            {
                this.CreateSpider(GetTile(TileType.Outdoor));
            }
            if (rnd.Next(101) >= AntHillConfig.foodProbability)
            {

                this.CreateFood(GetTile(TileType.Outdoor));
            }
            if (rnd==null && rnd.Next(101) >= AntHillConfig.rainProbability)
            {
                this.CreateRain(GetRandomTile());
            }

            tempAnts.Clear();
            tempSpiders.Clear();
            // tworzenie list
            // optymalizacja!!!!!
            for (int i = 0; i < ants.Count; i++)
            {
                tempAnts.Add(ants[i]);
            }
            for (int i = 0; i < spiders.Count; i++)
            {
                tempSpiders.Add(spiders[i]);
            }

            
            while (tempAnts.Count != 0 || tempSpiders.Count != 0)
            {
                Creature creature=null;
                // wybieramy jedno lub 2gie.
                if (tempSpiders.Count == 0 || rnd.Next(1)==0)
                {
                    creature = tempAnts[0];
                    tempAnts.RemoveAt(0);
                }
                else
                {
                    creature = tempSpiders[0];
                    tempSpiders.RemoveAt(0);
                }

                creature.Maintain(this);


            }

            

        }

        

        private void CreateRain(Point point)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void CreateFood(Point point)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void CreateSpider(Point point)
        {
            throw new Exception("The method or operation is not implemented.");
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
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ISimulationWorld Members


        public void DeleteRain()
        {
            rain = null;
        }

        #endregion
    }
}
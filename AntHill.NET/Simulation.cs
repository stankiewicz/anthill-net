using System;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        private Map map;

        public List<Message> messages = new List<Message>();
        public List<Food> food = new List<Food>();
        public List<Spider> spiders = new List<Spider>();
        public List<Ant> ants = new List<Ant>();
        
        public Queen queen = null;

	    public Simulation(Map map)
        {
            this.map = map;
        }

        public Map GetMap
        {
            get { return map; }
        }
    	
	    #region ISimulation Members


        /// <summary>
        /// This is the most important function 
        /// </summary>
        void ISimulationUser.DoTurn()
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

        public List<Ant> GetVisibleAnts(Creature c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Food> GetVisibleFood(Creature c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Spider> GetVisibleSpiders(Creature c)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Message> GetVisibleMessages(Creature c)
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
    }
}
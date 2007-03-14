using System;
using System.Collections.Generic;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        private Map map;

        //We should add List<Food>, List<Spider>, List<Ant> here
        public List<Message> messages = new List<Message>();
        
        public Queen queen = null;

	    public Simulation(Map map)
        {
            this.map = map;
        }

        public Map GetMap
        {
            get { return map; }
        }

        public void AddMessage(List<Message> newMessage) { }
        public void AddElement(List<Element> newElement) { }
    	
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

        #endregion
    }
}
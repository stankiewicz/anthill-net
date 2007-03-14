using System;
using System.Collections.Generic;

public class Simulation : ISimulation
{
    private Map map;

    //We should add List<Food>, List<Spider>, List<Ant> here
    public List<Message> messages = new List<Message>();
    public List<Element> elements = new List<Element>();
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

    void ISimulation.DoTurn()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    void ISimulation.Reset()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    void ISimulation.Start()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    void ISimulation.Pause()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #endregion
}
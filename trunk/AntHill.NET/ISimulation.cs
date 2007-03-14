using System;

public interface ISimulation
{
	void  DoTurn();
	void  Reset();
	void  Start();
	void  Pause();
}
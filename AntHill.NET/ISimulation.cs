using System;

public interface ISimulationUser
{
	void  DoTurn();
	void  Reset();
	void  Start();
	void  Pause();
}
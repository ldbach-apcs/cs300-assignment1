using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuestInput 
{
	List<IQuestObserver> observers = new List<IQuestObserver>();

	public void Register(IQuestObserver observer) 
	{
		if (!observers.Contains(observer)) 
		{
			observers.Add(observer);
		}
	}

	protected void Notify(QuestInputData data) 
	{
		foreach (IQuestObserver observer in observers) 
		{
			observer.OnQuestInput(data);	
		}
	}

	public virtual void Init() {

	}

	public virtual void Destroy() {
		
	}
}

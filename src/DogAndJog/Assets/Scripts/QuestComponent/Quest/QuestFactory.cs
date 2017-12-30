using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class QuestFactory {
	private DatabaseReader dbReader;
	private static readonly QuestFactory instance = new QuestFactory();
	private QuestFactory()
	{ 
		dbReader = DatabaseReader.Instance();
	}

	public static QuestFactory Instance()
	{
		return instance;
	}

	public IQuest ParseQuest(System.Data.IDataReader reader) {
		dbReader = DatabaseReader.Instance();
		IQuest currentQuest = null;
		
		string name = reader.GetString(0);
		string description = reader.GetString(1);
		int type = reader.GetInt32(2);
		int rewardExp = reader.GetInt32(3);
		int rewardMoney = reader.GetInt16(4);
		double prevValue = reader.GetDouble(5);
		double requireValue = reader.GetDouble(6);

		switch (type) {
			case 0: // Distance 
				double totalDistance = dbReader.totalDistance;
				currentQuest = new DistanceQuest
								(name, description, 
								 requireValue, prevValue,
								 rewardExp, rewardMoney);
				break;
			case 1: // Share
				int totalShare = dbReader.totalShare;
				currentQuest = new FacebookQuest(
								name, description,
								(int) prevValue, (int) requireValue,
								rewardExp, rewardMoney);
				break;
			default:
				break;
		}

		
		return currentQuest;
	}

	/*
	 * This function returns a random quest
	 */ 
	public IQuest GetQuest()
	{
		// Future overloading: add level or affection point
		// to create scaling  quest

		// For now, return a new distance quest
		IQuest newQuest = null;

		// Get current share number and distancce
		int currentShare = DatabaseReader.Instance().totalShare;
		float currentDistance = (float) DatabaseReader.Instance().totalDistance;
	
		int randomQuest = Random.Range(0, 4);
		if (randomQuest == 1) 
			newQuest = new DistanceQuest(
			"Running never boring", "Dog is a breed full of energy, they can run on and on for many hours", 
			100.0f, currentDistance, 10, 100);
		else if (randomQuest == 2)
			return new DistanceQuest("Greeting season", "A dog park is opened nearby, your pet got so excited"
			+ ", please walk him there", 200.0f, currentDistance, 50, 50);
		else if (randomQuest == 3) {
			newQuest =  new FacebookQuest("New to the neighborhood", 
			"It's always a good courtersy to say hello to your neighbor", currentShare, 1, 10, 15);
		} else {
			newQuest =  new FacebookQuest("Let's make some friend", "Your pet is lonely", currentShare, 2, 20, 30);
		}

		if (newQuest != null)
			dbReader.SaveQuest(newQuest);
		return newQuest;
	}
}

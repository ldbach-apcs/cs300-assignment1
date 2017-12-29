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
	
		/*
		if (Random.Range(0, 1) == 0) 
			return new DistanceQuest("Let's run away", "Your pet have seen a luxurious house in town"
			+ " and he is very excited that he ran there. Go after him, because he is in need of a snack!", 500.0f);
		else 
			return new DistanceQuest("Greeting season", "A dog park is opened nearby, your pet got so excited"
			+ ", please walk him there", 200.0f);
		*/


		// Get current share number
		int currentShare = PlayerPrefs.GetInt(FacebookQuestInput.PREV_SHARE);
		IQuest newQuest =  new FacebookQuest("Let's make some friend", "Your pet is lonely", currentShare, 3, 1, 1);

		dbReader.SaveQuest(newQuest);
		return newQuest;
	}
}

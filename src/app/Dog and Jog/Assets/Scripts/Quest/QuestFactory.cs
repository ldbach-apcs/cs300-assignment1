using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class QuestFactory {
	private static readonly QuestFactory instance = new QuestFactory();
	private QuestFactory()
	{ }

	public static QuestFactory Instance()
	{
		return instance;
	}


	/*
	 * This function returns a random quest
	 */ 
	public IQuest GetQuest()
	{
		// Future overloading: add level or affection point
		// to create scaling  quest

		// For now, return a new distance quest
		if (Random.Range(0, 1) == 0) 
			return new DistanceQuest("Let's run away", "Your pet have seen a luxurious house in town"
			+ " and he is very excited that he ran there. Go after him, because he is in need of a snack!", 500.0f);
		else 
			return new DistanceQuest("Greeting season", "A dog park is opened nearby, your pet got so excited"
			+ ", please walk him there", 200.0f);
	}


}

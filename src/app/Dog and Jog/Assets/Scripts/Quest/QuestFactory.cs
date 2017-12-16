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
		return new DistanceQuest("Let's run away", "Run for your life", 500.0f);
	}


}

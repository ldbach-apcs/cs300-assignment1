using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager 
{
	List<IQuest> achievements;

	public AchievementManager() 
	{
		achievements = LoadAchievements ();
	}


	/* 
	 * Call the database to load the list of achievement
	 */ 
	List<IQuest> LoadAchievements()
	{
		List<IQuest> list = new List<IQuest>();
		list.Add (new DistanceQuest ("random name", "random description", 50.0));
		return list;
	}
}

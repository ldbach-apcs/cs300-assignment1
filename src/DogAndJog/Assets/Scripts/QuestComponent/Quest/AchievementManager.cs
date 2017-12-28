using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager 
{
	List<Achievement> achievements;

	public AchievementManager() 
	{
		achievements = LoadAchievements ();
		
	}


	/* 
	 * Call the database to load the list of achievement
	 */ 
	List<Achievement> LoadAchievements()
	{
		DatabaseReader dbReader = DatabaseReader.Instance();
		return dbReader.ReadAchievements();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This class is responsible for passing data around */
public class QuestInputData 
{
	private string key;
	private Dictionary<string, double> valueMap = new Dictionary<string, double>();

	public QuestInputData(string key)
	{
		this.key = key;
	}

	public double GetValue(string query)
	{
		return valueMap.ContainsKey(query) ? valueMap[query] : 0;
	}

	public void PutValue(double val) 
	{
		if (val < 0) 
		{
			return;
		}
		valueMap [key] = val;
	}
}

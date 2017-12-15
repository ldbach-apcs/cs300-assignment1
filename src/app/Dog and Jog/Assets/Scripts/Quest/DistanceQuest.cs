using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceQuest : IQuest {

    private double disRequire;
    // Record current distance statistic when quest created
    private double disStart;
	private double disTotal;
	private bool isDone = false;

    public DistanceQuest
		(string name, string description, double disRequire) : base(name, description)
    {
        this.disRequire = disRequire;
		this.disTotal = this.disStart = PlayerPrefs.GetFloat (DistanceQuestInput.PREV_DISTANCE, 0);
    }

	public DistanceQuest
		(string name, string description, double disRequire, double disStart): base(name, description)
	{
		this.disRequire = disRequire;
		this.disTotal = this.disStart = disStart;
	}

	public override void Update(QuestInputData data)
	{
		double totalDistance = data.GetValue (DistanceQuestInput.INPUT_DISTANCE);
		if (totalDistance - disStart >= disRequire)
			isDone = true;

		disTotal = totalDistance;
    }
	
    public override bool IsFinish()
    {
		return isDone;
    }

	public override void Save()
	{
		PlayerPrefs.SetString ("test_name", name);
		PlayerPrefs.SetString ("test_des", description);
		PlayerPrefs.SetFloat ("test_req", (float) disRequire);
		PlayerPrefs.SetFloat ("test_start", (float) disStart);
	}

	public override string ToString()
	{
		return "Quest name: " + name + ", Distance remaining: " + (disStart + disRequire - disTotal).ToString() + " meters";
	}
}

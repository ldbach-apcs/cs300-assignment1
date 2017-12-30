using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

public class MasterController : IQuestObserver {
	private static readonly MasterController instance = new MasterController();

	private QuestManager manager;
	private DatabaseReader dbReader = DatabaseReader.Instance();

	public static MasterController Instance()
	{
		return instance;
	}

	public void Init() {
		// Debug.Log("Do nothing");
	}

	private MasterController() {
		manager = new QuestManager(dbReader.ReadQuest());
		// Debug.Log("DB init done");
        QuestInputManager.Instance().Register(this);
		Debug.Log(manager.GetQuest().name);
	}

    public void OnQuestInput(QuestInputData data)
    {
        var newDis = data.GetValue(DistanceQuestInput.INPUT_DISTANCE);
        var newShare = data.GetValue(FacebookQuestInput.INPUT_SHARE);

        dbReader.totalDistance = Math.Max(newDis, dbReader.totalDistance);
        dbReader.totalShare = Math.Max((int) newShare, dbReader.totalShare);

        manager.UpdateQuest(data);
        if (manager.QuestIsFinish()) {
            dbReader.money += manager.GetQuest().rewardMoney;
            dbReader.exp += manager.GetQuest().rewardExp;
            manager.SetQuest(QuestFactory.Instance().GetQuest());
			dbReader.SaveSimpleData();
        }
    }

	
}

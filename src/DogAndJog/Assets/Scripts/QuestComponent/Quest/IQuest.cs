using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuest {
    public string name { get; set; }
    public string description { get; set; }
    protected string action {get; set; } // specify the action needed to complete quest like Run 10 meters
    // public GameData reward { get; }

    public int rewardExp { get; set;}
    public int rewardMoney {get; set;}
    public double prevValue {get; set;}
    public double requireValue {get; set;}

    public IQuest(string name, string description, int rewardExp, int rewardMoney)
    {
        this.name = name;
        this.description = description;
        this.rewardExp = rewardExp;
        this.rewardMoney = rewardMoney;
    }

    public abstract string GetProgress();
	public abstract void Update(QuestInputData data);
    public abstract bool IsFinish();
	public abstract void Save ();
}

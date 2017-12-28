using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuest {
    public string name { get; set; }
    public string description { get; set; }
    public string action {get; set; } // specify the action needed to complete quest like Run 10 meters
    // public GameData reward { get; }

    public IQuest(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public abstract string GetProgress();
	public abstract void Update(QuestInputData data);
    public abstract bool IsFinish();
	public abstract void Save ();
}

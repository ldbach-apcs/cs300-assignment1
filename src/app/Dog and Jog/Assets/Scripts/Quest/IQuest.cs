using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuest {
    protected string name { get; set; }
    protected string description { get; set; }
    // public GameData reward { get; }

    public IQuest(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

	public abstract void Update(QuestInputData data);
    public abstract bool IsFinish();
	public abstract void Save ();
}

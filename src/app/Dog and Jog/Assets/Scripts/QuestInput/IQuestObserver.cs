using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestObserver {
	void OnQuestInput(QuestInputData data);
}

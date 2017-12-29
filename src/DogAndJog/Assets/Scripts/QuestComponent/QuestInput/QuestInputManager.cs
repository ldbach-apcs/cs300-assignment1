using System.Collections;
using System.Collections.Generic;

/*
 * Maintain list of QuestInputDevices to register observer
 */

public sealed class QuestInputManager {
    private static readonly QuestInputManager instance = new QuestInputManager();

    private List<IQuestInput> inputs = new List<IQuestInput>();

	private QuestInputManager() {
        inputs.Add(new DistanceQuestInput());
        inputs.Add(new FacebookQuestInput());
    }

	public static QuestInputManager Instance()
	{
		return instance;
	}

    ~QuestInputManager() {
        foreach (var input in inputs)
        {
            input.Destroy();
        }
    }

    public void Register(IQuestObserver observer) {
        foreach (var input in inputs)
        {
            input.Register(observer);
        }
    }
}
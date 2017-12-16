<h1> CS300 Assignment</h1>

This is a project from group 4, 15 CTT. Our group include:
- Le Duy Bach
- Ho Sy Nguyen
- Lieng The Phy
- Tran Thoai Thong
- Bui Nguyen Duc Toan

This repository stores the software artifacts of the group, including documents, source code, and project assignments.

<h2> Extension of Quest </h2>

To add more quest types, you need to do the following:
1. Create a new QuestInput class extending IQuestInput
> Remeber to register GameController or Driver class as its Observer by calling IQuestInput.Register(IQuestObserver)
> Remember to call Notify(QuestInputData) when you want to update the quest
2. Create a new Quest class extending IQuest
> Constructor must call base(questName, questDescription)
> We need to override Update(QuestInputData) and IsFinish().
3. For manual testin purpose now, change QuestFactory to return only your Quest type
> QuestFactory should ideally be kept unchanged in working environment, however during development process, make QuestFactory return only your IQuest type can speed up the process
4. Change TestController class accordingly to your need
> Meaning change questInput object to be your QuestInput type defined above, also override IQuest.ToString() for ease of debugging.

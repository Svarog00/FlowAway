﻿using System.Collections.Generic;
using UnityEngine;

public class QuestValues : MonoBehaviour
{
	public static QuestValues Instance { get; private set; }
	[SerializeField] private List<QuestStage> _quests = new List<QuestStage>();

	public List<QuestStage> QuestList
	{
		get => _quests;
		set => _quests = value;
	}

	private void Awake()
	{
		if (Instance == null)
        {
			Instance = this; //Должен существовать только один глобальный экземпляр, в котором хранятся все значения для квестов.
        }
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(this);
	}

	public void Add(string name)
	{
		if (!_quests.Contains(new QuestStage { name = name }))
        {
			_quests.Add(new QuestStage { name = name, stage = 0 });
        }
	}

	public void SetStage(string name, int stage)
	{
		if (_quests.Contains(new QuestStage { name = name }))
        {
			QuestStage quest = _quests.Find(questToFind => questToFind.name.Equals(name));
			quest.stage = stage;
        }
		else
		{
			//Debug.Log($"Can not set stage of quest named as {name}");
		}
	}

	public int GetStage(string name, bool isInit = false)
	{
		//Если функция вызывается при первой загрузке диалога, то добавляется новый квест в список, в ином случае - возвращается -1 и в консоль выводится сообщение об ошибке.
		//При вызове функции не из диалога НЕ ПЕРЕДАВАТЬ ВТОРОЙ АРГУМЕНТ
		if (_quests.Contains(new QuestStage { name = name }))
		{
			QuestStage quest = _quests.Find(questToFind => questToFind.name.Equals(name));
			return quest == null ? -1 : quest.stage;
		}
		else if (isInit)
		{
			Add(name);
			QuestStage quest = _quests.Find(questToFind => questToFind.name.Equals(name));
			return quest == null ? -1 : quest.stage;
		}
		else
		{
			//Debug.Log($"Can not get stage of quest named as {name}");
			return -1;
		}
	}

	public void Clear()
	{
		_quests.Clear();
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestValues : MonoBehaviour
{
    private Dictionary<string, int> questValue = new Dictionary<string, int>(); //string - name of the quest, int - stage of the quest
    public static QuestValues Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this; //Должен существовать только один глобальный экземпляр, в котором хранятся все значения для квестов.
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void Add(string name)
    {
        if(!questValue.ContainsKey(name))
            questValue.Add(name, 0);
    }

    public void SetStage(string name, int stage)
    {
        if (questValue.ContainsKey(name))
            questValue[name] = stage;
        else
        {
            Debug.Log($"Can not find quest named as {name}");
        }
    }

    public int GetStage(string name, bool isInit = false)
    {
        //Если функция вызывается при первой загрузке диалога, то добавляется новый квест в список, в ином случае - возвращается -1 и в консоль выводится сообщение об ошибке.
        //При вызове функции не из диалога НЕ ПЕРЕДАВАТЬ ВТОРОЙ АРГУМЕНТ
        if (questValue.ContainsKey(name)) //If there is a good in dictionary
            return questValue[name];
        else if(isInit)
        {
            Add(name);
            return questValue[name];
        }
        else
        {
            Debug.Log($"Can not find quest named as {name}");
            return -1;
        }
    }

    public void Clear()
    {
        questValue.Clear();
    }

    public Dictionary<string, int> QuestList
    {
        get { return questValue; }
        set { questValue = value;  }
    }
}


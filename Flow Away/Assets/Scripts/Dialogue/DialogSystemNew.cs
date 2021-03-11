using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemNew : MonoBehaviour
{
    public TextAsset tAsset;
    public Dialogue dialogue;
    public List<Answer> answersList = new List<Answer>();
    public bool showDialog;
    public GUISkin GUISkin;

    private int curNode;

    void Start()
    {
        dialogue = Dialogue.Load(tAsset);
        UpdateAnswers();
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    void UpdateAnswers()
    {
        answersList.Clear(); //очистка диалога перед заполнением
        for (int i = 0; i < dialogue.nodes[curNode].answers.Length; i++)
        {
            if (dialogue.nodes[curNode].answers[i].questName == null
                || dialogue.nodes[curNode].answers[i].neededQuestValue == FindObjectOfType<QuestValues>().GetStage(dialogue.nodes[curNode].answers[i].questName, true))
                //PlayerPrefs.GetInt(dialogue.nodes[curNode].answers[i].questName)) //Если за этой фразой не закреплено диалога или какой-то квест находится на нужной стадии
            {                                                                       //То добавить его в лист ответов игрока
                answersList.Add(dialogue.nodes[curNode].answers[i]);
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = GUISkin;
        if(showDialog)
        {
            Time.timeScale = 0;
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 300), ""); //Создание бокса с ответами
            GUI.Label(new Rect(Screen.width / 2 - 145, Screen.height - 242, 400, 90), dialogue.nodes[curNode].npcText);//Фраза НПС
            for (int i = 0; i < answersList.Count; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 150 + 28 * i, 500, 25), answersList[i].text))//Если нажата кнопка с текстом ответа
                {
                    if(answersList[i].questValue > 0)
                    {
                        FindObjectOfType<QuestValues>().SetStage(answersList[i].questName, answersList[i].questValue);
                        //PlayerPrefs.SetInt(answersList[i].questName, answersList[i].questValue); //перевести квест на другой этап, если это связано с квестом
                    }
                    if(answersList[i].questValue == 1)
                    {
                        //questList.AddNewQuest(answersList[i].questName, dialogue.nodes[curNode].npcText);
                    }
                    if (answersList[i].endDialog == "true") //Если закончить диалог - закрыть окно
                    {
                        showDialog = false;
                        Time.timeScale = 1;
                    }
                    curNode = answersList[i].nextNode;
                    UpdateAnswers(); //Обновить список ответов
                }
            }
        }
    }
}

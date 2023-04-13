using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    public TextAsset tAsset;
    public Dialogue dialogue;
    public List<Answer> answersList = new List<Answer>();
    public bool showDialog;
    public GUISkin GUISkin;
    public Texture2D npcIconImage;

    private int curNode;

    private InventoryModel _playerInventory;

    void Start()
    {
        dialogue = Dialogue.Load(tAsset);

        SetPlayerInventory(FindObjectOfType<InventoryRoot>().InventoryModel);
        UpdateAnswers();
    }

    public void SetPlayerInventory(InventoryModel playerInventory)
    {
        _playerInventory = playerInventory;
    }

    private void UpdateAnswers()
    {
        answersList.Clear(); //очистка диалога перед заполнением
        for (int i = 0; i < dialogue.nodes[curNode].answers.Length; i++)
        {
            if (dialogue.nodes[curNode].answers[i].questName == null
                || dialogue.nodes[curNode].answers[i].neededQuestValue == QuestValues.Instance.GetStage(dialogue.nodes[curNode].answers[i].questName, true)
                || _playerInventory.PickItem(dialogue.nodes[curNode].answers[i].neededItemId))
                //Если за этой фразой не закреплено квеста или какой-то квест находится на нужной стадии или есть требуемый предмет
                //То добавить его в лист ответов игрока
            {
                answersList.Add(dialogue.nodes[curNode].answers[i]);
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = GUISkin;
        if(showDialog)
        {
            GUI.Box(new Rect(Screen.width / 2 - 375, Screen.height - 408, 750, 400), ""); //Создание бокса с ответами
            GUI.DrawTexture(new Rect(Screen.width / 2 - 300, Screen.height - 340, 128, 128), npcIconImage);
            GUI.Label(new Rect(Screen.width / 2 - 160, Screen.height - 335, 450, 110 ), dialogue.nodes[curNode].npcText);//Фраза НПС 
            for (int i = 0; i < answersList.Count; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 215 + 28 * i, 500, 25), answersList[i].text))//Если нажата кнопка с текстом ответа
                {
                    if(answersList[i].questValue > 0)
                    {
                        QuestValues.Instance.SetStage(answersList[i].questName, answersList[i].questValue);
                        //перевести квест на другой этап, если это связано с квестом
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

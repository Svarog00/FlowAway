using InventorySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_DialogueWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _npcText;
    [SerializeField] private GameObject _answersView;
    [SerializeField] private GameObject _answerButtonPrefab;
    [SerializeField] private GameObject _window;

    private InventoryModel _playerInventory;

    private Dialogue _dialogue;
    private int _curNode;
    
    [SerializeField] private List<Answer> _answers;

    public void Start()
    {
        _window.SetActive(false);
    }

    public void SetPlayerInventory(InventoryModel playerInventory)
    {
        _playerInventory = playerInventory;
    }

    public void ShowWindow(UnityEngine.TextAsset textAsset)
    {
        _dialogue = Dialogue.Load(textAsset);
        UpdateWindow();
        _window.SetActive(true);
    }

    public void CloseWindow()
    {
        _window?.SetActive(false);
    }

    private void UpdateWindow()
    {
        UpdateNpcText();
        UpdateAnswers();
    }

    private void UpdateNpcText()
    {
        _npcText.text = _dialogue.nodes[_curNode].npcText;
    }

    private void UpdateAnswers()
    {
        UpdateAnswersList();

        foreach (Transform button in _answersView.transform)
        {
            Destroy(button.gameObject);
        }
        for(int i = 0; i < _answers.Count; i++)
        {
            var answerIndex = i;
            GameObject itemButtonPrefab = Instantiate(_answerButtonPrefab, _answersView.transform);
            itemButtonPrefab.GetComponent<Button>().onClick.AddListener(() => ReactOnAnswer(answerIndex));
            itemButtonPrefab.GetComponentInChildren<TMP_Text>().text = _answers[i].text;
        }
    }

    public void UpdateAnswersList()
    {
        _answers.Clear(); //очистка диалога перед заполнением
        for (int i = 0; i < _dialogue.nodes[_curNode].answers.Length; i++)
        {
            if (_dialogue.nodes[_curNode].answers[i].questName == null
                || _dialogue.nodes[_curNode].answers[i].neededQuestValue == QuestValues.Instance.GetStage(_dialogue.nodes[_curNode].answers[i].questName, true)
                && (_playerInventory.PickItem(_dialogue.nodes[_curNode].answers[i].neededItemId) || _dialogue.nodes[_curNode].answers[i].neededItemId == 0))
            //Если за этой фразой не закреплено квеста или какой-то квест находится на нужной стадии или есть требуемый предмет
            //То добавить его в лист ответов игрока
            {
                _answers.Add(_dialogue.nodes[_curNode].answers[i]);
            }
        }
    }

    private void ReactOnAnswer(int answerIndex)
    {
        if (_answers[answerIndex].questValue > 0)
        {
            QuestValues.Instance.SetStage(_answers[answerIndex].questName, _answers[answerIndex].questValue);
            //перевести квест на другой этап, если это связано с квестом
        }
        if (_answers[answerIndex].questValue == 1)
        {
            //questList.AddNewQuest(answersList[i].questName, dialogue.nodes[curNode].npcText);
        }
        if (_answers[answerIndex].endDialog == "true") //Если закончить диалог - закрыть окно
        {
            _window.SetActive(false);
        }
        _curNode = _answers[answerIndex].nextNode;
        UpdateWindow(); //Обновить список ответов
    }
}

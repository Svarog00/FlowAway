using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TerminalWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _noteText;

    [SerializeField] private GameObject _notesListView;
    [SerializeField] private GameObject _window;

    [SerializeField] private GameObject _noteButtonPrefab;

    private TerminalModel _terminalModel;

    private void Awake()
    {
        _terminalModel = new TerminalModel();
    }

    private void Start()
    {
        _window.SetActive(false);
    }

    public void ShowWindow(TerminalModel model)
    {
        _terminalModel = model;
        _window.SetActive(true);
        UpdateWindow();
    }

    public void CloseWindow()
    {
        _window.SetActive(false);
    }

    private void UpdateWindow()
    {
        foreach(Transform noteButton in _notesListView.transform)
        {
            Destroy(noteButton.gameObject);
        }

        for(int i = 0; i < _terminalModel.Notes.Count; i++)
        {
            var noteIndex = i;
            GameObject noteButton = Instantiate(_noteButtonPrefab, _notesListView.transform);
            noteButton.GetComponent<Button>().onClick.AddListener(() => UpdateNoteTextWindow(noteIndex));
            noteButton.GetComponentInChildren<TMP_Text>().text = _terminalModel.Notes[i].Title;
        }
    }

    private void UpdateNoteTextWindow(int noteIndex)
    {
        _noteText.text = _terminalModel.Notes[noteIndex].Text;
    }
}
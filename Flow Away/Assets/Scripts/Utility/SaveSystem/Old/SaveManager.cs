using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private Transform _playerPos;
    private PlayerHealthController _playerHealthController;

    private SaveLoadSystem _saveLoadSystem;

    public void Awake()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        _playerHealthController = player.GetComponent<PlayerHealthController>();
        _playerPos = player.transform;

        _saveLoadSystem = new SaveLoadSystem(_playerPos, _playerHealthController);

        //Флаг LevelMove ставится в Exit.cs при переходе со сцены на сцену
        if (PlayerPrefs.GetInt("LevelMove") == 1) //Если это переход с другой сцены, то загружаем данные из сейва, который был сделан до перехода на эту сцену
        {
            Continue(SceneManager.GetActiveScene().name);
        }
        else if (PlayerPrefs.GetInt("LoadHandleSave") == 1)
        {
            _saveLoadSystem.LoadHandleSave();
            PlayerPrefs.SetInt("LoadHandleSave", 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            _saveLoadSystem.SaveData("Handle_Save");
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.SetInt("LoadHandleSave", 1);
            _saveLoadSystem.LoadHandleSave();
        }
    }

    private void Continue(string nameSave)
    {
        PlayerPrefs.SetInt("QuickLoad", 1);
        _saveLoadSystem.LoadData(nameSave);
        PlayerPrefs.SetInt("QuickLoad", 0);
        PlayerPrefs.SetInt("LevelMove", 0);
        Debug.Log("Loaded level exit save");
    }

    public void LoadLastSave()
    {
        _saveLoadSystem.LoadHandleSave();
    }

    public void ClearSaves()
    {
        _saveLoadSystem.ClearSaves();
    }

    public void SaveGame(string name)
    {
        _saveLoadSystem.SaveData(name);
    }
}

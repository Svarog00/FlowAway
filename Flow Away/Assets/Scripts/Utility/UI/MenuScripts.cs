using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour {

    private SaveLoadSystem load;

    private void Start()
    {
        load = GetComponent<SaveLoadSystem>();
    }

    void OnGUI()
    {
        const int buttonWidth = 85;
        const int buttonHeight = 30;

        Rect buttonStart = new Rect(Screen.width / 2 - (buttonHeight / 2), (2 * Screen.height / 3.5f) - (buttonHeight / 2), buttonWidth, buttonHeight);
        Rect buttonLoad = new Rect(Screen.width / 2 - (buttonHeight / 2), (2 * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight);
        Rect buttonExit = new Rect(Screen.width / 2 - (buttonHeight / 2), (2 * Screen.height / 2.6f) - (buttonHeight / 2), buttonWidth, buttonHeight);

        if (GUI.Button(buttonStart, "StartGame"))
        {
            PlayerPrefs.DeleteAll();
            
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("Space", LoadSceneMode.Single);
        }
        if(GUI.Button(buttonLoad, "LoadGame"))
        {
            Debug.Log("Loading last save");
            PlayerPrefs.SetInt("QuickLoad", 0);
            load.LoadData("CheckPoint");
        }
        if(GUI.Button(buttonExit, "Exit"))
        {
            Application.Quit();
        }
    }
}

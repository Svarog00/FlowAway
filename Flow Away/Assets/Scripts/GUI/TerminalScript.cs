using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    public GUISkin GUISkin;

    void Start()
    {
        
    }

    private void OnGUI() 
    {
        GUI.skin = GUISkin;
        Time.timeScale = 0;
        GUI.Box(new Rect(Screen.width / 2 - 700, Screen.height - 800, 1400, 700), ""); //Создание бокса с ответами
    }
}

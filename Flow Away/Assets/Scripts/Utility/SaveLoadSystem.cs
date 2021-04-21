using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public Transform PlayerPos;
    public Player_Health PlayerHP;
    public Player_Healing PlayerMedkits;

    public void SaveData(string name) //Параметр название сейва для разделения сохранений на чекпоинты и переходы между сценами
    {
        SavedData tmp = new SavedData
        {
            x = PlayerPos.position.x,
            y = PlayerPos.position.y,
            health = PlayerHP.CurrentHealth,
            medkitCount = PlayerMedkits.GetCapsuleCount(),
            currentScene = SceneManager.GetActiveScene().name
            //questValues = QuestValues.Instance.QuestList
        };

        Debug.Log(tmp.health + " " + tmp.x + " " + tmp.y);
        if (!Directory.Exists(Application.dataPath + "/Saves")) //if directory doesn't exist 
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves"); //then create directory
        }

        FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Create); //open stream to create a save file
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, tmp); //serialize savedData in fs file
        fs.Close(); //close file stream

        Debug.Log("Saved: HP - " + tmp.health + "; position - " + tmp.x + " " + tmp.y + "; scene - " + tmp.currentScene);

    }

    public void LoadData(string name) //LevelMove = player move berween scenes //CheckPoint = player died
    {
        
        if (File.Exists(Application.dataPath + "/Saves/" + name + ".sv"))
        {
            
            FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                SavedData tmp = (SavedData)formatter.Deserialize(fs);
                if (PlayerPrefs.GetInt("QuickLoad") == 0) //load from main menu
                {
                    SceneManager.LoadScene(tmp.currentScene, LoadSceneMode.Single);
                }
                else if(PlayerPrefs.GetInt("QuickLoad") == 1)//quickload from ingame menu or in case of dead or to continue from certain point
                {
                    PlayerPos.position = new Vector2(tmp.x, tmp.y);
                    PlayerHP.CurrentHealth = tmp.health;
                    PlayerMedkits.LoadCapsule(tmp.medkitCount);
                    //QuestValues.Instance.QuestList = tmp.questValues;
                }
            }
            catch (System.Exception Error)
            {
                Debug.Log(Error.Message);
            }
            finally
            {
                fs.Close();
            }
        }
    }

    public void AutoSave()
    {
        SavedData tmp = new SavedData
        {
            x = PlayerPos.position.x,
            y = PlayerPos.position.y,
            medkitCount = PlayerMedkits.GetCapsuleCount()
            //questValues = QuestValues.Instance.QuestList
        };
        if (!Directory.Exists(Application.dataPath + "/Saves")) //if directory doesn't exist
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves"); //then create directory
        }
        FileStream fs = new FileStream(Application.dataPath + "/Saves/auto.sv", FileMode.Create); //open stream to create a save file
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, tmp); //serialize savedData in fs file
        fs.Close(); //close file stream

        Debug.Log("Position in Space saved " + tmp.x + " " + tmp.y);
    }

}


[System.Serializable]
public class SavedData
{
    public float x, y;
    public int health;
    public string currentScene;
    public int medkitCount;
    //public Dictionary<string, int> questValues;
}


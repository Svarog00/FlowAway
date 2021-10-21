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
		SavedData saveData = new SavedData
		{
			x = PlayerPos.position.x,
			y = PlayerPos.position.y,
			health = PlayerHP.CurrentHealth,
			medkitCount = PlayerMedkits.GetCapsuleCount(),
			currentScene = SceneManager.GetActiveScene().name,
			questValues = new List<QuestStages>(QuestValues.Instance.QuestList)
		};
		if (!Directory.Exists(Application.dataPath + "/Saves")) //if directory doesn't exist 
		{
			Directory.CreateDirectory(Application.dataPath + "/Saves"); //then create directory
		}

		FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Create); //open stream to create a save file
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fs, saveData); //serialize savedData in fs file
		fs.Close(); //close file stream

		Debug.Log("Saved: HP - " + saveData.health + "; position - " + saveData.x + " " + saveData.y + "; scene - " + saveData.currentScene);

	}

	public void LoadData(string name) //LevelMove = player move berween scenes //Handle_Save = player died
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
					SceneManager.LoadSceneAsync(tmp.currentScene, LoadSceneMode.Single);
				}
				else if(PlayerPrefs.GetInt("QuickLoad") == 1)//quickload from ingame menu or in case of dead or to continue from certain point
				{
					PlayerPos.position = new Vector2(tmp.x, tmp.y);
					PlayerHP.CurrentHealth = tmp.health;
					PlayerMedkits.LoadCapsule(tmp.medkitCount);
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

	public void LoadHandleSave()
	{
		if (File.Exists(Application.dataPath + "/Saves/Handle_Save.sv"))
		{
			FileStream fs = new FileStream(Application.dataPath + "/Saves/Handle_Save.sv", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				SavedData tmp = (SavedData)formatter.Deserialize(fs);
				if(tmp.currentScene != SceneManager.GetActiveScene().name)
                {
					SceneManager.LoadSceneAsync(tmp.currentScene, LoadSceneMode.Single);
				}
				else //quickload from ingame menu or in case of dead or to continue from certain point
				{
					PlayerPos.position = new Vector2(tmp.x, tmp.y);
					PlayerHP.CurrentHealth = tmp.health;
					PlayerMedkits.LoadCapsule(tmp.medkitCount);
					QuestValues.Instance.QuestList = new List<QuestStages>(tmp.questValues);
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

	public void ClearSaves()
    {
		if (Directory.Exists(Application.dataPath + "/Saves"))
		{
			var dirInfo = new DirectoryInfo(Application.dataPath + "/Saves");
			foreach (var file in dirInfo.GetFiles())
			{
				if (file.FullName == "Handle_Save")
					continue;
				file.Delete();
			}
		}
	}
}



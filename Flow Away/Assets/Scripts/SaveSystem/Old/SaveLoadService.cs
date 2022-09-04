using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadService
{
	private Transform _playerPos;
	private PlayerHealthController _playerHealth;
	private QuestValues _questValues;
			
    public SaveLoadService()
    {

    }

	public SaveLoadService(Transform playerPos, PlayerHealthController playerHealth)
    {
		_playerPos = playerPos;
		_playerHealth = playerHealth;
		_questValues = QuestValues.Instance;
    }

    public void SaveData(string name, WorldData worldData) //Параметр название сейва для разделения сохранений на чекпоинты и переходы между сценами
	{
		if (!Directory.Exists(Application.dataPath + "/Saves")) //if directory doesn't exist 
		{
			Directory.CreateDirectory(Application.dataPath + "/Saves"); //then create directory
		}

		FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Create); //open stream to create a save file
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fs, worldData); //serialize savedData in fs file
		fs.Close(); //close file stream

		Debug.Log("Saved: HP - " + worldData.health + "; position - " + worldData.x + " " + worldData.y + "; scene - " + worldData.currentScene);

	}

	public void LoadData(string name) //LevelMove = player move berween scenes //Handle_Save = player died
	{
		if (File.Exists(Application.dataPath + "/Saves/" + name + ".sv"))
		{
			FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				WorldData tmp = (WorldData)formatter.Deserialize(fs);
				if (PlayerPrefs.GetInt("QuickLoad") == 0) //load from main menu
				{
					SceneManager.LoadSceneAsync(tmp.currentScene, LoadSceneMode.Single);
				}
				else if(PlayerPrefs.GetInt("QuickLoad") == 1)//quickload from ingame menu or in case of dead or to continue from certain point
				{
					_playerPos.position = new Vector2(tmp.x, tmp.y);
					_playerHealth.PlayerHealth = tmp.health;
					_playerHealth.LoadCapsule(tmp.medkitCount);
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
				WorldData tmp = (WorldData)formatter.Deserialize(fs);
				if(tmp.currentScene != SceneManager.GetActiveScene().name)
                {
					SceneManager.LoadSceneAsync(tmp.currentScene, LoadSceneMode.Single);
				}
				else //quickload from ingame menu or in case of dead or to continue from certain point
				{
					_playerPos.position = new Vector2(tmp.x, tmp.y);
					_playerHealth.PlayerHealth = tmp.health;
					_playerHealth.LoadCapsule(tmp.medkitCount);
					_questValues.QuestList = new List<QuestStages>(tmp.questValues);
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



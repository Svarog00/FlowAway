using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Infrastructure;

public class SaveLoadService : ISaveLoadService
{		
    public SaveLoadService()
    {

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

	public WorldData LoadData(string name) //LevelMove = player move berween scenes //Handle_Save = player died
	{
		if (File.Exists(Application.dataPath + "/Saves/" + name + ".sv"))
		{
			FileStream fs = new FileStream(Application.dataPath + "/Saves/" + name + ".sv", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				WorldData tmp = (WorldData)formatter.Deserialize(fs);
                return tmp;
			}
			catch (System.Exception Error)
			{
				Debug.Log(Error.Message);
				return null;
			}
			finally
			{
				fs.Close();
			}
		}
		return null;
	}

	public WorldData LoadHandleSave()
	{
		if (File.Exists(Application.dataPath + "/Saves/Handle_Save.sv"))
		{
			FileStream fs = new FileStream(Application.dataPath + "/Saves/Handle_Save.sv", FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				WorldData tmp = (WorldData)formatter.Deserialize(fs);
                return tmp;
            }
			catch (System.Exception Error)
			{
				Debug.Log(Error.Message);
                return null;
            }
			finally
			{
				fs.Close();
			}
		}
        return null;
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



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializationManager
{
    public bool SaveData(string saveName, object obj)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.dataPath + "/Saves")) //if directory doesn't exist 
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves"); //then create directory
        }

        string persistentPath = Application.dataPath + "/Saves/" + saveName + ".sv";
        FileStream fs = File.Create(persistentPath);
        binaryFormatter.Serialize(fs, obj);

        fs.Close();

        return true;
    }

    public object LoadData(string name)
    {
        string path = Application.dataPath + "/Saves/" + name + ".sv";

        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        FileStream fs = File.Open(path, FileMode.Open);

        try
        {
            object save = binaryFormatter.Deserialize(fs);
            fs.Close();
            return save;
        }
        catch
        {
            Debug.LogError("Failed to deserialize");
            return null;
        }
    }

    private BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        return binaryFormatter;
    }
}

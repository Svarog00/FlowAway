using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _instance;

    public List<EntityData> entities;

    public static SaveData Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new SaveData();
            }
            return _instance;
        }
    }


}

using System.Collections.Generic;

[System.Serializable]
public class WorldData
{
	public float x, y;
	public int health;
	public string currentScene;
	public int medkitCount;
	public List<QuestStages> questValues;
}



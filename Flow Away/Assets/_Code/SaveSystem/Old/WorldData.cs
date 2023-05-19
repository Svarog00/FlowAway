using System.Collections.Generic;

[System.Serializable]
public class WorldData
{
	public float x, y;
	public int Health;
	public string CurrentScene;
	public int MedkitCount;
	public List<QuestStage> QuestValues;
	public List<int> Items;
	public List<bool> AbilityStatuses;
}



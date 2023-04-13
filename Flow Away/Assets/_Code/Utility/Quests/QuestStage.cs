using System;

[System.Serializable]
public class QuestStage : IEquatable<QuestStage>
{
	public string name;
	public int stage = 0;

	public bool Equals(QuestStage other)
	{
		if (other == null)
			return false;
		else 
			return name.Equals(other.name);
	}
}



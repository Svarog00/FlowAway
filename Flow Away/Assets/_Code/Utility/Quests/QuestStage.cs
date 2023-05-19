using System;

[System.Serializable]
public class QuestStage : IEquatable<QuestStage>
{
	public string QuestName;
	public int Stage = 0;

	public bool Equals(QuestStage other)
	{
		if (other == null)
			return false;
		else 
			return QuestName.Equals(other.QuestName);
	}
}



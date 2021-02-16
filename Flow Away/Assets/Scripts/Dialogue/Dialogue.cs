using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dialogue")]
public class Dialogue
{
    [XmlElement("node")]
    public Node[] nodes;

    public static Dialogue Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialogue));
        StringReader reader = new StringReader(_xml.text);
        Dialogue dialogue = serializer.Deserialize(reader) as Dialogue;
        return dialogue;
    }
}

[System.Serializable]
public class Node
{
    [XmlElement("npctext")]
    public string npcText;

    [XmlArray("answers")]//Массив ответов в XML файле
    [XmlArrayItem("answer")] 
    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    [XmlElement("text")]
    public string text; //сам текст диалога
    [XmlAttribute("tonode")]
    public int nextNode; //переход к какому-то другому узлу диалогов
    //  
    [XmlElement("dialend")]
    public string endDialog; //конец диалога или нет
    [XmlAttribute("questvalue")]
    public int questValue; //значение на каком этапе находится квест
    [XmlAttribute("neededquestvalue")]
    public int neededQuestValue; //нужный этап квеста для появления строчки диалога
    [XmlElement("questname")]
    public string questName; //название квеста
}

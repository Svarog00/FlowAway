using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("terminal")]
public class TerminalModel
{
    [XmlArray("notes")]
    [XmlArrayItem("note")]
    public Note[] notes;

    public TerminalModel Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TerminalModel));
        StringReader reader = new StringReader(_xml.text);
        TerminalModel terminal = serializer.Deserialize(reader) as TerminalModel;
        return terminal;
    }
}

[System.Serializable]
public class Note
{
    [XmlAttribute("title")]
    public string title;
    [XmlElement("text")]
    public string text;
}


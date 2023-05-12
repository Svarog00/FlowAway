using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[XmlRoot("terminal")]
public class TerminalModel
{
    [XmlArray("notes")]
    [XmlArrayItem("note")]
    public List<Note> Notes;

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
    public string Title;
    [XmlElement("text")]
    public string Text;
}


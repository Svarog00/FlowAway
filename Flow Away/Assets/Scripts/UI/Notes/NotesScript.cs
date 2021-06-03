using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

    [XmlRoot("terminal")]
    public class Terminal
    {
        [XmlArray("notes")]
        [XmlArrayItem("note")]
        public Note[] notes;

        public static Terminal Load(TextAsset _xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Terminal));
            StringReader reader = new StringReader(_xml.text);
            Terminal terminal = serializer.Deserialize(reader) as Terminal;
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


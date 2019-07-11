using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "input")]
    public class Input
    {
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }
        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "position")]
        public string Position { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
        [XmlAttribute(AttributeName = "loop")]
        public string Loop { get; set; }
        [XmlText]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "muted")]
        public string Muted { get; set; }
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "balance")]
        public string Balance { get; set; }
        [XmlAttribute(AttributeName = "solo")]
        public string Solo { get; set; }
        [XmlAttribute(AttributeName = "audiobusses")]
        public string Audiobusses { get; set; }
        [XmlAttribute(AttributeName = "meterF1")]
        public string MeterF1 { get; set; }
        [XmlAttribute(AttributeName = "meterF2")]
        public string MeterF2 { get; set; }
    }


}

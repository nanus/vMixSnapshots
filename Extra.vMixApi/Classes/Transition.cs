using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "transition")]
    public class Transition
    {
        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }
        [XmlAttribute(AttributeName = "effect")]
        public string Effect { get; set; }
        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
    }
}

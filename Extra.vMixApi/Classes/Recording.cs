using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "recording")]
    public class Recording
    {
        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
        [XmlText]
        public string IsActive { get; set; }
    }
}

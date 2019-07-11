using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "overlay")]
    public class Overlay
    {
        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }
    }
}

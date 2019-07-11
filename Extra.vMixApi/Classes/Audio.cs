using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "audio")]
    public class Audio
    {
        [XmlElement(ElementName = "master")]
        public Master Master { get; set; }
    }
}

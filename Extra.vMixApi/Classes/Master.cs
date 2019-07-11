using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "master")]
    public class Master
    {
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "muted")]
        public string Muted { get; set; }
        [XmlAttribute(AttributeName = "meterF1")]
        public string MeterF1 { get; set; }
        [XmlAttribute(AttributeName = "meterF2")]
        public string MeterF2 { get; set; }
        [XmlAttribute(AttributeName = "headphonesVolume")]
        public string HeadphonesVolume { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "overlays")]
    public class Overlays
    {
        [XmlElement(ElementName = "overlay")]
        public List<Overlay> Overlay { get; set; }
    }
}

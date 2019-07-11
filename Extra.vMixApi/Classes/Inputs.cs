using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "inputs")]
    public class Inputs
    {
        [XmlElement(ElementName = "input")]
        public List<Input> Input { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "transitions")]
    public class Transitions
    {
        [XmlElement(ElementName = "transition")]
        public List<Transition> Transition { get; set; }
    }
}

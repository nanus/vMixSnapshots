using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Extra.vMixApi
{
    [XmlRoot(ElementName = "vmix")]
    public class vMixStatus
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "edition")]
        public string Edition { get; set; }
        [XmlElement(ElementName = "preset")]
        public string Preset { get; set; }
        [XmlElement(ElementName = "inputs")]
        public Inputs Inputs { get; set; }
        [XmlElement(ElementName = "overlays")]
        public Overlays Overlays { get; set; }
        [XmlElement(ElementName = "preview")]
        public string Preview { get; set; }
        [XmlElement(ElementName = "active")]
        public string Active { get; set; }
        [XmlElement(ElementName = "fadeToBlack")]
        public string FadeToBlack { get; set; }
        [XmlElement(ElementName = "transitions")]
        public Transitions Transitions { get; set; }
        [XmlElement(ElementName = "recording")]
        public Recording Recording { get; set; }
        [XmlElement(ElementName = "external")]
        public string External { get; set; }
        [XmlElement(ElementName = "streaming")]
        public string Streaming { get; set; }
        [XmlElement(ElementName = "playList")]
        public string PlayList { get; set; }
        [XmlElement(ElementName = "multiCorder")]
        public string MultiCorder { get; set; }
        [XmlElement(ElementName = "fullscreen")]
        public string Fullscreen { get; set; }
        [XmlElement(ElementName = "audio")]
        public Audio Audio { get; set; }
    }
}

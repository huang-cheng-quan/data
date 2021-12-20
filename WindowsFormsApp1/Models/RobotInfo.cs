using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dyestripping.Models
{
    [XmlRoot(ElementName = "RobotInfo")]
    public class RobotInfo
    {
        [XmlElement(ElementName = "Offsets")]
        public List<PickOffset> Offsets = new List<PickOffset>();//取料补偿
    }
    public class PickOffset 
    {
        [XmlAttribute("PickStation")]
        public int PickStation { get; set; }//贴胶工位
        [XmlElement(ElementName = "OffsetX")]
        public float OffsetX { get; set; }//放料X轴偏移量
        [XmlElement(ElementName = "OffsetY")]
        public float OffsetY { get; set; }//放料Y轴偏移量
        [XmlElement(ElementName = "OffsetU")]
        public float OffsetU { get; set; }//放料U轴偏移量(deg)
    }

}

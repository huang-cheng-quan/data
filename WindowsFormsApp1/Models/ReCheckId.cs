using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Models
{
    [XmlRoot(ElementName = "ReCheckId")]
    public class ReCheckId
    {
        [XmlArray("ReCheckIdNumber")]
        public List<string> ReCheckIdNumber { get; set; }
    }
}

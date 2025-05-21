using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    [XmlRoot("MedicalRecords")]
    public class MedicalRecords
    {
        [XmlElement("Record")]
        public List<MedicalRecord> Records { get; set; }

        public MedicalRecords()
        {
            Records = new List<MedicalRecord>();
        }
    }
}
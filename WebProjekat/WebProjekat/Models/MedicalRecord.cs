using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class MedicalRecord
    {

        [XmlElement("Patient")]
        public Patient Patient { get; set; }

        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment")]
        public List<Appointment> Appointments { get; set; }

        public MedicalRecord()
        {
            Appointments = new List<Appointment>();
        }
    }
}
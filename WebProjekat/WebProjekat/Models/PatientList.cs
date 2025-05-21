using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    [XmlRoot("Patients")]
    public class PatientList
    {
        [XmlElement("Patient")]
        public List<Patient> Patients { get; set; }

        public PatientList()
        {
            Patients = new List<Patient>();
        }
    }
}
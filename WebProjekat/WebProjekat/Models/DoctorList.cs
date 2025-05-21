using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    [XmlRoot("Doctors")]
    public class DoctorList
    {
        [XmlElement("Doctor")]
        public List<Doctor> Doctors { get; set; }

        public DoctorList()
        {
            Doctors = new List<Doctor>();
        }
    }
}
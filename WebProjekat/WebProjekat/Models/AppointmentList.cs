using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    [XmlRoot("Appointments")]
    public class AppointmentList
    {
        [XmlElement("Appointment")]
        public List<Appointment> Appointments { get; set; }

        public AppointmentList()
        {
            Appointments = new List<Appointment>();
        }
    }
}
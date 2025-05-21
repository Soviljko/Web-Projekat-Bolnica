using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class Doctor
    {
        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Surname")]
        public string Surname { get; set; }

        [XmlElement("BirthDate")]
        public DateTime BirthDate { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment")]
        public List<Appointment> Appointments { get; set; }

        public Doctor()
        {
            Appointments = new List<Appointment>();
        }

        public Doctor(string username, string password, string name, string surname, DateTime birthDate, string email)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Appointments = new List<Appointment>();
        }

    }
}
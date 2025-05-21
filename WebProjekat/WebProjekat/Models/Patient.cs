using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class Patient
    {
        //[XmlElement("Username")]
        //public string Username { get; set; }

        //[XmlElement("JMBG")]
        //public string JMBG { get; set; }

        //[XmlElement("Password")]
        //public string Password { get; set; }

        //[XmlElement("Name")]
        //public string Name { get; set; }

        //[XmlElement("Surname")]
        //public string Surname { get; set; }

        //[XmlElement("BirthDate")]
        //public DateTime BirthDate { get; set; }

        //[XmlElement("Email")]
        //public string Email { get; set; }

        //[XmlArray("Appointments")]
        //[XmlArrayItem("Appointment")]
        //public List<Appointment> Appointments { get; set; }

        //[XmlArray("Therapies")]
        //[XmlArrayItem("Therapy")]
        //public List<string> TherapyList { get; set; }

        //public Patient()
        //{
        //    Appointments = new List<Appointment>();
        //    TherapyList = new List<string>();
        //}

        //public Patient(string username, string jMBG, string password, string name, string surname, DateTime date, string email)
        //{
        //    Username = username;
        //    JMBG = jMBG;
        //    Password = password;
        //    Name = name;
        //    Surname = surname;
        //    BirthDate = date;
        //    Email = email;
        //    Appointments = new List<Appointment>();
        //    TherapyList = new List<string>();
        //}

        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement("JMBG")]
        public string JMBG { get; set; }

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

        [XmlArray("Therapies")]
        [XmlArrayItem("Therapy")]
        public List<string> Therapies { get; set; }

        public Patient()
        {
            Appointments = new List<Appointment>();
            Therapies = new List<string>();
        }

        public Patient(string username, string jmbg, string password, string name, string surname, DateTime birthDate, string email)
        {
            Username = username;
            JMBG = jmbg;
            Password = password;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Appointments = new List<Appointment>();
            Therapies = new List<string>();
        }
    }
}

using System;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public enum Status
    {
        [XmlEnum("FREE")]
        FREE,

        [XmlEnum("SCHEDULED")]
        SCHEDULED
    }

    public class Appointment
    {
        [XmlElement("Doctor")]
        public Doctor Doctor { get; set; }

        [XmlElement("Status")]
        public Status Status { get; set; }

        [XmlElement("DateOfAppointment")]
        public DateTime AppointmentDate { get; set; }

        [XmlElement("Description")]
        public string DescriptionOfTherapy { get; set; }

        public Appointment()
        {
            Doctor = new Doctor();
        }

        public Appointment(Doctor doctor, Status status, DateTime appointmentDate, string descriptionOfTherapy)
        {
            Doctor = doctor;
            Status = status;
            AppointmentDate = appointmentDate;
            DescriptionOfTherapy = descriptionOfTherapy;
        }
    }
}

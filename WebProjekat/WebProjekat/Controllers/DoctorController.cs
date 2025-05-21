using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        // GET za Main Page
        public ActionResult DoctorMainPage()
        {
            var currentDoctor = Session["User"] as Doctor;

            if(currentDoctor == null)
            {
                return RedirectToAction("Login", "Register");
            }

            string path = Server.MapPath("~/App_Data/Appointments.xml");
            AppointmentList appointments;
            List<Appointment> listOfAppointments;

            if (System.IO.File.Exists(path))
            {
                appointments = AppointmentsLoader();
                listOfAppointments = appointments.Appointments;

            }
            else
            {
                listOfAppointments = new List<Appointment>();
            }

            var doctorAppointments = listOfAppointments.Where(a => a.Doctor.Username == currentDoctor.Username).ToList();

            return View(doctorAppointments);
        }

        // GET
        public ActionResult AppointmentCreation()
        {
            return View(new Appointment());
        }

        // POST

        [HttpPost]
        public ActionResult AppointmentCreation(Appointment madeAppointment)
        {
            var doctor = Session["User"] as Doctor;

            if(doctor == null)
            {
                return RedirectToAction("Login", "Register");
            }

            madeAppointment.Doctor = doctor;
            madeAppointment.Status = Status.FREE;

            AppointmentList appointments = AppointmentsLoader();
            List<Appointment> listOfAllAppointments = appointments.Appointments;

            listOfAllAppointments.Add(madeAppointment);

            SaveAppointmentsToXml(appointments);

            return RedirectToAction("DoctorMainPage");

        }

        public ActionResult TherapyPrescription(long idOfAppointmentForPrescription)
        {
            AppointmentList appointments = AppointmentsLoader();
            List<Appointment> listOfAllAppointments = appointments.Appointments;

            var appointmentForPrescription = listOfAllAppointments.FirstOrDefault(a => a.AppointmentDate.Ticks == idOfAppointmentForPrescription);

            if(appointmentForPrescription == null)
            {
                return RedirectToAction("DoctorMainPage");
            }

            return View(appointmentForPrescription);
        }

        [HttpPost]
        public ActionResult TherapySavingForPatient(long idOfAppointmentForPrescription, string descriptionOfTherapy)
        {
            try
            {
                AppointmentList appointments = AppointmentsLoader();
                List<Appointment> listOfAllAppointments = appointments.Appointments;

                var appointmentForPrescription = listOfAllAppointments.FirstOrDefault(a => a.AppointmentDate.Ticks == idOfAppointmentForPrescription);

                if(appointmentForPrescription != null)
                {
                    appointmentForPrescription.DescriptionOfTherapy = descriptionOfTherapy;

                    SaveAppointmentsToXml(appointments);

                    PatientList patients = PatientsLoader();
                    List<Patient> listOfAllPatients = patients.Patients;

                    // Finding patient with appointment selected

                    foreach(Patient patient in listOfAllPatients)
                    {
                        var appointmentForPatient = patient.Appointments.FirstOrDefault(a => a.AppointmentDate.Ticks == idOfAppointmentForPrescription);
                        if(appointmentForPatient != null)
                        {
                            appointmentForPatient.DescriptionOfTherapy = descriptionOfTherapy;
                            break;
                        }
                    }

                    SavePatientsToXml(patients);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Saving Therapy Failed: " + ex.Message);
            }

            return RedirectToAction("DoctorMainPage");
        }


        private PatientList PatientsLoader()
        {
            string filePath = Server.MapPath("~/App_Data/Patients.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(PatientList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (PatientList)serializer.Deserialize(fileStream);
            }
        }

        private void SavePatientsToXml(PatientList patients)
        {
            string filePath = Server.MapPath("~/App_Data/Patients.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(PatientList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, patients);
            }
        }

        private AppointmentList AppointmentsLoader()
        {
            string filePath = Server.MapPath("~/App_Data/Appointments.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (AppointmentList)serializer.Deserialize(fileStream);
            }
        }

        private void SaveAppointmentsToXml(AppointmentList appointmentList)
        {
            string filePath = Server.MapPath("~/App_Data/Appointments.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, appointmentList);
            }
        }
    }
}
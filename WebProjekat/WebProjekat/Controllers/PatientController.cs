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
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PatientMainPage()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["User"] is Patient)
            {
                ViewBag.UserRole = "Patient";
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult PatientAppointmentScheduling()
        {
            AppointmentList appointments = AppointmentsLoader();
            List<Appointment> listOfAllAppointments = appointments.Appointments;

            List<Appointment> unscheduledAppointments = listOfAllAppointments.Where(a => a.Status == Status.FREE).ToList();

            return View(unscheduledAppointments);
        }

        [HttpPost]
        public ActionResult PatientAppointmentScheduling(long appointmentId)
        {
            Patient loggedPatient = Session["User"] as Patient;

            if(loggedPatient == null)
            {
                return RedirectToAction("Login", "Register");
            }

            AppointmentList appointments = AppointmentsLoader();
            List<Appointment> listOfAllAppointments = appointments.Appointments;

            var choosenAppointment = listOfAllAppointments.FirstOrDefault(a => a.AppointmentDate.Ticks == appointmentId);

            if(choosenAppointment == null)
            {
                ViewBag.Error = "Appointment selection failed!";
                return View(listOfAllAppointments.Where(a => a.Status == Status.FREE).ToList());
            }

            choosenAppointment.Status = Status.SCHEDULED;

            loggedPatient.Appointments.Add(choosenAppointment);

            SaveAppointmentsToXml(appointments);

            PatientSaver(loggedPatient);

            return RedirectToAction("PatientMainPage");

        }

        public ActionResult SortAppointments(string sortOrder)
        {
            var patient = Session["User"] as Patient;

            if (patient == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var appointments = patient.Appointments.AsQueryable();

            switch (sortOrder)
            {
                case "date_asc":
                    appointments = appointments.OrderBy(a => a.AppointmentDate);
                    break;
                case "date_desc":
                    appointments = appointments.OrderByDescending(a => a.AppointmentDate);
                    break;
                case "doctor_asc":
                    appointments = appointments.OrderBy(a => a.Doctor.Name);
                    break;
                case "doctor_desc":
                    appointments = appointments.OrderByDescending(a => a.Doctor.Name);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.AppointmentDate);
                    break;
            }

            patient.Appointments = appointments.ToList();
            return View("PatientMainPage", patient); // Ovaj View bi trebao da se poklapa sa vašim trenutnim pogledom
        }




        private void PatientSaver(Patient scheduledPatient)
        {
            string filePath = Server.MapPath("~/App_Data/Patients.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(PatientList));
            PatientList patientList;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                patientList = (PatientList)serializer.Deserialize(fileStream);
            }

            int index = patientList.Patients.FindIndex(p => p.JMBG == scheduledPatient.JMBG);
            if (index != -1)
            {
                patientList.Patients[index] = scheduledPatient;
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, patientList);
            }
        }





        private void SaveAppointmentsToXml(AppointmentList appointmentList)
        {
            // Definiši putanju gde će se XML fajl sačuvati
            string filePath = Server.MapPath("~/App_Data/Appointments.xml");

            // Kreiraj XmlSerializer za tip AppointmentList
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));

            // Otvori FileStream za kreiranje ili prepisivanje fajla
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                // Serijalizuj AppointmentList objekat u XML format i sačuvaj u fajl
                serializer.Serialize(fileStream, appointmentList);
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


    }
}
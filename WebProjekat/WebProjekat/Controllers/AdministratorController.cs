using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Serialization;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        // GET
        public ActionResult PatientCreation()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult PatientCreation(Patient createdPatient)
        {
            PatientList patients = PatientsLoader();
            List<Patient> listOfPatients = patients.Patients;

            if (listOfPatients.Any(p => p.Username == createdPatient.Username))
            {
                ModelState.AddModelError("Username", "Patient with entered username already exists in database!");
            }

            if(listOfPatients.Any(p => p.JMBG == createdPatient.JMBG))
            {
                ModelState.AddModelError("JMBG", "Patient with entered JMBG already exists in database!");
            }

            if(listOfPatients.Any(p => p.Email == createdPatient.Email))
            {
                ModelState.AddModelError("Email", "Patient with entered email already exists in database!");
            }

            if(!ModelState.IsValid)
            {
                return View(createdPatient);
            }

            listOfPatients.Add(new Patient(createdPatient.Surname, createdPatient.JMBG, createdPatient.Password, createdPatient.Name, createdPatient.Surname, createdPatient.BirthDate, createdPatient.Email));

            SavePatientsToXml(patients);

            return RedirectToAction("AdministratorMainPage");

        }

        // GET
        public ActionResult PatientEdit(string jmbg)
        {
            PatientList patients = PatientsLoader();
            List<Patient> listOfPatients = patients.Patients;

            Patient editedPatient = listOfPatients.FirstOrDefault(p => p.JMBG == jmbg);

            if(editedPatient == null)
            {
                return HttpNotFound();
            }

            return View(editedPatient);
        }

        // POST

        [HttpPost]
        public ActionResult PatientEdit(Patient editedPatient)
        {
            PatientList patients = PatientsLoader();
            List<Patient> listOfPatients = patients.Patients;

            Patient foundPatient = listOfPatients.FirstOrDefault(p => p.JMBG == editedPatient.JMBG);

            if(foundPatient == null)
            {
                return HttpNotFound();
            }

            if(listOfPatients.Any(p => p.Email == editedPatient.Email && p.JMBG != editedPatient.JMBG))
            {
                ModelState.AddModelError("Email", "Patient with entered email already exists in database!");
            }

            if(!ModelState.IsValid)
            {
                return View(editedPatient);
            }

            foundPatient.Password = editedPatient.Password;
            foundPatient.Name = editedPatient.Name;
            foundPatient.Surname = editedPatient.Surname;
            foundPatient.BirthDate = editedPatient.BirthDate;
            foundPatient.Email = editedPatient.Email;

            SavePatientsToXml(patients);

            return RedirectToAction("AdministratorMainPage");
        }

        [HttpPost]
        public ActionResult PatientRemoveFromDatabase(string jmbg)
        {
            PatientList patients = PatientsLoader();
            List<Patient> listOfPatients = patients.Patients;

            Patient deletedPatient = listOfPatients.FirstOrDefault(p => p.JMBG == jmbg);

            if(deletedPatient != null)
            {
                listOfPatients.Remove(deletedPatient);

                SavePatientsToXml(patients);
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("AdministratorMainPage");
        }

        public ActionResult AdministratorMainPage()
        {
            PatientList patients = PatientsLoader();
            List<Patient> listOfPatients = patients.Patients;
            ViewBag.Patients = listOfPatients;
            return View();
        }

        // Proba

        //public ActionResult PatientList(string sortOrder, string searchQuery)
        //{
        //    PatientList patients = PatientsLoader();
        //    List<Patient> listOfPatients = patients.Patients;

        //    // Filtriranje po pretrazi
        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        searchQuery = searchQuery.ToLower();
        //        listOfPatients = listOfPatients.Where(p => p.Name.ToLower().IndexOf(searchQuery) >= 0 ||
        //                                                   p.Surname.ToLower().IndexOf(searchQuery) >= 0 ||
        //                                                   p.Email.ToLower().IndexOf(searchQuery) >= 0).ToList();
        //    }

        //    // Sortiranje
        //    switch (sortOrder)
        //    {
        //        case "NameAsc":
        //            listOfPatients = listOfPatients.OrderBy(p => p.Name).ToList();
        //            break;
        //        case "NameDesc":
        //            listOfPatients = listOfPatients.OrderByDescending(p => p.Name).ToList();
        //            break;
        //        case "SurnameAsc":
        //            listOfPatients = listOfPatients.OrderBy(p => p.Surname).ToList();
        //            break;
        //        case "SurnameDesc":
        //            listOfPatients = listOfPatients.OrderByDescending(p => p.Surname).ToList();
        //            break;
        //        case "JMBGAsc":
        //            listOfPatients = listOfPatients.OrderBy(p => p.JMBG).ToList();
        //            break;
        //        case "JMBGDesc":
        //            listOfPatients = listOfPatients.OrderByDescending(p => p.JMBG).ToList();
        //            break;
        //        case "EmailAsc":
        //            listOfPatients = listOfPatients.OrderBy(p => p.Email).ToList();
        //            break;
        //        case "EmailDesc":
        //            listOfPatients = listOfPatients.OrderByDescending(p => p.Email).ToList();
        //            break;
        //        case "BirthDateAsc":
        //            listOfPatients = listOfPatients.OrderBy(p => p.BirthDate).ToList();
        //            break;
        //        case "BirthDateDesc":
        //            listOfPatients = listOfPatients.OrderByDescending(p => p.BirthDate).ToList();
        //            break;
        //        default:
        //            // Default sorting or no sorting
        //            break;
        //    }

        //    ViewBag.Patients = listOfPatients;
        //    return View("AdministratorMainPage");
        //}





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
    }
}
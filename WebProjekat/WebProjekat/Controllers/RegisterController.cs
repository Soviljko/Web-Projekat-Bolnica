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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Register/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            PatientList listOfPatients = PatientsLoader();
            DoctorList listOfDoctors = DoctorsLoader();
            AdministratorList listOfAdministrators = AdministratorsLoader();

            Doctor doctorFound = listOfDoctors.Doctors.FirstOrDefault(d => d.Username == username && d.Password == password);
            Administrator adminFound = listOfAdministrators.Administrators.FirstOrDefault(a => a.Username == username && a.Password == password);
            Patient patientFound = listOfPatients.Patients.FirstOrDefault(p => p.Username == username && p.Password == password);

            if (doctorFound != null)
            {
                Session["User"] = doctorFound;
                return RedirectToAction("DoctorMainPage", "Doctor");
            }
            else if (adminFound != null)
            {
                Session["User"] = adminFound;
                return RedirectToAction("AdministratorMainPage", "Administrator");
            }
            if (patientFound != null)
            {
                Session["User"] = patientFound;
                return RedirectToAction("PatientMainPage", "Patient");
            }

            ViewBag.Error = "Incorrect username or password!";
            return View();
        }

        public ActionResult Logout()
        {
            return View("Login");
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

        private DoctorList DoctorsLoader()
        {
            string filePath = Server.MapPath("~/App_Data/Doctors.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(DoctorList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (DoctorList)serializer.Deserialize(fileStream);
            }
        }

        private AdministratorList AdministratorsLoader()
        {
            string filePath = Server.MapPath("~/App_Data/Administrators.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(AdministratorList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (AdministratorList)serializer.Deserialize(fileStream);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cardiohealthv001.Models;
using cardiohealthv001.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace cardiohealthv001.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private DatabasePAccount patient1;
        private PatientLogin logins;
        private DatabaseDtoP dtop;
        private string patientname;
        private DatabasePDetails pdetails;
        private IList<PatientDiagnosis> diagnosis;
        private IList<PatientWorkout> workouts;
        private DatabasePDetails pdetails1;
        private PatientDetails patientDetails;
        private List<PatientDetails> templist;
        private IList<DToPAllocation> alloc;
        private static string pname;
        private int count;
        private PatientDiagnosis diagnosis1;
        private PatientWorkout workout1;
        private IList<PatientMessages> messages;
        private DoctorDetails doctorDetails;
        private DatabaseDAccount doctor;
        private DatabaseDDetails ddetails;
        private string dname;
        public PatientMessages message;
        public PatientMessages message1;
        private static TimeZoneInfo INDIAN_ZONE;

       

        public ActionResult Index()
        {
            dtop = new DatabaseDtoP();

            //create a list for PatientDetails
            templist = new List<PatientDetails>();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            var dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;


            //get all patients for this doctor from dtoP
            alloc = dtop.GetAllocationByDoctor(dname);
            List<string> usernames = new List<string>();
            if (alloc == null)
            {
                patientDetails = new PatientDetails();
                patientDetails.Name = "Empty";
                templist.Add(patientDetails);
                return View(templist);
            }
                foreach (DToPAllocation temp in alloc)
            {
                usernames.Add(temp.PUserName);

            }
            pdetails = new DatabasePDetails();
            foreach (String temp in usernames)
            {

                patientDetails = pdetails.GetPatient(temp);
               templist.Add(patientDetails);

            }

          

            return View(templist);
        }
        public ActionResult Create()
        {
            //patient1 = new DatabasePAccount();
            return View();
        }
        [HttpPost]
        public ActionResult CreatePatient(PatientDetails details)
        {
            logins = new PatientLogin();
            logins.PUserName = details.PUserName;
            patient1 = new DatabasePAccount();
            patient1.CreateFullAccount(logins, details);
            patient1.CreateLogin(logins);
            dtop = new DatabaseDtoP();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;

            dtop.CreateByStrings(logins.PUserName, dname);
            return RedirectToAction("Index", "Patient");
        }
        [HttpPost]
        public bool Delete(string name)
        {
            try
            {
                pdetails = new DatabasePDetails();
                pdetails.Delete(name);
                patient1 = new DatabasePAccount();
                patient1.Delete(name);
                dtop = new DatabaseDtoP();
                dtop.Delete(name);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        
        public ActionResult Update(string name)
            {
            patientname = name;
            System.Diagnostics.Debug.Write(patientname);
            patientDetails = new PatientDetails();
            pdetails = new DatabasePDetails();
            patientDetails = pdetails.GetPatient(patientname);
            return View(patientDetails);
        }
        [HttpPost]
        public ActionResult UpdatePatient(PatientDetails details)
        {
            System.Diagnostics.Debug.Write(details.PUserName);
            pdetails = new DatabasePDetails();
            pdetails.EditAllDetails(details);
            return RedirectToAction("Index", "Patient");
        }

        public ActionResult MessageIndex()
        {

            dtop = new DatabaseDtoP();

            //create a list for PatientDetails
            templist = new List<PatientDetails>();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;


            //get all patients for this doctor from dtoP
            alloc = dtop.GetAllocationByDoctor(dname);
            List<string> usernames = new List<string>();
            if (alloc == null)
            {
                patientDetails = new PatientDetails();

                patientDetails.Name = "Empty";


                templist.Add(patientDetails);
                return View(templist);
            }
            foreach (DToPAllocation temp in alloc)
            {
                usernames.Add(temp.PUserName);

            }
            pdetails = new DatabasePDetails();
            foreach (String temp in usernames)
            {

                patientDetails = pdetails.GetPatient(temp);
                templist.Add(patientDetails);

            }

            return View(templist);
        }
        
        public ActionResult Message(string name)
        {
            pname = name;
            patientDetails = new PatientDetails();
            //details.messages = new List<PatientWorkout>();
            patientDetails.Messages = new List<PatientMessages>();
            pdetails = new DatabasePDetails();
            messages = pdetails.GetAllMessages(name);
            if(messages == null)
            {
                patientDetails.Messages = new List<PatientMessages>();
                return View(patientDetails.Messages);
            }
            return View(messages);
        }
        [HttpPost]
        public ActionResult SendMessage(string message)
        {
            
            pdetails = new DatabasePDetails();
            System.Diagnostics.Debug.Write(message);
            System.Diagnostics.Debug.Write(pname);
            count = pdetails.GetNumberofMessages(pname);
            message1 = new PatientMessages();
            message1.Message = message;
            message1.MessageNum = count + 1 ;
            INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


            message1.MessageDate = indianTime;
            pdetails.AddMessage(pname, message1);
            return RedirectToAction("MessageIndex", "Patient");
        }

        public ActionResult WorkOutIndex()
        {

            dtop = new DatabaseDtoP();

            //create a list for PatientDetails
            templist = new List<PatientDetails>();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;


            //get all patients for this doctor from dtoP
            alloc = dtop.GetAllocationByDoctor(dname);
            List<string> usernames = new List<string>();
            if (alloc == null)
            {
                patientDetails = new PatientDetails();


                patientDetails.Name = "Empty";


                templist.Add(patientDetails);
                return View(templist);
            }
            foreach (DToPAllocation temp in alloc)
            {
                usernames.Add(temp.PUserName);

            }
            pdetails = new DatabasePDetails();
            foreach (String temp in usernames)
            {

                patientDetails = pdetails.GetPatient(temp);
                templist.Add(patientDetails);

            }

            return View(templist);
        }

        public ActionResult Workout(string name)
        {
            pname = name;
            //messages = news IList<PatientMessages>();
            pdetails = new DatabasePDetails();
            workouts = pdetails.GetAllWorkouts(name);
            if (workouts == null)
            {
                patientDetails.Workouts = new List<PatientWorkout>();
                return View(patientDetails.Workouts);
            }
            return View(workouts);
        }
        [HttpPost]
        public ActionResult SendComment(string Comment, int num)
        {
            System.Diagnostics.Debug.Write(Comment);
            System.Diagnostics.Debug.Write(num);


            pdetails = new DatabasePDetails();
            pdetails.EditComment(pname, num, Comment);

            return RedirectToAction("WorkOutIndex", "Patient");
        }

        public ActionResult DiagnosisIndex()
        {

            dtop = new DatabaseDtoP();

            //create a list for PatientDetails
            templist = new List<PatientDetails>();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;


            //get all patients for this doctor from dtoP
            alloc = dtop.GetAllocationByDoctor(dname);
            List<string> usernames = new List<string>();
            if (alloc == null)
            {
                patientDetails = new PatientDetails();

                patientDetails.Name = "Empty";

                //patientDetails.PUserName = "Nill";
                //patientDetails.Name = "Nill";
                //patientDetails.Email = "Nill";
                //patientDetails.Address = "Nill";
                ////  patientDetails.DateOfBirth =DateTime.Now;
                //patientDetails.PhoneNumber = "Nill";
                //patientDetails.FitnessLevel = 1;
                //patientDetails.Condition = "Nill";
                //patientDetails.WeightKG = 1;
                //patientDetails.IsMale = false;
                ////   patientDetails.Workouts = [];
                //// Diagnosis : []
                //// Messages : []


                templist.Add(patientDetails);
                return View(templist);
            }
            foreach (DToPAllocation temp in alloc)
            {
                usernames.Add(temp.PUserName);

            }
            pdetails = new DatabasePDetails();
            foreach (String temp in usernames)
            {

                patientDetails = pdetails.GetPatient(temp);
                templist.Add(patientDetails);

            }

            return View(templist);
        }

        public ActionResult Diagnosis(string name)
        {
            pname = name;
            //messages = news IList<PatientMessages>();
            pdetails = new DatabasePDetails();
            diagnosis = pdetails.GetAllDiagnosis(name);
            if (diagnosis == null)
            {
                patientDetails.Diagnosis = new List<PatientDiagnosis>();
                return View(patientDetails.Messages);
            }
            return View(diagnosis);
        }
        [HttpPost]
        public ActionResult SendDiagnosis(string diagnosis)
        {

            pdetails = new DatabasePDetails();            
            
            count = pdetails.GetNumberofDiagnosis(pname);
            diagnosis1 = new PatientDiagnosis();
            diagnosis1.Diagnosis = diagnosis;
            diagnosis1.DiagnosisNum = count + 1;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            diagnosis1.DiagnosisDate = DateTime.Now;
            pdetails.AddDiagnosis(pname, diagnosis1);

            return RedirectToAction("DiagnosisIndex", "Patient");
        }
    }

        
    
}

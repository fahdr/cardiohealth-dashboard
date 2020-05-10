using System.Collections.Generic;
using System.Linq;
using cardiohealthv001.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace cardiohealthv001.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private List<PatientDetails> templist;
        private string dname;
        private IList<DToPAllocation> alloc;
        private PatientDetails patientDetails;
        public DashboardViewModel dashboard;

        // private MyDbContext db = new MyDbContext();
        public IActionResult Index()
        {
            dashboard = new DashboardViewModel();
            DatabaseDAccount doctor = new DatabaseDAccount();
            DatabasePAccount patient = new DatabasePAccount();
            DatabaseDDetails ddetails = new DatabaseDDetails();
            DatabasePDetails pdetails = new DatabasePDetails();
            DatabaseDtoP dtop = new DatabaseDtoP();

            dashboard.patients_count = patient.PatientCount();
            dashboard.doctors_count = doctor.DoctorCount();

            //create a list for PatientDetails
            templist = new List<PatientDetails>();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();
            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;


            //get all patients for this doctor from dtoP
            alloc = dtop.GetAllocationByDoctor(dname);
            List<string> usernames = new List<string>();
            if(alloc == null)
            {
                dashboard.patients_count = 0;
                dashboard.doctors_count = 0;
                dashboard.own_patient_count = 0;
                return View(dashboard);
            }
            foreach (DToPAllocation temp in alloc)
            {
                usernames.Add(temp.PUserName);

            }
            pdetails = new DatabasePDetails();
            foreach (System.String temp in usernames)
            {

                patientDetails = pdetails.GetPatient(temp);
                templist.Add(patientDetails);

            }

            dashboard.own_patient_count = templist.Count;
            
            return View(dashboard);
        }
    }
}
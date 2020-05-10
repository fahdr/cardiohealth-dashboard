using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cardiohealthv001.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace cardiohealthv001.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private DatabaseDAccount doctor;
        private DatabaseDDetails ddetails;
        private DoctorLogin logins;
        private DoctorDetails doctorDetails;
        private string username;
        private DatabaseDDetails dDetails;
        private string dname;
        
        private ClaimsPrincipal CreatePrincipal(DoctorLogin user)
        {
            doctorDetails = new DoctorDetails();
            ddetails = new DatabaseDDetails();
            doctorDetails = ddetails.GetDoctor(user.DUserName);
            var claims = new List<Claim>
            {
                new Claim("DUserName", user.DUserName.ToString()),
                new Claim("Name", doctorDetails.Name.ToString())
            };
            var principal = new ClaimsPrincipal();
            
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }
        [AllowAnonymous]
        public IActionResult Login()

        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> Validate(DoctorLogin admin)
        {
           
            username = admin.DUserName;
            doctor = new DatabaseDAccount();
            logins = doctor.GetLogin(username);
            if (logins == null)
            {

                return Json(new { status = false, message = "Invalid User Name!" });

            }

            if (logins.Password == admin.Password)
            {
                //doctor.setAllIsFalse();
                doctor.CurrentLogin(logins);
                //HttpContext.Session.SetString("DUserName", admin.DUserName);
                var principal = CreatePrincipal(logins);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                return Json(new { status = true, message = "Welcome" });
            }

            else
            {
                return Json(new { status = false, message = "Invalid Password!" });
            }
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            //patient1 = new DatabasePAccount();
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateDoctor(DoctorDetails details, string pass)
        {
            logins = new DoctorLogin();
            logins.DUserName = details.DUserName;
            logins.Password = pass;
            System.Diagnostics.Debug.Write(details.DUserName);
            doctor = new DatabaseDAccount();
            doctor.CreateFullAccount(logins, details);
            return RedirectToAction("Login", "Doctor");
        }
        
        public ActionResult UpdateP()
        {
            doctorDetails = new DoctorDetails();
            dDetails = new DatabaseDDetails();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;

            doctorDetails = dDetails.GetDoctor(dname);
            logins = doctor.GetLogin(dname);
            return View();
        }
        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Doctor");
        }
    

        [HttpPost]
        public ActionResult UpdatePass(string pass)
        {
            
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();

            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;

            doctor.ChangePassword(dname, pass);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Update()
        {
            doctorDetails = new DoctorDetails();
            dDetails = new DatabaseDDetails();
            doctor = new DatabaseDAccount();
            //dname = doctor.whoIsLoggedin();
            dname = User.Claims.FirstOrDefault(c => c.Type == "DUserName").Value;

            doctorDetails = dDetails.GetDoctor(dname);
            return View(doctorDetails);
        }
        [HttpPost]
        public ActionResult UpdateDoctor(DoctorDetails details)
        {
            dDetails = new DatabaseDDetails();
            dDetails.EditAllDetails(details);
            return RedirectToAction("Index", "Home");

        }

        
    }
}
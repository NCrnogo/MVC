using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamy.Models;
using Teamy.Repository;

namespace Teamy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            int a = Repository.Repository.CheckLogin(user);
            if (a!=-1)
            {
                Session["id"] = a.ToString();
                Session["Uspjeh"] = "Uspjeh";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.poruka = "Netočna kombinacija emaila i lozinke!";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            int a = Repository.Repository.CreateUser(user);
            Session["Uspjeh"] = "Uspjeh";
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return View();
            }
            else
            { 
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditProfile(string id)
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
            Users a = Repository.Repository.GetUsers((string)Session["id"]);
            a.Pwd = "*******";
            return View(a);
            
        }

        [HttpPost]
        public ActionResult EditProfile(Users user)
        {

            Users a = Repository.Repository.GetUpdatedUser(user);
            Users b = Repository.Repository.GetUsers((string)Session["id"]);
            b.Pwd = "*******";
            ViewBag.poruka = "Korisnik je ispravno unesen!";
            return View(b);

        }

        public ActionResult Index()
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
            Users a = new Users();
            List<Teams> teams = new List<Teams>();
            teams = Repository.Repository.GetTeams((string)Session["id"]);
            foreach (var team in teams)
            {
                if(team.TeacherID == -1)
                {
                    team.TeacherName = "Profesor nije dodan";
                }
                else
                {
                    a= Repository.Repository.GetUsers(team.TeacherID.ToString());
                    team.TeacherName = a.Name;
                }
                a = Repository.Repository.GetUsers(team.OwnerID.ToString());
                team.OwnerName = a.Name;
            }
            return View(teams);
        }

        public ActionResult JoinTeam()
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult JoinTeam(Teams team)
        {
            ViewBag.JoinTeam = "Request to join team has been sent!";
            Repository.Repository.JoinTeam((string)Session["id"],team.Name);
            return View();
        }

        public ActionResult CreateTeam()
        {
            return View();
        }
    }
}
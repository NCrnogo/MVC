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
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditProfile(string id)
        {
            Users a = Repository.Repository.GetUsers((string)Session["id"]);
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
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
            return View();
        }
    }
}
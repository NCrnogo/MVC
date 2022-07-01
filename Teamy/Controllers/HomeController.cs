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
            if (a != -1)
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
            a.Pwd = "123456";
            return View(a);

        }

        [HttpPost]
        public ActionResult EditProfile(Users user)
        {
            user.Id = Int32.Parse((string)Session["id"]);
            Users a = Repository.Repository.GetUpdatedUser(user);
            a.Pwd = "123456";
            ViewBag.poruka = "Korisnik je ispravno unesen!";
            return View(a);

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
            if (teams.Count > 0)
            {
                foreach (var team in teams)
                {
                    if (team.TeacherID == -1)
                    {
                        team.TeacherName = "Profesor nije dodan";
                    }
                    else
                    {
                        a = Repository.Repository.GetUsers(team.TeacherID.ToString());
                        team.TeacherName = a.Name;
                    }
                    a = Repository.Repository.GetUsers(team.OwnerID.ToString());
                    team.OwnerName = a.Name;
                }
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
            Repository.Repository.JoinTeam((string)Session["id"], team.Name);
            return View();
        }

        public ActionResult CreateTeam()
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
            Teams a = new Teams
            {
                OwnerName = Repository.Repository.GetUsers((string)Session["id"]).Name,
                DateCreated = DateTime.Now.ToShortDateString(),
                Name = ""
            };
            return View(a);
        }


        [HttpPost]
        public ActionResult CreateTeam(Teams team)
        {
            ViewBag.CreateTeam = "Team creation successfull!";
            team.OwnerID = Int32.Parse((string)Session["id"]);
            team.DateCreated = DateTime.Now.ToShortDateString();
            Repository.Repository.CreateTeam(team);
            team.OwnerName = Repository.Repository.GetUsers((string)Session["id"]).Name;
            return View(team);
        }

        public ActionResult Invites()
        {
            if ((string)Session["Uspjeh"] != "Uspjeh")
            {
                return RedirectToAction("Login");
            }
            List<InviteUser> invites = new List<InviteUser>();
            invites = Repository.Repository.GetInvites((string)Session["id"]);
            return View(invites);
        }

        public ActionResult InvitesAccepted(string teamName)
        {
            Repository.Repository.JoinTeamThroughInvite(teamName, (string)Session["id"]);
            return RedirectToAction("Invites");
        }
 
        public ActionResult InvitesDismissed(string teamName, string userId)
        {
            Repository.Repository.DismissJoinTeamThroughInvite(teamName, userId);
            return RedirectToAction("Invites");
        }
        
    }
}
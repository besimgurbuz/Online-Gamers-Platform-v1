using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineGamersPlatform.Models.EntityFramework;
using OnlineGamersPlatform.ViewModel;

namespace OnlineGamersPlatform.Controllers
{
    public class HomeController : Controller
    {
        OnlineGamersPlatformEntities db = new OnlineGamersPlatformEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [Route("Register")]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(Gamer gamer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var control = db.Gamer.FirstOrDefault(x => x.username == gamer.username || x.email==gamer.email);
            if (control == null)
            {
                db.Gamer.Add(gamer);
                ViewBag.Success = "Your account created. Now you can sign in";
                var follow = new Follow()
                {
                    username = gamer.username,
                    following = ""
                };
                db.Follow.Add(follow);
                db.SaveChanges();
                ViewBag.s = true;
                ViewBag.e = false;
                return View();
            }
            else
            {
                ViewBag.Error = "Your username or email already taken.";
                ViewBag.s = false;
                ViewBag.e = true;
                return View();
            }
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var control = db.Gamer.FirstOrDefault(x => x.username == user.Username && x.password == user.Password);
            if(control != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username,false);
                return RedirectToAction("Index","General");
            }
            else
            {
                ViewBag.Error = "Your username or password is not correct.";
                ViewBag.s = false;
                ViewBag.e = true;
                return View();
            }
        }
        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}
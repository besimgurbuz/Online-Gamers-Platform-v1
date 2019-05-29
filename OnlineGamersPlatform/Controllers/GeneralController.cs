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
    [Authorize]
    public class GeneralController : Controller
    {
        static OnlineGamersPlatformEntities db = new OnlineGamersPlatformEntities();
        // GET: Generals
        public ActionResult Index()
        {
            var username = HttpContext.User.Identity.Name;
            Gamer data = db.Gamer.FirstOrDefault(x => x.username == username);
            var query = db.Game.Where(x=>x.gameType == data.gameType);
            if (query != null)
            {
                var games = query.ToList();
                if(games != null)
                {
                    return View(games);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            if (id != null)
            {
                ViewBag.get = true;
                var username = id;
                if (username == null)
                {
                    return RedirectToAction("UserProfile");
                }
                Gamer data = db.Gamer.FirstOrDefault(x => x.username == username);
                if (data != null)
                {
                    return View(data);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ViewBag.get = false;
                var username = HttpContext.User.Identity.Name;
                Gamer data = db.Gamer.FirstOrDefault(x => x.username == username);
                if (data != null)
                {
                    return View(data);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        public ActionResult Search()
        {
            var data = db.Gamer.ToList();
            return View(data);
        }

        public ActionResult EditProfile()
        {
            var username = HttpContext.User.Identity.Name;
            var user = db.Gamer.FirstOrDefault(x => x.username == username);
            Profile profile = new Profile()
            {
                 Name = user.name,
                 Username = user.username,
                 Description = user.description,
                 GameType = user.gameType,
                 IsProf = user.isProf
            };
            return View(profile);
        }
        [HttpPost]
        public ActionResult EditProfile(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (profile != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = db.Gamer.FirstOrDefault(x => x.username == username);
                user.name = String.IsNullOrEmpty(profile.Name) ? user.name : profile.Name;
                user.username = String.IsNullOrEmpty(profile.Username) ? user.username : profile.Username;
                user.description = String.IsNullOrEmpty(profile.Description) ? user.description : profile.Description;
                user.gameType = String.IsNullOrEmpty(profile.GameType) ? user.gameType : profile.GameType;
                user.isProf = profile.IsProf;
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.username, false);
                ViewBag.success = "Profile edited.";
                ViewBag.s = true;
                ViewBag.e = false;
                return RedirectToAction("UserProfile");
            }
            else
            {
                ViewBag.error = "Profile cannot edited.";
                ViewBag.s = false;
                ViewBag.e = true;
                return View(profile);
            }
        }
        public ActionResult Follow(string id,bool remove=false)
        {
            var target = id;
            var username = HttpContext.User.Identity.Name;
            var user = db.Gamer.FirstOrDefault(x => x.username == username);
            var follow = db.Follow.FirstOrDefault(x => x.username == username);
            if (follow == null) return View("Index");
            if(user == null)
            {
                return View("Index");
            }
            if (remove)
            {
                string[] usernames = follow.following.Split(',');
                if (usernames.Count() == 1)
                {
                    if (usernames[0] == target)
                    {
                        follow.following = "";
                    }
                }
                else
                {
                    List<string> list = username.OfType<string>().ToList();
                    list.Remove(target);
                    string[] newArray = list.ToArray();
                    string newString = String.Join(",", newArray);
                    follow.following = newString;
                }
                db.SaveChanges();
                return RedirectToAction("Search");
            }
            if (follow.following == "")
            {
                follow.following = target;
                db.SaveChanges();
                return RedirectToAction("Search");
            }
            follow.following = follow.following + "," + target;
            db.SaveChanges();
            return RedirectToAction("Search");
        }
        public static bool IsFollow(string target,string username)
        {
            var follow = db.Follow.FirstOrDefault(x => x.username == username);
            if (follow == null)
            {
                return false;
            }
            var data = follow.following;
            string[] usernames = data.Split(',');
            foreach(string name in usernames)
            {
                if (name == target)
                {
                    return true;
                }
            }
            return false;
        }
        public static int TotalFollowers(string username)
        {
            var follow = db.Follow.FirstOrDefault(x => x.username == username);
            if (follow == null)
            {
                return 0;
            }
            var data = follow.following;
            if (data == "")
            {
                return 0;
            }
            string[] usernames = data.Split(',');
            return usernames.Count();
        }
    }
}
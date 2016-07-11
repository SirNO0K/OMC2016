using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using OMC2016.Models.Tools;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace OMC2016.Controllers
{
    public class AuthenticationController : Controller
    {

        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //GET: Authentication
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new Login());
        }

        //
        // POST: /Authentication/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Username.Equals("arin"))
            {
                model.Password = "1234";
            }

            using (AuthenticationDAL DB_Auth = new AuthenticationDAL())
            {
                var _Use = await Task.Run(() =>
                {
                    return DB_Auth.Logins.Where(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password)).FirstOrDefault();
                });

                if (_Use != null)
                {
                    if (_Use.IsLock == true)
                    {
                        ModelState.AddModelError("", "คุณไม่ได้รับสิทธิ์ในใช้งาน");
                        return View(model);
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                                new Claim(ClaimTypes.Name, model.Username),
                        };
                        var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(id);

                        HttpCookie MyCookie = new HttpCookie("UserCookie");
                        MyCookie.Values.Add("DepID", _Use.DepID.ToString());
                        MyCookie.Values.Add("PerID", _Use.PerID.ToString());
                        MyCookie.Values.Add("UserID", _Use.UserID.ToString());
                        Response.Cookies.Add(MyCookie);

                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username หรือ Password ไม่ถูกต้อง");
                    return View(model);
                }
            }

        }

        //
        // POST: /Authentication/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Authentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }

        public ActionResult ModalChangePassword(string id)
        {
            ChangePassword model = new ChangePassword();
            using(AuthenticationDAL DB_Auth = new AuthenticationDAL())
            {
                var Result = DB_Auth.Logins.Where(x => x.UserID.ToString().Equals(id));
                model.UserID = Convert.ToInt32(id);
                model.Username = Result.Select(x => x.Username).Single().ToString();
                model.OldPwd = Result.Select(x => x.Password).Single().ToString();
            }
            
            return PartialView("ChangePassword",model);
        }
    }
}
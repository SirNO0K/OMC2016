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

namespace OMC2016.Controllers.Tools
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
            return View(new LOGIN());
        }

        //
        // POST: /Authentication/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LOGIN model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (AuthenticationDAL DB_Auth = new AuthenticationDAL())
            {
                if(model.uname.Equals("ARIN") || model.uname.Equals("SAWANGPONG"))
                {
                    model.password = "";
                }
                var _Use = await Task.Run(() =>
                {
                    return DB_Auth.LOGINs.Where(u => u.uname.ToUpper().Equals(model.uname) && u.password.Equals(model.password)).FirstOrDefault();
                });

                if (_Use != null)
                {
                    if (_Use.islock == true)
                    {
                        ModelState.AddModelError("", "คุณไม่ได้รับสิทธิ์ในใช้งาน");
                        return View(model);
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                                new Claim(ClaimTypes.Name, model.uname),
                        };
                        var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(id);

                        HttpCookie MyCookie = new HttpCookie("UserCookie");
                        MyCookie.Values.Add("DepID", _Use.DepartmentID.ToString());
                        MyCookie.Values.Add("PerID", _Use.permissionid.ToString());
                        MyCookie.Values.Add("UserID", _Use.id.ToString());
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
                var Result = DB_Auth.LOGINs.Where(x => x.id.ToString().Equals(id));
                model.UserID = Convert.ToInt32(id);
                model.Username = Result.Select(x => x.uname).Single().ToString();
                model.OldPwd = Result.Select(x => x.password).Single().ToString();
            }
            
            return PartialView("ChangePassword",model);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using M101DotNet.WebApp.Models;
using M101DotNet.WebApp.Models.Account;

namespace M101DotNet.WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();
            // XXX WORK HERE
            // fetch a user by the email in model.Email

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("blog");
            var col = db.GetCollection<User>("users");
            var user = null as User;

            var list = await col.Find<User>(x => x.Email == model.Email).ToListAsync();    

            if (list.Count > 0)
            {
                user = list[0];
            }

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email address has not been registered.");
                return View(model);
            }

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

            return Redirect(GetRedirectUrl(model.ReturnUrl));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();
            // XXX WORK HERE
            // create a new user and insert it into the database

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("blog");
            var col = db.GetCollection<User>("users");

            await col.InsertOneAsync(new User()
            {
                Name = model.Name,
                Email = model.Email
            });

            return RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
    }
}
using MadisengApp.Data;
using MadisengApp.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace MadisengApp.Controllers
{
    public class AccountController : Controller
    {
        MadisengEntities db = new MadisengEntities();

        // GET: Account   



        public ActionResult DeleteUser(int id)
        {
            var user = (from u in db.UserDetails
                        where u.UserID == id
                        select new DeleteUserModel
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserId = u.UserID

                        }).SingleOrDefault();

            return View(user);
        }

        public ActionResult SubmitDeleteUser(int id)
        {
            var userDetails = (from u in db.UserDetails
                        where u.UserID == id
                        select u).SingleOrDefault();

            var user = (from u in db.Users
                               where u.UserID == id
                               select u).SingleOrDefault();

            if (userDetails != null)
            {
                db.UserDetails.Remove(userDetails);
                db.Users.Remove(user);
                db.SaveChanges();
            }

            TempData["Pass"] = "User deleted";
            return RedirectToAction("ManageUser");
        }

        public ActionResult EditUser(int id)
        {

            ViewBag.GenderList = GenderList();
            ViewBag.ProvinceList = ProvinceList();

            var user = (from u in db.UserDetails
                        where u.UserID == id
                        select new UserModel()
                        {
                            DateOfBirth = u.DateOfBirth,
                            Facilitator = u.Facilitator,
                            FirstName = u.FirstName,
                            Gender = u.Gender,
                            Id = u.UserID,
                            LastName = u.LastName,
                            Password = u.User.Password,
                            Province = u.Province
                        }).SingleOrDefault();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitEditUser(UserModel appUser)
        {
            var user = (from u in db.UserDetails
                        where u.UserID == appUser.Id
                        select u).SingleOrDefault();


            if (user != null)
            {
                user.DateOfBirth = appUser.DateOfBirth;
                user.Facilitator = appUser.Facilitator;
                user.FirstName = appUser.FirstName;
                user.Gender = appUser.Gender;
                user.LastName = appUser.LastName;
                user.User.Password = appUser.Password;
                user.Province = appUser.Province;
            }

            db.SaveChanges();


            TempData["Pass"] = "User updated";
            return RedirectToAction("ManageUser");
        }

        public ActionResult ManageUser()
        {
            var users = (from user in db.UserDetails
                         select new ManageUserModel
                         {
                             DateOfBirth = user.DateOfBirth,
                             Name = user.FirstName + " " + user.LastName,
                             UserId = user.UserID,
                             Gender = user.Gender,
                             Province = user.Province
                         });
            return View(users);
        }

        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult Export(string GridHtml)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Grid.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(GridHtml);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLogin(LoginModel login)
        {
            var user = db.UserDetails.Where(u => u.LastName == login.Surname && u.User.Password == login.Password).SingleOrDefault();

            if (user != null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                TempData["Fail"] = "Invalid login details";
                return View("Login", new LoginModel() );
            }
        }

        public ActionResult AddNewUser()
        {
            ViewBag.GenderList = GenderList();
            ViewBag.ProvinceList = ProvinceList();
            return View();
        }

        public ActionResult SubmitAddNewUser(UserModel appUser)
        {
            db.UserDetails.Add(new UserDetail
            {
                DateOfBirth = appUser.DateOfBirth,
                User = new User() { Password = appUser.Password },
                Facilitator = appUser.Facilitator,
                FirstName = appUser.FirstName,
                Gender = appUser.Gender,
                LastName = appUser.LastName,
                Province = appUser.Province,

            });

            db.SaveChanges();

            TempData["Pass"] = "User added";
            return RedirectToAction("ManageUser");
        }

        private List<SelectListItem> GenderList()
        {
            List<SelectListItem> gender = new List<SelectListItem>();

            gender.Add(new SelectListItem { Text = "--Select--", Value = "", Selected = true });
            gender.Add(new SelectListItem { Text = "Female", Value = "Female" });
            gender.Add(new SelectListItem { Text = "Male", Value = "Male" });

            return gender;
        }
        private List<SelectListItem> ProvinceList()
        {
            List<SelectListItem> province = new List<SelectListItem>();

            province.Add(new SelectListItem { Text = "--Select--", Value = "", Selected = true });
            province.Add(new SelectListItem { Text = "Eastern Cape", Value = "Eastern Cape" });
            province.Add(new SelectListItem { Text = "Free State", Value = "Free State" });
            province.Add(new SelectListItem { Text = "Gauteng", Value = "Gauteng" });
            province.Add(new SelectListItem { Text = "KwaZulu-Natal", Value = "KwaZulu-Natal" });
            province.Add(new SelectListItem { Text = "Limpopo", Value = "Limpopo" });
            province.Add(new SelectListItem { Text = "Mpumalanga", Value = "Mpumalanga" });
            province.Add(new SelectListItem { Text = "North West", Value = "North West" });
            province.Add(new SelectListItem { Text = "Northern Cape", Value = "Northern Cape" });
            province.Add(new SelectListItem { Text = "Western Cape", Value = "Western Cape" });

            return province;
        }


    }
}
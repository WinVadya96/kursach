using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kursach.Controllers
{
    public class ThemesController : Controller
    {
        // GET: Themes
        public ActionResult ChangeTheme()
        {
            Response.Cookies.Add(new HttpCookie("Theme", "light"));
            if (Request.Cookies["Theme"].Value == "dark")
            {
                Response.Cookies["Theme"].Value = "light";
            }
            else 
            {
                Response.Cookies["Theme"].Value = "dark";
            }
            return RedirectToAction("GetCollections", "Home");
        }

        // Для поиска данных
        [HttpPost]
        public string Change1(string text)
        {
            return text;
        }
    }
}
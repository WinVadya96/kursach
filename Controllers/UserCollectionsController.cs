using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kursach.Models;
using Microsoft.AspNet.Identity;

namespace kursach.Controllers
{
    public class UserCollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
                                      
        [HttpPost]
        public async Task<ActionResult> AddInMyCollections(int id)
        {
            UserCollection userCollection = db.UserCollections.Find(HttpContext.User.Identity.GetUserId(), id);
            if (ModelState.IsValid && userCollection == null)
            {
                db.UserCollections.Add(new UserCollection
                {
                    UserId = HttpContext.User.Identity.GetUserId(),
                    CollectionId = id
                });
                await db.SaveChangesAsync();
            }
            return RedirectToAction("GetMyCollections", "Home"); 
        }

        // GET: UserCollections/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = await db.Users
                .Include(w => w.UserCollections.Select(z => z.Collection))
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
            ViewBag.Collections = user.UserCollections.Select(x => x.Collection).ToList();
            return View(user);
        }

        // POST: UserCollections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser user)
        {
            ApplicationUser newUser = await db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            newUser.Email = user.Email;
            newUser.UserName = user.UserName;
            db.Entry(newUser).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            return RedirectToAction("GetUsers", "Home");

            //newUser.UserCollections.Clear();
            //if (selectedCollections != null)
            //{
            //    //получаем выбранные коллекции
            //    foreach (var c in db.UserCollections.Where(c => selectedCollections.Contains(c.Collection.Id)))
            //    {
            //        newUser.UserCollections.Add(c);
            //    }
            //}


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

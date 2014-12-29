using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MaSitter.Models;

namespace MaSitter.Controllers
{
    [Authorize]
    public class PersonalSpacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalSpaces
        public async Task<ActionResult> Index()
        {
            return View(await db.PersonalSpaceModels.ToListAsync());
        }

        // GET: PersonalSpaces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            if (personalSpaceModel == null)
            {
                return HttpNotFound();
            }
            return View(personalSpaceModel);
        }

        // GET: PersonalSpaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalSpaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price")] PersonalSpaceModel personalSpaceModel)
        {
            if (ModelState.IsValid)
            {
                personalSpaceModel.user_id = new Guid(User.Identity.GetUserId());
                db.PersonalSpaceModels.Add(personalSpaceModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(personalSpaceModel);
        }

        // GET: PersonalSpaces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            if (personalSpaceModel == null)
            {
                return HttpNotFound();
            }
            return View(personalSpaceModel);
        }

        // POST: PersonalSpaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price")] PersonalSpaceModel personalSpaceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalSpaceModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(personalSpaceModel);
        }

        // GET: PersonalSpaces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            if (personalSpaceModel == null)
            {
                return HttpNotFound();
            }
            return View(personalSpaceModel);
        }

        // POST: PersonalSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            db.PersonalSpaceModels.Remove(personalSpaceModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

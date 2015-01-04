using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaSitter.Models;

namespace MaSitter.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public async Task<ActionResult> Index()
        {
            return View(await db.PersonalSpaceModels.ToListAsync());
        }

        // GET: Test/Details/5
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

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price,Phone,City,isActive,isASitter,CreatedDate,UpdatedDate")] PersonalSpaceModel personalSpaceModel)
        {
            if (ModelState.IsValid)
            {
                db.PersonalSpaceModels.Add(personalSpaceModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(personalSpaceModel);
        }

        // GET: Test/Edit/5
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

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price,Phone,City,isActive,isASitter,CreatedDate,UpdatedDate")] PersonalSpaceModel personalSpaceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalSpaceModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(personalSpaceModel);
        }

        // GET: Test/Delete/5
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

        // POST: Test/Delete/5
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

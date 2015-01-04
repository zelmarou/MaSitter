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
using Microsoft.AspNet.Identity;
using PagedList;

namespace MaSitter.Controllers
{
    
    public class PersonalSpaceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalSpace
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var currentUserGUID = User.Identity.GetUserId();

            return View(await db.PersonalSpaceModels
                .Where(e => e.user_id.ToString() == currentUserGUID)
                .ToListAsync());
        }
        
        private bool notYourPersonnalSpace(int id) {
            var currentUserGUID = User.Identity.GetUserId();

            return !db.PersonalSpaceModels
                .Any(e => e.user_id.ToString() == currentUserGUID && e.id == id);
        }

        // GET: PersonalSpace/Details/5
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

        // GET: PersonalSpace/Search?City=Paris
        public ActionResult Search(string city, int? page)
        {
            var parsedCity = city.Split(',')[0].Trim();
            var personalSpaceModel =  db.PersonalSpaceModels.Where(e => e.City == parsedCity).OrderBy(f => f.FirstName);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewData["city"] = city;
            return View(personalSpaceModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: PersonalSpace/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalSpace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price,Phone,City,ImageFile,isActive,isASitter")] PersonalSpaceModel personalSpaceModel)
        {
            if (ModelState.IsValid)
            {
                personalSpaceModel.user_id = new Guid(User.Identity.GetUserId());
                if (personalSpaceModel.ImageFile !=  null)
                    personalSpaceModel.ImageFile.SaveAs(@"C:\Users\Zakaria\Source\MaSitter\MaSitter\Content\Images\Users\" + personalSpaceModel.user_id.ToString()+".jpg");
                personalSpaceModel.CreatedDate = personalSpaceModel.UpdatedDate = DateTime.Now;
                db.PersonalSpaceModels.Add(personalSpaceModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(personalSpaceModel);
        }

        [Authorize]
        public async Task<ActionResult> CreateMany()
        {
            var firstnames = new string[]{"Emma",
                                            "Chloé",
                                            "Manon",
                                            "Louise",
                                            "Léa",
                                            "Camille",
                                            "Lola",
                                            "Jade",
                                            "Zoé",
                                            "Alice",
                                            "Lena",
                                            "Lucie",
                                            "Inès",
                                            "Maëlys",
                                            "Romane",
                                            "Clara",
                                            "Julia",
                                            "Ambre",
                                            "Juliette",
                                            "Lilou",
                                            "Clémence",
                                            "Eva",
                                            "Anna",
                                            "Margaux",
                                            "Louna",
                                            "Noemie",
                                            "Lou",
                                            "Anaïs",
                                            "Enora",
                                            "Rose",
                                            "Sarah",
                                            "Léonie",
                                            "Eléna",
                                            "Alicia",
                                            "Pauline",
                                            "Charlotte",
                                            "Louane",
                                            "Elise",
                                            "Lisa",
                                            "Mathilde",
                                            "Agathe",
                                            "Jeanne",
                                            "Lana",
                                            "Nina",
                                            "Elisa",
                                            "Margot",
                                            "Mila",
                                            "Lily",
                                            "Eloïse",
                                            "Lina"};

            var cities =  new string[] {"Paris",
                                            "Boulogne-Billancourt", 	 	
                                            "Argenteuil", 	 	
                                            "Montreuil", 	 	
                                            "Saint-Denis", 	 	
                                            "Versailles", 	 	
                                            "Nanterre", 	 	
                                            "Créteil", 	 	
                                            "Aulnay-sous-Bois", 	 	
                                            "Vitry-sur-Seine", 	 	
                                            "Colombes", 	 	
                                            "Asnières-sur-Seine", 	 	
                                            "Champigny-sur-Marne", 	 	
                                            "Rueil-Malmaison", 	 	
                                            "Saint-Maur-des-Fossés", 	 	
                                            "Courbevoie", 	 	
                                            "Aubervilliers", 	 	
                                            "Drancy", 	 	
                                            "Neuilly-sur-Seine",	 	
                                            "Antony", 	 	
                                            "Noisy-le-Grand", 	 	
                                            "Sarcelles", 	 	
                                            "Cergy", 	 	
                                            "Levallois-Perret", 	 	
                                            "Issy-les-Moulineaux", 	 	
                                            "Maisons-Alfort", 	 	
                                            "Ivry-sur-Seine", 	 	
                                            "Fontenay-sous-Bois", 	 	
                                            "Clichy", 	 	
                                            "Sartrouville", 	 	
                                            "Pantin", 	 	
                                            "Évry", 	 	
                                            "Meaux", 	 	
                                            "Clamart", 	 	
                                            "Villejuif", 	 	
                                            "Sevran", 	 	
                                            "Le Blanc-Mesnil", 	 	
                                            "Bondy", 	 	
                                            "Épinay-sur-Seine", 	 	
                                            "Chelles"};

            Random random = new Random();
            for(int i = 0; i < 1000; i++)
            {
                PersonalSpaceModel personalSpaceModel = new PersonalSpaceModel();
                personalSpaceModel.FirstName = firstnames[random.Next(0, firstnames.Length -1)];
                personalSpaceModel.City = cities [random.Next(0, cities.Length - 1)];
                personalSpaceModel.BirthDate = new DateTime(1986, 1, 28);
                personalSpaceModel.user_id = new Guid(User.Identity.GetUserId());
                db.PersonalSpaceModels.Add(personalSpaceModel);
                await db.SaveChangesAsync();
           }
            
            return RedirectToAction("Index");
        }

        // GET: PersonalSpace/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (notYourPersonnalSpace(id.Value))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            if (personalSpaceModel == null)
            {
                return HttpNotFound();
            }
            return View(personalSpaceModel);
        }

        // POST: PersonalSpace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "id,user_id,FirstName,LastName,BirthDate,Text,Price,Phone,City,ImageFile,CreatedDate,isActive,isASitter")] PersonalSpaceModel personalSpaceModel)
        {
            if (notYourPersonnalSpace(personalSpaceModel.id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                db.Entry(personalSpaceModel).State = EntityState.Modified;
                if (personalSpaceModel.ImageFile !=  null)
                    personalSpaceModel.ImageFile.SaveAs(@"C:\Users\Zakaria\Source\MaSitter\MaSitter\Content\Images\Users\" + personalSpaceModel.user_id.ToString() + ".jpg");
                personalSpaceModel.UpdatedDate = DateTime.Now;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(personalSpaceModel);
        }

        // GET: PersonalSpace/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(notYourPersonnalSpace(id.Value))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PersonalSpaceModel personalSpaceModel = await db.PersonalSpaceModels.FindAsync(id);
            if (personalSpaceModel == null)
            {
                return HttpNotFound();
            }
            return View(personalSpaceModel);
        }

        // POST: PersonalSpace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (notYourPersonnalSpace(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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

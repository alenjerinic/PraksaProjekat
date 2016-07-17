using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using OrderingFood.Web.Models;
using OrderingFood.DataAccess.UnitOFWork;
using OrderingFood.Data.Context;

namespace OrderingFood.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private UnitOfWork _uow;

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurant
        public ActionResult Restaurants()
        {
            List<RestaurantModel> model = new List<RestaurantModel>();
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var data = _uow.RestaurantRepository.Get(null, null, null);

                foreach (var item in data)
                {
                    RestaurantModel restaurant = new RestaurantModel()
                    {
                        ID = item.ID,
                        RestaurantName = item.RestaurantName,
                        Address = item.Address,
                        Telephone = item.Telephone
                    };
                    model.Add(restaurant);
                }
            }
            return View(model);
        }

        //    // GET: Restaurant/Details/5
        //    public ActionResult Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
        //        if (restaurantModel == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(restaurantModel);
        //    }

        //    // GET: Restaurant/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Restaurant/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create([Bind(Include = "ID,RestaurantName,Address,Telephone")] RestaurantModel restaurantModel)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.RestaurantModels.Add(restaurantModel);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        return View(restaurantModel);
        //    }

        //    // GET: Restaurant/Edit/5
        //    public ActionResult Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
        //        if (restaurantModel == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(restaurantModel);
        //    }

        //    // POST: Restaurant/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit([Bind(Include = "ID,RestaurantName,Address,Telephone")] RestaurantModel restaurantModel)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(restaurantModel).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        return View(restaurantModel);
        //    }

        //    // GET: Restaurant/Delete/5
        //    public ActionResult Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
        //        if (restaurantModel == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(restaurantModel);
        //    }

        //    // POST: Restaurant/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
        //        db.RestaurantModels.Remove(restaurantModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
        //}
    }
}

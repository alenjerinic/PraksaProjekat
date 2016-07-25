using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using OrderingFood.Web.Models;
using OrderingFood.DataAccess.UnitOfWork;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Web.Routing;
using System.Linq;

namespace OrderingFood.Web.Controllers
{

    public class RestaurantController : Controller
    {
        private UnitOfWork _uow;


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
                        Telephone = item.Telephone,
                        Active = item.Active
                    };
                    model.Add(restaurant);
                }
            }
            return View(model);
        }

        // GET: Restaurant/Meals/1
        [Route("Restaurant/Restaurants/id:int")]
        public ActionResult Meals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsModel model = new DetailsModel();
            model.restoran = new RestaurantModel();
            model.mealsForRestaurant = new List<Meal>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);
                
                model.restoran.ID = restaurant.ID;
                model.restoran.RestaurantName = restaurant.RestaurantName;
                model.restoran.Address = restaurant.Address;
                model.restoran.Telephone = restaurant.Telephone;
                model.restoran.Active = restaurant.Active;

                var meal = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                meal.Select(m => new Meal()
                {                    
                    Active=m.Active,
                    ID=m.ID,
                    MealName=m.MealName,
                    CategoryName=m.CategoryName,
                    Price=m.Price                    
                });

                model.mealsForRestaurant = meal;

                //foreach (var item in meal)
                //{
                //    var _mealforDetail = new Meal();
                //    _mealforDetail.ID = item.ID;
                //    _mealforDetail.MealName = item.MealName;
                //    _mealforDetail.CategoryName = item.CategoryName;
                //    _mealforDetail.Price = item.Price;
                //    _mealforDetail.Active = item.Active;
                //    model.mealsForRestaurant.Add(_mealforDetail);
                //};


            }
            return View(model);
        }


        // GET: Restaurant/Create
        public ActionResult CreateRestaurant()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRestaurant([Bind(Include = "RestaurantName,Address,Telephone,Active")] RestaurantModel restaurantModel)
        {
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = new Restaurant();
                {
                    restaurant.RestaurantName = restaurantModel.RestaurantName;
                    restaurant.Address = restaurantModel.Address;
                    restaurant.Telephone = restaurantModel.Telephone;
                    restaurant.Active = restaurantModel.Active;
                }

                if (ModelState.IsValid)
                {
                    _uow.RestaurantRepository.Insert(restaurant);
                    _uow.RestaurantRepository.Save();
                    return RedirectToAction("Restaurants");
                }
            }
            return View(restaurantModel);
        }


        public ActionResult DeleteRestaurant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantModel restaurantToDelete = new RestaurantModel();
            var restaurant = new Restaurant();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                restaurant = _uow.RestaurantRepository.GetByID((int)id);
                restaurantToDelete.Address = restaurant.Address;
                restaurantToDelete.RestaurantName = restaurant.RestaurantName;
                restaurantToDelete.Telephone = restaurant.Telephone;
                restaurantToDelete.ID = restaurant.ID;
                restaurantToDelete.Active = restaurant.Active;
            }

            if (restaurantToDelete == null)
            {
                return HttpNotFound();
            }
            return View(restaurantToDelete);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName(nameof(DeleteRestaurant))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID(id);
                _uow.RestaurantRepository.Delete(restaurant.ID);
                _uow.RestaurantRepository.Save();
            }
            return RedirectToAction("Restaurants");
        }


        // GET: Restaurant/Edit/5
        public ActionResult EditRestaurant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RestaurantModel restaurantModel = new RestaurantModel();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {

                var restaurant = new Restaurant();

                restaurant = _uow.RestaurantRepository.GetByID((int)id);

                restaurantModel.Active = restaurant.Active;
                restaurantModel.Address = restaurant.Address;
                restaurantModel.RestaurantName = restaurant.RestaurantName;
                restaurantModel.Telephone = restaurant.Telephone;
                restaurantModel.ID = restaurant.ID;
            }

            if (restaurantModel == null)
            {
                return HttpNotFound();
            }
            return View(restaurantModel);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRestaurant([Bind(Include = "ID,RestaurantName,Address,Telephone,Active")] RestaurantModel restaurantModel)
        {
            using (_uow = new UnitOfWork(new OrderingContext()))
            {

                var restaurant = new Restaurant();

                restaurant.ID = restaurantModel.ID;
                restaurant.RestaurantName = restaurantModel.RestaurantName;
                restaurant.Address = restaurantModel.Address;
                restaurant.Telephone = restaurantModel.Telephone;
                restaurant.Active = restaurantModel.Active;



                if (ModelState.IsValid)
                {
                    _uow.RestaurantRepository.Update(restaurant);
                    _uow.RestaurantRepository.Save();
                    return RedirectToAction("Restaurants");
                }
            }
            return View(restaurantModel);
        }




        // GET: Restaurant/id/Meal/Create
        [Route("Restaurants/id:int/CreateMeal")]
        public ActionResult CreateMeal(int? id)
        {
            return View();
        }

        // POST: Restaurant/Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Restaurants/int:id/CreateMeal")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeal([Bind(Include = "MealName,CategoryName,Price")] Meal mealModel, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);

                var meal = new Meal();
                {
                    meal.MealName = mealModel.MealName;
                    meal.CategoryName = mealModel.CategoryName;
                    meal.Price = mealModel.Price;
                    meal.Active = mealModel.Active;                    
                }

                if (ModelState.IsValid)
                {
                    _uow.MealRepository.AddMeal(meal,restaurant);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Meals", restaurant);
                }
            }
            return View(mealModel);
        }

    }
}





/*
 * 
 * 
 * 
 * 
 *
 *  [Route("Restaurants/int:id/CreateMeal")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeal([Bind(Include = "MealName,CategoryName,Price")] DetailsModel model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            model.restoran = new RestaurantModel();
            model.mealsForRestaurant = new List<Meal>();


            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);

                var meal = new Meal();

                foreach (var item in model.mealsForRestaurant)
                {
                    meal.ID = item.ID;
                    meal.MealName = item.MealName;
                    meal.CategoryName = item.CategoryName;
                    meal.Price = item.Price;
                    meal.Active = item.Active;
                    meal.RestaurantID = model.restoran.ID;
                };               

                if (ModelState.IsValid)
                {
                    _uow.MealRepository.AddMeal(meal);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Meals");
                }
            }
            return View(mealModel);
        } 
 * 
 * 
 * 
 * 
 *
 */

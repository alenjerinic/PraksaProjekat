using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using OrderingFood.Web.Models;
using OrderingFood.DataAccess.UnitOfWork;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Web.Routing;

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
        public ActionResult Meals(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DetailsModel> model = new List<DetailsModel>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)ID);
                var meal = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                foreach (var item in meal)
                {
                    DetailsModel detail = new DetailsModel()
                    {
                        ID = item.ID,
                        MealName = item.MealName,
                        Price = item.Price,
                        Category = item.CategoryName
                    };
                    model.Add(detail);
                }
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
                    _uow.RestaurantRepository.AddRestaurant(restaurant);
                    _uow.RestaurantRepository.Save();
                    return RedirectToAction("Restaurants");
                }
            }
            return View(restaurantModel);
        }


        public ActionResult DeleteRestaurant(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantModel restaurantToDelete = new RestaurantModel();
            var restaurant = new Restaurant();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                restaurant = _uow.RestaurantRepository.GetByID((int)ID);
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
        public ActionResult CreateMeal()
        {
            return View();
        }

        // POST: Restaurant/Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Restaurant/int:id/Meals")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeal([Bind(Include = "MealName,CategoryName,Price")] Meal mealModel,int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);        

                var meal= new Meal();
                {
                    meal.MealName = mealModel.MealName;
                    meal.CategoryName = mealModel.CategoryName;
                    meal.Price = mealModel.Price;
                    meal.Active = mealModel.Active;
                    meal.RestaurantID = restaurant.ID;
                }

                if (ModelState.IsValid)
                {
                    _uow.MealRepository.AddMeal(meal);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Meals");
                }
            }
            return View(mealModel);
        }

    }
}


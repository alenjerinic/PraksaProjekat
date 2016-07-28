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
using System;

namespace OrderingFood.Web.Controllers
{

    public class RestaurantController : Controller
    {
        private UnitOfWork _uow;

        #region Getting list of Restaurants
        // GET: Restaurant        
        public ActionResult Restaurants()
        {
            List<RestaurantModel> model = new List<RestaurantModel>();
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var rest = _uow.RestaurantRepository.Get(null, null, null);

                foreach (var item in rest)
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
        #endregion

        #region Getting list of Meals for Restaurant
        // GET: Restaurant/Meals/1
        [Route("Restaurant/Restaurants/id:int")]
        public ActionResult Meals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealDetailsModel model = new MealDetailsModel();
            model.restoran = new RestaurantModel();
            model.mealsForRestaurant = new List<MealModel>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);

                model.restoran.ID = restaurant.ID;
                model.restoran.RestaurantName = restaurant.RestaurantName;
                model.restoran.Address = restaurant.Address;
                model.restoran.Telephone = restaurant.Telephone;
                model.restoran.Active = restaurant.Active;

                var meal = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                foreach (var item in meal)
                {
                    MealModel mod = new MealModel()
                    {
                        Active=item.Active,
                        CategoryName=item.CategoryName,
                        ID=item.ID,
                        MealName=item.MealName,
                        Price=item.Price,
                        RestaurantID=item.RestaurantID
                    };
                    model.mealsForRestaurant.Add(mod);
                } 
            }
            return View(model);
        }
        #endregion

        #region Getting list of Orders for Meal
        public ActionResult Orders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetailsModel model = new OrderDetailsModel();
            model.restoran = new RestaurantModel();
            model.meal = new MealModel();
            model.ordersForMeal = new List<OrderModel>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID((int)id);

                model.meal.ID = meal.ID;
                model.meal.MealName = meal.MealName;
                model.meal.CategoryName = meal.CategoryName;
                model.meal.Price = meal.Price;
                model.meal.Active = meal.Active;

                var rest = _uow.RestaurantRepository.GetByID(meal.RestaurantID);
                model.restoran.ID = rest.ID;
                model.restoran.RestaurantName = rest.RestaurantName;
                model.restoran.Address = rest.Address;
                model.restoran.Telephone = rest.Telephone;
                model.restoran.Active = rest.Active;

                var orders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

                foreach (var item in orders)
                {
                    OrderModel mod = new OrderModel()
                    {
                        ID = item.ID,
                        UserName = item.UserName,
                        Amount = item.Amount,
                        OrderTime = item.OrderTime,
                        Delivery = item.Delivery,
                        MealID = item.MealID
                    };
                    model.ordersForMeal.Add(mod);
                }
            }
            return View(model);
        }
        #endregion



        #region Creating Restaurants        
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
        #endregion

        #region Deletation of Restaurant
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
        public ActionResult DeleteRestaurantConfirmed(int id)
        {
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID(id);
                _uow.RestaurantRepository.Delete(restaurant.ID);
                _uow.RestaurantRepository.Save();
            }
            return RedirectToAction("Restaurants");
        }
        #endregion

        #region Editing restaurant
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
        #endregion


        #region Adding Meal
        // GET: Restaurant/id/Meal/Create
        [Route("Restaurants/id:int/CreateMeal")]
        public ActionResult CreateMeal(int? id)
        {
            var meal = new MealModel();           

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var rest = new Restaurant();

                rest = _uow.RestaurantRepository.GetByID((int)id);

                meal.RestaurantName = rest.RestaurantName;

            };

            return View(meal);
        }

        
        [Route("Meals/id:int/CreateMeal")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeal([Bind(Include = "MealName,CategoryName,Price")] MealModel mealModel, int? id)
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
                    _uow.MealRepository.AddMeal(meal, restaurant);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Meals", routeValues: new { Id = restaurant.ID });
                }
            }
            return View(mealModel);
        }
        #endregion

        #region Editing Meal
        [Route("Meals/id:int/EditMeal")]        
        public ActionResult EditMeal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MealModel mealModel = new MealModel();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = new Meal();

                meal = _uow.MealRepository.GetByID((int)id);

                mealModel.ID = meal.ID;
                mealModel.Active = meal.Active;
                mealModel.MealName = meal.MealName;
                mealModel.CategoryName = meal.CategoryName;
                mealModel.Price = meal.Price;
                mealModel.RestaurantID = meal.RestaurantID;

                var rest = new Restaurant();

                rest = _uow.RestaurantRepository.GetByID(mealModel.RestaurantID);

                mealModel.RestaurantName = rest.RestaurantName;
            }

            if (mealModel == null)
            {
                return HttpNotFound();
            }
            return View(mealModel);
        }

        [Route("Meals/id:int/EditMeal")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeal([Bind(Include = "ID,MealName,CategoryName,Price,Active,RestaurantID")] MealModel mealModel)
        {
            var det = new MealDetailsModel();
            det.mealsForRestaurant = new List<MealModel>();
            det.restoran = new RestaurantModel();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = new Meal();

                meal.ID = mealModel.ID;
                meal.MealName = mealModel.MealName;
                meal.CategoryName = mealModel.CategoryName;
                meal.Price = mealModel.Price;
                meal.Active = mealModel.Active;
                meal.RestaurantID = mealModel.RestaurantID; ;

                var restaurant = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

                det.restoran.ID = restaurant.ID;
                det.restoran.RestaurantName = restaurant.RestaurantName;
                det.restoran.Address = restaurant.Address;
                det.restoran.Telephone = restaurant.Telephone;
                det.restoran.Active = restaurant.Active;             

                var listMeal = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                foreach (var item in listMeal)
                {
                    MealModel mod = new MealModel()
                    {
                        Active = item.Active,
                        CategoryName = item.CategoryName,
                        ID = item.ID,
                        MealName = item.MealName,
                        Price = item.Price,
                        RestaurantID = item.RestaurantID
                    };
                    det.mealsForRestaurant.Add(mod);
                }                

                if (ModelState.IsValid)
                {
                    _uow.MealRepository.UpdateMeal(meal, restaurant);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Meals", routeValues: new { Id = restaurant.ID });
                }
            }
            return View(det);
        }
        #endregion

        #region Deleting Meal
        public ActionResult DeleteMeal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MealModel mealToDelete = new MealModel();
            var meal = new Meal();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                meal = _uow.MealRepository.GetByID((int)id);

                mealToDelete.ID = meal.ID;
                mealToDelete.MealName = meal.MealName;
                mealToDelete.CategoryName = meal.CategoryName;
                mealToDelete.Price = meal.Price;
                mealToDelete.Active = meal.Active;
                mealToDelete.RestaurantID = meal.RestaurantID;

                var rest = new Restaurant();

                rest = _uow.RestaurantRepository.GetByID(mealToDelete.RestaurantID);

                mealToDelete.RestaurantName = rest.RestaurantName;
            }

            if (mealToDelete == null)
            {
                return HttpNotFound();
            }
            return View(mealToDelete);
        }

        [HttpPost, ActionName(nameof(DeleteMeal))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMealConfirmed(int id)
        {
            var obrok = new Meal();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID(id);
                obrok = meal;
                _uow.MealRepository.Delete(meal.ID);
                _uow.MealRepository.Save();
            }
            return RedirectToAction("Meals", routeValues: new { Id = obrok.RestaurantID });
        }

        #endregion


        #region Adding Order
        [Route("Meals/id:int/CreateOrder")]
        public ActionResult CreateOrder(int? id)
        {
            var order = new OrderModel();
            

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID((int)id);

                order.MealName = meal.MealName;

                order.OrderTime = DateTime.Now;
                order.UserName = User.Identity.Name;

            };

            return View(order);
        }

        [Route("Meals/id:int/CreateOrder")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include = "UserName,Amount,OrderTime,Delivery")] OrderModel orderModel, int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID((int)id);

                var order = new Order()
                {
                    Amount = orderModel.Amount,
                    OrderTime = orderModel.OrderTime,
                    Delivery = orderModel.Delivery,
                    MealID = orderModel.MealID,
                    UserName=orderModel.UserName
                };

                if (ModelState.IsValid)
                {
                    _uow.OrderRepository.AddOrder(order, meal);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Orders", routeValues: new { Id = meal.ID });
                }                                
                return View(orderModel);
                }
            }
        #endregion

        #region Edit Order
        [Route("Orders/id:int/EditOrder")]
        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderModel orderModel = new OrderModel();
            
            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var order = new Order();              

                order = _uow.OrderRepository.GetByID((int)id);

                orderModel.ID = order.ID;
                orderModel.UserName = User.Identity.Name;
                orderModel.Amount = order.ID;
                orderModel.Delivery = order.Delivery;
                orderModel.OrderTime = order.OrderTime = DateTime.Now;
                orderModel.MealID = order.MealID;

                var meal = _uow.MealRepository.GetByID(orderModel.MealID);

                orderModel.MealName = meal.MealName;

                }                    

            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        [Route("Orders/id:int/EditOrder")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "ID,UserName,Amount,Delivery,OrderTime,MealID")] OrderModel orderModel)
        {
            var det = new OrderDetailsModel();
            det.meal = new MealModel();
            det.ordersForMeal = new List<OrderModel>();
            det.restoran = new RestaurantModel();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var order = new Order();

                order.ID = orderModel.ID;
                order.UserName = orderModel.UserName;
                order.Amount = orderModel.Amount;
                order.Delivery = orderModel.Delivery;
                order.OrderTime = orderModel.OrderTime = DateTime.Now;
                order.MealID = orderModel.MealID;

                var meal = _uow.MealRepository.GetByID(order.MealID);

                det.meal.ID = meal.ID;
                det.meal.MealName = meal.MealName;
                det.meal.CategoryName = meal.CategoryName;
                det.meal.Price = meal.Price;
                det.meal.Active = meal.Active;
                det.meal.RestaurantID = meal.RestaurantID;

                var restaurant = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

                det.restoran.ID = restaurant.ID;
                det.restoran.RestaurantName = restaurant.RestaurantName;
                det.restoran.Address = restaurant.Address;
                det.restoran.Telephone = restaurant.Telephone;
                det.restoran.Active = restaurant.Active;

                var listorders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

                foreach (var item in listorders)
                {
                    OrderModel mod = new OrderModel()
                    {
                        ID = item.ID,
                        UserName = item.UserName,
                        Amount = item.Amount,
                        OrderTime = item.OrderTime,
                        Delivery = item.Delivery,
                        MealID = item.MealID
                    };
                    det.ordersForMeal.Add(mod);

                    if (ModelState.IsValid)
                    {
                        _uow.OrderRepository.UpdateOrder(order, meal);
                        _uow.MealRepository.Save();
                        return RedirectToAction("Orders", routeValues: new { Id = meal.ID });
                    }
                }
                return View(det);
            }
        }
        #endregion

        #region Delete Order
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderModel orderToDelete = new OrderModel();
            
            var order = new Order();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                order = _uow.OrderRepository.GetByID((int)id);

                orderToDelete.ID = order.ID;
                orderToDelete.UserName = order.UserName;
                orderToDelete.Amount = order.Amount;
                orderToDelete.Delivery = order.Delivery;
                orderToDelete.MealID = order.MealID;

                var meal = _uow.MealRepository.GetByID(orderToDelete.MealID);
                orderToDelete.MealName = meal.MealName;
            }

            if (orderToDelete == null)
            {
                return HttpNotFound();
            }
            return View(orderToDelete);
        }

        [HttpPost, ActionName(nameof(DeleteOrder))]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderConfirmed(int id)
        {
            var narudzba = new Order();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var order = _uow.OrderRepository.GetByID(id);
                narudzba = order;
                _uow.OrderRepository.Delete(order.ID);
                _uow.OrderRepository.Save();
            }
            return RedirectToAction("Orders", routeValues: new { Id = narudzba.MealID});
        }

        #endregion
    }
}







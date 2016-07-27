﻿using System.Collections.Generic;
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
                    Active = m.Active,
                    ID = m.ID,
                    MealName = m.MealName,
                    CategoryName = m.CategoryName,
                    Price = m.Price
                });

                model.mealsForRestaurant = meal;

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
            model.ordersForMeal = new List<Order>();

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

                orders.Select(o => new OrderModel()
                {
                    ID = o.ID,
                    Amount = o.Amount,
                    OrderTime = o.OrderTime,
                    Delivery = o.Delivery
                });

                model.ordersForMeal = orders;
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
            return View();
        }

        // POST: Restaurant/Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            det.mealsForRestaurant = new List<Meal>();
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

                listMeal.Select(m => new Meal()
                {
                    Active = m.Active,
                    ID = m.ID,
                    MealName = m.MealName,
                    CategoryName = m.CategoryName,
                    Price = m.Price
                });

                det.mealsForRestaurant = listMeal;

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
                _uow.RestaurantRepository.Save();
            }
            return RedirectToAction("Meals", routeValues: new { Id = obrok.RestaurantID });
        }

        #endregion


        #region Adding Orders
        [Route("Meals/id:int/CreateOrder")]
        public ActionResult CreateOrder(int? id)
        {
            var order = new OrderModel();

            order.OrderTime = DateTime.Now;
            order.UserName = User.Identity.Name;

            return View(order);
        }

        [Route("Meals/id:int/CreateOrder")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include = "Amount,OrderTime,Delivery")] OrderModel orderModel, int? id)
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
                    MealID = orderModel.MealID

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
                orderModel.UserName = order.UserName;
                orderModel.Amount = order.ID;
                orderModel.Delivery = order.Delivery;
                orderModel.OrderTime = order.OrderTime = DateTime.Now;
                orderModel.MealID = order.MealID;
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
        public ActionResult EditOrder([Bind(Include = "ID,UserName,Amaunt,Delivery,OrderTime,MealID")] OrderModel orderModel)
        {
            var det = new OrderDetailsModel();
            det.meal = new MealModel();
            det.ordersForMeal = new List<Order>();
            det.restoran = new RestaurantModel();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var order = new Order();              

                orderModel.ID = order.ID;
                orderModel.UserName = order.UserName;
                orderModel.Amount = order.Amount;
                orderModel.Delivery = order.Delivery;
                orderModel.OrderTime = order.OrderTime = DateTime.Now;
                orderModel.MealID = order.MealID;

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

                listorders.Select(o => new Order()
                {
                    Amount = o.Amount,
                    ID = o.ID,
                    UserName = o.UserName,
                    Delivery = o.Delivery,
                    OrderTime = o.OrderTime,

                });

                det.ordersForMeal = listorders;

                if (ModelState.IsValid)
                {
                    _uow.OrderRepository.UpdateOrder(order, meal);
                    _uow.MealRepository.Save();
                    return RedirectToAction("Orders", routeValues: new { Id = meal.ID });
                }
            }
            return View(det);
        }
        #endregion
    }
}







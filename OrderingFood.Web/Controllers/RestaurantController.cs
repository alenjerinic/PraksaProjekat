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




        #region UserRegion
        
        #region Getting list of Restaurants for Users              
        public ActionResult UserRestaurants()
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
                        Active = item.Active,                        
                    };
                    model.Add(restaurant);
                }
            }
            return View(model);
        }
        #endregion

        #region Getting list of Orders and Meals for Restaurant for User        
        [Route("Restaurant/id:int/UserRestaurants")]
        public ActionResult UserMealsAndOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AllOrdersForMeals all = new AllOrdersForMeals();

            MealDetailsModel modelm = new MealDetailsModel();
            modelm.restoran = new RestaurantModel();
            modelm.mealsForRestaurant = new List<MealModel>();

            OrderDetailsModel modelo = new OrderDetailsModel();
            modelo.restoran = new RestaurantModel();
            modelo.meal = new MealModel();
            modelo.ordersForMeal = new List<OrderModel>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);

                modelm.restoran.ID = restaurant.ID;
                modelm.restoran.RestaurantName = restaurant.RestaurantName;
                modelm.restoran.Address = restaurant.Address;
                modelm.restoran.Telephone = restaurant.Telephone;
                modelm.restoran.Active = restaurant.Active;

                modelo.restoran = modelm.restoran;

                var mealm = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                foreach (var item in mealm)
                {
                    MealModel modm = new MealModel()
                    {
                        Active = item.Active,
                        CategoryName = item.CategoryName,
                        ID = item.ID,
                        MealName = item.MealName,
                        Price = item.Price,
                        Restaurant=modelm.restoran
                                              
                    };
                    modelm.mealsForRestaurant.Add(modm);                   

                    var meal = _uow.MealRepository.GetByID(modm.ID);

                    modelo.meal.ID = meal.ID;
                    modelo.meal.MealName = meal.MealName;
                    modelo.meal.CategoryName = meal.CategoryName;
                    modelo.meal.Price = meal.Price;
                    modelo.meal.Active = meal.Active;
                    modelo.meal.Restaurant = modelo.restoran;                    

                    var orders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

                    foreach (var itemo in orders)
                    {
                        OrderModel modo = new OrderModel()
                        {
                            ID = itemo.ID,
                            UserName = itemo.UserName,                          
                            Amount = itemo.Amount,
                            OrderTime = itemo.OrderTime,
                            Delivery = itemo.Delivery,
                            Meal=modelo.meal                           
                        };
                        modelo.ordersForMeal.Add(modo);
                    }
                }
            }

            all.MealDetail = modelm;
            all.OrderDetail = modelo;

            return View(all);
        }
        #endregion

        #region Getting list of Meals for Restaurant "UserMealsAlone" for User
        public ActionResult UserMealsAlone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MealDetailsModel modelm = new MealDetailsModel();
            modelm.restoran = new RestaurantModel();
            modelm.mealsForRestaurant = new List<MealModel>();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var restaurant = _uow.RestaurantRepository.GetByID((int)id);

                modelm.restoran.ID = restaurant.ID;
                modelm.restoran.RestaurantName = restaurant.RestaurantName;
                modelm.restoran.Address = restaurant.Address;
                modelm.restoran.Telephone = restaurant.Telephone;
                modelm.restoran.Active = restaurant.Active;

                var mealm = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

                foreach (var item in mealm)
                {
                    MealModel modm = new MealModel()
                    {
                        Active = item.Active,
                        CategoryName = item.CategoryName,
                        ID = item.ID,
                        MealName = item.MealName,
                        Price = item.Price,
                        Restaurant = modelm.restoran                       
                    };
                    modelm.mealsForRestaurant.Add(modm);
                }
            }
            return View(modelm);
        }
        #endregion

        #region Getting list of Orders for Meal "UserOrdersAlone" for User
        public ActionResult UserOrdersAlone(int? id)
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
                        Meal=model.meal                       
                    };
                    model.ordersForMeal.Add(mod);
                }
            }
            return View(model);
        }
        #endregion

        #region Adding Order for User
        [Route("Restaurant/UserCreateOrder/id:int")]
        [HttpGet]
        public ActionResult UserCreateOrder(int? id)
        {
            var order = new OrderModel();
           


            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID((int)id);

                order.Meal.MealName= meal.MealName;
                order.Meal.Restaurant.ID = meal.RestaurantID;
                order.OrderTime = DateTime.Now;
                order.UserName = User.Identity.Name;

            };

            return View(order);
        }

        [Route("Restaurant/UserCreateOrder/id:int")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreateOrder([Bind(Include = "UserName,Amount,OrderTime,Delivery,Meal")] OrderModel orderModel, int? id)
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
                    MealID = orderModel.Meal.ID,
                    UserName = orderModel.UserName
                };

                if (ModelState.IsValid)
                {
                    _uow.OrderRepository.AddOrder(order, meal);
                    _uow.MealRepository.Save();
                    return RedirectToAction("UserMealsAndOrders", routeValues: new { Id = meal.RestaurantID });
                }
                return View(orderModel);
            }
        }
        #endregion

        #region Edit Order for User
        [Route("Restaurant/UserEditOrder/id:int")]
        public ActionResult UserEditOrder(int? id)
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

                var meal = _uow.MealRepository.GetByID(order.MealID);               

                orderModel.ID = order.ID;
                orderModel.UserName = User.Identity.Name;
                orderModel.Amount = order.ID;
                orderModel.Delivery = order.Delivery;
                orderModel.OrderTime = order.OrderTime = DateTime.Now;
                orderModel.Meal.ID = order.Meal.ID;
                orderModel.Meal.MealName = order.Meal.MealName;               
                orderModel.Meal.Restaurant.ID = order.Meal.RestaurantID;
            }

            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        [Route("Restaurant/UserEditOrder/id:int")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEditOrder([Bind(Include = "ID,UserName,Amount,Delivery,OrderTime,Meal")] OrderModel orderModel)
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
                order.MealID = orderModel.Meal.ID;

                var meal = _uow.MealRepository.GetByID(order.MealID);

                det.meal.ID = meal.ID;
                det.meal.MealName = meal.MealName;
                det.meal.CategoryName = meal.CategoryName;
                det.meal.Price = meal.Price;
                det.meal.Active = meal.Active;
                det.meal.Restaurant.ID = meal.RestaurantID;

                var restaurant = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

                det.restoran.ID = restaurant.ID;
                det.restoran.RestaurantName = restaurant.RestaurantName;
                det.restoran.Address = restaurant.Address;
                det.restoran.Telephone = restaurant.Telephone;
                det.restoran.Active = restaurant.Active;

                var listorders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

                foreach (var item in listorders)
                {
                    OrderModel mod = new OrderModel();

                    mod.ID = item.ID;
                    mod.UserName = item.UserName;
                    mod.Amount = item.Amount;
                    mod.OrderTime = item.OrderTime;
                    mod.Delivery = item.Delivery;
                    mod.Meal.ID = item.MealID;
                    
                    det.ordersForMeal.Add(mod);

                    if (ModelState.IsValid)
                    {
                        _uow.OrderRepository.UpdateOrder(order, meal);
                        _uow.MealRepository.Save();
                        return RedirectToAction("UserMealsAndOrders", routeValues: new { Id = meal.RestaurantID });
                    }
                }
                return View(det);
            }
        }
        #endregion

        #region Delete Order for User
        public ActionResult UserDeleteOrder(int? id)
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
                orderToDelete.Meal.ID = order.MealID;

                var meal = _uow.MealRepository.GetByID(order.MealID);
                orderToDelete.Meal.MealName = meal.MealName;
                orderToDelete.Meal.Restaurant.ID = meal.RestaurantID;
            }

            if (orderToDelete == null)
            {
                return HttpNotFound();
            }
            return View(orderToDelete);
        }

        [HttpPost, ActionName(nameof(UserDeleteOrder))]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteOrderConfirmed(int id)
        {
            var narudzba = new Order();
            var obrok = new Meal();

            using (_uow = new UnitOfWork(new OrderingContext()))
            {
                var meal = _uow.MealRepository.GetByID(narudzba.MealID);
                obrok = meal;

                var order = _uow.OrderRepository.GetByID(id);
                narudzba = order;
                _uow.OrderRepository.Delete(order.ID);
                _uow.OrderRepository.Save();
            }
            return RedirectToAction("UserMealsAndOrders", routeValues: new { Id = obrok.RestaurantID });
        }

        #endregion

        #endregion
        
                

        //#region AdminRegion

        //#region Getting list of Restaurants For Admins
        //// GET: Restaurant        
        //public ActionResult AdminRestaurants()
        //{
        //    List<RestaurantModel> model = new List<RestaurantModel>();
        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var rest = _uow.RestaurantRepository.Get(null, null, null);

        //        foreach (var item in rest)
        //        {
        //            RestaurantModel restaurant = new RestaurantModel()
        //            {
        //                ID = item.ID,
        //                RestaurantName = item.RestaurantName,
        //                Address = item.Address,
        //                Telephone = item.Telephone,
        //                Active = item.Active,
        //            };
        //            model.Add(restaurant);
        //        }
        //    }
        //    return View(model);
        //}
        //#endregion

        //#region Getting list of Meals for Restaurant "AdminMealsAlone" for Admin
        //public ActionResult AdminMealsAlone(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    MealDetailsModel modelm = new MealDetailsModel();
        //    modelm.restoran = new RestaurantModel();
        //    modelm.mealsForRestaurant = new List<MealModel>();



        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var restaurant = _uow.RestaurantRepository.GetByID((int)id);

        //        modelm.restoran.ID = restaurant.ID;
        //        modelm.restoran.RestaurantName = restaurant.RestaurantName;
        //        modelm.restoran.Address = restaurant.Address;
        //        modelm.restoran.Telephone = restaurant.Telephone;
        //        modelm.restoran.Active = restaurant.Active;

        //        var mealm = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

        //        foreach (var item in mealm)
        //        {
        //            MealModel modm = new MealModel()
        //            {
        //                Active = item.Active,
        //                CategoryName = item.CategoryName,
        //                ID = item.ID,
        //                MealName = item.MealName,
        //                Price = item.Price,
        //                Restaurant = modelm.restoran
        //            };
        //            modelm.mealsForRestaurant.Add(modm);
        //        }
        //    }
        //    return View(modelm);
        //}
        //#endregion

        //#region Getting list of Orders for Meal "AdminOrdersAlone" for Admin
        //public ActionResult AdminOrdersAlone(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderDetailsModel model = new OrderDetailsModel();
        //    model.restoran = new RestaurantModel();
        //    model.meal = new MealModel();
        //    model.ordersForMeal = new List<OrderModel>();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = _uow.MealRepository.GetByID((int)id);

        //        model.meal.ID = meal.ID;
        //        model.meal.MealName = meal.MealName;
        //        model.meal.CategoryName = meal.CategoryName;
        //        model.meal.Price = meal.Price;
        //        model.meal.Active = meal.Active;

        //        var rest = _uow.RestaurantRepository.GetByID(meal.RestaurantID);
        //        model.restoran.ID = rest.ID;
        //        model.restoran.RestaurantName = rest.RestaurantName;
        //        model.restoran.Address = rest.Address;
        //        model.restoran.Telephone = rest.Telephone;
        //        model.restoran.Active = rest.Active;

        //        var orders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

        //        foreach (var item in orders)
        //        {
        //            OrderModel mod = new OrderModel()
        //            {
        //                ID = item.ID,
        //                UserName = item.UserName,
        //                Amount = item.Amount,
        //                OrderTime = item.OrderTime,
        //                Delivery = item.Delivery,
        //                MealID = item.MealID
        //            };
        //            model.ordersForMeal.Add(mod);
        //        }
        //    }
        //    return View(model);
        //}
        //#endregion


        //#region Creating Restaurants for Admin
        //public ActionResult AdminCreateRestaurant()
        //{
        //    return View();
        //}
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminCreateRestaurant([Bind(Include = "RestaurantName,Address,Telephone,Active")] RestaurantModel restaurantModel)
        //{
        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var restaurant = new Restaurant();
        //        {
        //            restaurant.RestaurantName = restaurantModel.RestaurantName;
        //            restaurant.Address = restaurantModel.Address;
        //            restaurant.Telephone = restaurantModel.Telephone;
        //            restaurant.Active = restaurantModel.Active;
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            _uow.RestaurantRepository.Insert(restaurant);
        //            _uow.RestaurantRepository.Save();
        //            return RedirectToAction("AdminRestaurants");
        //        }
        //    }
        //    return View(restaurantModel);
        //}
        //#endregion

        //#region Deletation of Restaurant for Admin
        //public ActionResult AdminDeleteRestaurant(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RestaurantModel restaurantToDelete = new RestaurantModel();
        //    var restaurant = new Restaurant();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        restaurant = _uow.RestaurantRepository.GetByID((int)id);
        //        restaurantToDelete.Address = restaurant.Address;
        //        restaurantToDelete.RestaurantName = restaurant.RestaurantName;
        //        restaurantToDelete.Telephone = restaurant.Telephone;
        //        restaurantToDelete.ID = restaurant.ID;
        //        restaurantToDelete.Active = restaurant.Active;
        //    }

        //    if (restaurantToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(restaurantToDelete);
        //}

        
        //[HttpPost, ActionName(nameof(AdminDeleteRestaurant))]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteRestaurantConfirmed(int id)
        //{
        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var restaurant = _uow.RestaurantRepository.GetByID(id);
        //        _uow.RestaurantRepository.Delete(restaurant.ID);
        //        _uow.RestaurantRepository.Save();
        //    }
        //    return RedirectToAction("AdminRestaurants");
        //}
        //#endregion

        //#region Editing restaurant for Admin        
        //public ActionResult AdminEditRestaurant(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    RestaurantModel restaurantModel = new RestaurantModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {

        //        var restaurant = new Restaurant();

        //        restaurant = _uow.RestaurantRepository.GetByID((int)id);

        //        restaurantModel.Active = restaurant.Active;
        //        restaurantModel.Address = restaurant.Address;
        //        restaurantModel.RestaurantName = restaurant.RestaurantName;
        //        restaurantModel.Telephone = restaurant.Telephone;
        //        restaurantModel.ID = restaurant.ID;
        //    }

        //    if (restaurantModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(restaurantModel);
        //}
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminEditRestaurant([Bind(Include = "ID,RestaurantName,Address,Telephone,Active")] RestaurantModel restaurantModel)
        //{
        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {

        //        var restaurant = new Restaurant();

        //        restaurant.ID = restaurantModel.ID;
        //        restaurant.RestaurantName = restaurantModel.RestaurantName;
        //        restaurant.Address = restaurantModel.Address;
        //        restaurant.Telephone = restaurantModel.Telephone;
        //        restaurant.Active = restaurantModel.Active;



        //        if (ModelState.IsValid)
        //        {
        //            _uow.RestaurantRepository.Update(restaurant);
        //            _uow.RestaurantRepository.Save();
        //            return RedirectToAction("AdminRestaurants");
        //        }
        //    }
        //    return View(restaurantModel);
        //}
        //#endregion


        //#region Adding Meal for Admin
        //// GET: Restaurant/id/Meal/Create
        //[Route("AdminRestaurants/id:int/AdminCreateMeal")]
        //public ActionResult AdminCreateMeal(int? id)
        //{
        //    var meal = new MealModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var rest = new Restaurant();

        //        rest = _uow.RestaurantRepository.GetByID((int)id);

        //        meal.RestaurantName = rest.RestaurantName;

        //    };

        //    return View(meal);
        //}


        //[Route("Restaurants/id:int/AdminCreateMeal")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminCreateMeal([Bind(Include = "MealName,CategoryName,Price")] MealModel mealModel, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var restaurant = _uow.RestaurantRepository.GetByID((int)id);

        //        var meal = new Meal();
        //        {
        //            meal.MealName = mealModel.MealName;
        //            meal.CategoryName = mealModel.CategoryName;
        //            meal.Price = mealModel.Price;
        //            meal.Active = mealModel.Active;
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            _uow.MealRepository.AddMeal(meal, restaurant);
        //            _uow.MealRepository.Save();
        //            return RedirectToAction("AdminsMealsAndOrders", routeValues: new { Id = restaurant.ID });
        //        }
        //    }
        //    return View(mealModel);
        //}
        //#endregion

        //#region Editing Meal for Admin
        //[Route("AdminMealsAlone/id:int/AdminEditMeal")]
        //public ActionResult AdminEditMeal(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    MealModel mealModel = new MealModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = new Meal();
        //        var restoran = new RestaurantModel();

        //        meal = _uow.MealRepository.GetByID((int)id);

        //        var rest = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

        //        restoran.ID = rest.ID;
        //        restoran.RestaurantName = rest.RestaurantName;
        //        restoran.Address = rest.Address;
        //        restoran.Telephone = rest.Telephone;
        //        restoran.Active = rest.Active;

        //        mealModel.ID = meal.ID;
        //        mealModel.Active = meal.Active;
        //        mealModel.MealName = meal.MealName;
        //        mealModel.CategoryName = meal.CategoryName;
        //        mealModel.Price = meal.Price;
        //        mealModel.Restaurant=restoran;

        //    }

        //    if (mealModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mealModel);
        //}

        //[Route("AdminMealsAlone/id:int/AdminEditMeal")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminEditMeal([Bind(Include = "ID,MealName,CategoryName,Price,Active,RestaurantID")] MealModel mealModel)
        //{
        //    var det = new MealDetailsModel();
        //    det.mealsForRestaurant = new List<MealModel>();
        //    det.restoran = new RestaurantModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = new Meal();

        //        meal.ID = mealModel.ID;
        //        meal.MealName = mealModel.MealName;
        //        meal.CategoryName = mealModel.CategoryName;
        //        meal.Price = mealModel.Price;
        //        meal.Active = mealModel.Active;
        //        meal.Restaurant=

        //        var restaurant = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

        //        det.restoran.ID = restaurant.ID;
        //        det.restoran.RestaurantName = restaurant.RestaurantName;
        //        det.restoran.Address = restaurant.Address;
        //        det.restoran.Telephone = restaurant.Telephone;
        //        det.restoran.Active = restaurant.Active;

        //        var listMeal = _uow.MealRepository.GetMealByRestaurant(restaurant.ID);

        //        foreach (var item in listMeal)
        //        {
        //            MealModel mod = new MealModel()
        //            {
        //                Active = item.Active,
        //                CategoryName = item.CategoryName,
        //                ID = item.ID,
        //                MealName = item.MealName,
        //                Price = item.Price,
        //                RestaurantID = item.RestaurantID
        //            };
        //            det.mealsForRestaurant.Add(mod);
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            _uow.MealRepository.UpdateMeal(meal, restaurant);
        //            _uow.MealRepository.Save();
        //            return RedirectToAction("AdminMealsAndOrders", routeValues: new { Id = restaurant.ID });
        //        }
        //    }
        //    return View(det);
        //}
        //#endregion

        //#region Deleting Meal for Admin
        //public ActionResult AdminDeleteMeal(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    MealModel mealToDelete = new MealModel();
        //    var meal = new Meal();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        meal = _uow.MealRepository.GetByID((int)id);

        //        mealToDelete.ID = meal.ID;
        //        mealToDelete.MealName = meal.MealName;
        //        mealToDelete.CategoryName = meal.CategoryName;
        //        mealToDelete.Price = meal.Price;
        //        mealToDelete.Active = meal.Active;
        //        mealToDelete.RestaurantID = meal.RestaurantID;

        //        var rest = new Restaurant();

        //        rest = _uow.RestaurantRepository.GetByID(mealToDelete.RestaurantID);

        //        mealToDelete.RestaurantName = rest.RestaurantName;
        //    }

        //    if (mealToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mealToDelete);
        //}

        //[HttpPost, ActionName(nameof(AdminDeleteMeal))]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminDeleteMealConfirmed(int id)
        //{
        //    var obrok = new Meal();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = _uow.MealRepository.GetByID(id);
        //        obrok = meal;
        //        _uow.MealRepository.Delete(meal.ID);
        //        _uow.MealRepository.Save();
        //    }
        //    return RedirectToAction("AdminMealsAndOrders", routeValues: new { Id = obrok.RestaurantID });
        //}

        //#endregion



        //#region Adding Order for Admin
        //[Route("AdminMealsAlone/id:int/AdminCreateOrder")]
        //public ActionResult AdminCreateOrder(int? id)
        //{
        //    var order = new OrderModel();


        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = _uow.MealRepository.GetByID((int)id);

        //        order.MealName = meal.MealName;

        //        order.OrderTime = DateTime.Now;
        //        order.UserName = User.Identity.Name;

        //    };

        //    return View(order);
        //}

        //[Route("AdminMealsAlone/id:int/AdminCreateOrder")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminCreateOrder([Bind(Include = "UserName,Amount,OrderTime,Delivery")] OrderModel orderModel, int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var meal = _uow.MealRepository.GetByID((int)id);

        //        var order = new Order()
        //        {
        //            Amount = orderModel.Amount,
        //            OrderTime = orderModel.OrderTime,
        //            Delivery = orderModel.Delivery,
        //            MealID = orderModel.MealID,
        //            UserName = orderModel.UserName
        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _uow.OrderRepository.AddOrder(order, meal);
        //            _uow.MealRepository.Save();
        //            return RedirectToAction("AdminMealsAndOrders", routeValues: new { Id = meal.ID });
        //        }
        //        return View(orderModel);
        //    }
        //}
        //#endregion

        //#region Edit Order for Admin
        //[Route("AdminOrdersAlone/id:int/AdminEditOrder")]
        //public ActionResult AdminEditOrder(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    OrderModel orderModel = new OrderModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var order = new Order();

        //        order = _uow.OrderRepository.GetByID((int)id);

        //        orderModel.ID = order.ID;
        //        orderModel.UserName = User.Identity.Name;
        //        orderModel.Amount = order.ID;
        //        orderModel.Delivery = order.Delivery;
        //        orderModel.OrderTime = order.OrderTime = DateTime.Now;
        //        orderModel.MealID = order.MealID;

        //        var meal = _uow.MealRepository.GetByID(orderModel.MealID);

        //        orderModel.MealName = meal.MealName;

        //    }

        //    if (orderModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderModel);
        //}

        //[Route("AdminOrdersAlone/id:int/AdminEditOrder")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminEditOrder([Bind(Include = "ID,UserName,Amount,Delivery,OrderTime,MealID")] OrderModel orderModel)
        //{
        //    var det = new OrderDetailsModel();
        //    det.meal = new MealModel();
        //    det.ordersForMeal = new List<OrderModel>();
        //    det.restoran = new RestaurantModel();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var order = new Order();

        //        order.ID = orderModel.ID;
        //        order.UserName = orderModel.UserName;
        //        order.Amount = orderModel.Amount;
        //        order.Delivery = orderModel.Delivery;
        //        order.OrderTime = orderModel.OrderTime = DateTime.Now;
        //        order.MealID = orderModel.MealID;

        //        var meal = _uow.MealRepository.GetByID(order.MealID);

        //        det.meal.ID = meal.ID;
        //        det.meal.MealName = meal.MealName;
        //        det.meal.CategoryName = meal.CategoryName;
        //        det.meal.Price = meal.Price;
        //        det.meal.Active = meal.Active;
        //        det.meal.RestaurantID = meal.RestaurantID;

        //        var restaurant = _uow.RestaurantRepository.GetByID(meal.RestaurantID);

        //        det.restoran.ID = restaurant.ID;
        //        det.restoran.RestaurantName = restaurant.RestaurantName;
        //        det.restoran.Address = restaurant.Address;
        //        det.restoran.Telephone = restaurant.Telephone;
        //        det.restoran.Active = restaurant.Active;

        //        var listorders = _uow.OrderRepository.GetOrdersByMeal(meal.ID);

        //        foreach (var item in listorders)
        //        {
        //            OrderModel mod = new OrderModel()
        //            {
        //                ID = item.ID,
        //                UserName = item.UserName,
        //                Amount = item.Amount,
        //                OrderTime = item.OrderTime,
        //                Delivery = item.Delivery,
        //                MealID = item.MealID
        //            };
        //            det.ordersForMeal.Add(mod);

        //            if (ModelState.IsValid)
        //            {
        //                _uow.OrderRepository.UpdateOrder(order, meal);
        //                _uow.MealRepository.Save();
        //                return RedirectToAction("AdminMealsAndOrders", routeValues: new { Id = restaurant.ID});
        //            }
        //        }
        //        return View(det);
        //    }
        //}
        //#endregion

        //#region Delete Order for Admin
        //public ActionResult AdminDeleteOrder(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    OrderModel orderToDelete = new OrderModel();

        //    var order = new Order();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        order = _uow.OrderRepository.GetByID((int)id);

        //        orderToDelete.ID = order.ID;
        //        orderToDelete.UserName = order.UserName;
        //        orderToDelete.Amount = order.Amount;
        //        orderToDelete.Delivery = order.Delivery;
        //        orderToDelete.MealID = order.MealID;

        //        var meal = _uow.MealRepository.GetByID(orderToDelete.MealID);
        //        orderToDelete.MealName = meal.MealName;
        //    }

        //    if (orderToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderToDelete);
        //}

        //[HttpPost, ActionName(nameof(AdminDeleteOrder))]
        //[ValidateAntiForgeryToken]
        //public ActionResult AdminDeleteOrderConfirmed(int id)
        //{
        //    var narudzba = new Order();

        //    using (_uow = new UnitOfWork(new OrderingContext()))
        //    {
        //        var order = _uow.OrderRepository.GetByID(id);
        //        narudzba = order;
        //        _uow.OrderRepository.Delete(order.ID);
        //        _uow.OrderRepository.Save();
        //    }
        //    return RedirectToAction("AdminMealsAndOrders", routeValues: new { Id = narudzba.Meal.RestaurantID });
        //}

        //#endregion

        //#endregion




    }
}







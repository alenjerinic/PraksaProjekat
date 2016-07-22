using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingFood.DataAccess.UnitOfWork;
using System.Collections.Generic;
using OrderingFood.Data.Models;

namespace OrderingFood.Test
{
    [TestClass]
    public  class OrderRepositoryTest
    {

        #region getting-orders-from-meal
        [TestMethod]
        public void GetOrderByMeal()
        {
            var orders = new List<Order>();
            orders.Add(new Order()
            {
                ID=1,
                Amount=1,
                OrderTime=11,
                Delivery=true,
                MealID=1
            });

            orders.Add(new Order()
            {
                ID = 2,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                MealID = 2
            });

            orders.Add(new Order()
            {
                ID = 3,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                MealID = 2
            });

            var melas = new List<Meal>();
            melas.Add(new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                RestaurantID = 1,
                Active = true,                
                Orders=orders              

            });

            melas.Add(new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 1,
                Active = true,
                Orders = orders
            });

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas
            });

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).WithBuitinOrders(orders).Create();           

            var repository = new UnitOfWork(mockContext);            

            var ord = repository.OrderRepository.GetOrderByMeal(1);

            Assert.AreEqual(1, ord.Count);

        }
        #endregion

        #region adding-new-orders-to-meals
        [TestMethod]
        public void AddOrdersToMeals()
        {
            var orders = new List<Order>();
            orders.Add(new Order()
            {
                ID = 1,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                MealID = 1
            });

            orders.Add(new Order()
            {
                ID = 2,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                MealID = 2
            });

            orders.Add(new Order()
            {
                ID = 3,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                MealID = 2
            });

            var melas = new List<Meal>();
            melas.Add(new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                RestaurantID = 1,
                Active = true

            });

            melas.Add(new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 2,
                Active = true
            });            

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas
            });

            restaurants.Add(new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = null,
                Meals = melas
            });

            var ord = new Order();
            {
                ord.ID = 8;
                ord.MealID = 2;
                ord.Amount = 1;
                ord.OrderTime = 10;
                ord.Delivery = true;

            }

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).WithBuitinOrders(orders).Create();

            var repository = new UnitOfWork(mockContext);

            repository.OrderRepository.AddOrder(ord);

            var ordercic = repository.OrderRepository.GetOrderByMeal(2);

            Assert.AreEqual(4, orders.Count);
            Assert.AreEqual(3, ordercic.Count);


        }
        #endregion



    }
}

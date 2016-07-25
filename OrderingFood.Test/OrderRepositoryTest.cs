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

            var restaurants = new List<Restaurant>();
            var melas = new List<Meal>();
            var orders = new List<Order>();

            var rest = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas
            };
            restaurants.Add(rest);

            var mel1 = new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                Restaurant = null,
                Active = true,
                Orders = orders
            };
            melas.Add(mel1);

            var mel2 = new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant = null,
                Active = true,
                Orders = orders
            };
            melas.Add(mel2);

            var ord1 = new Order()
            {
                ID = 1,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel1,
                MealID=mel1.ID
            };
            orders.Add(ord1);

            var ord2 = new Order()
            {
                ID = 2,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel1,
                MealID = mel1.ID
            };
            orders.Add(ord2);

            var ord3 = new Order()
            {
                ID = 3,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel2,
                MealID = mel2.ID
            };
            orders.Add(ord3);


            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).WithBuitinOrders(orders).Create();           

            var repository = new UnitOfWork(mockContext);           

            var ord = repository.OrderRepository.GetOrderByMeal(1);

            Assert.AreEqual(2, ord.Count);

        }
        #endregion

        #region adding-new-orders-to-meals
        [TestMethod]
        public void AddOrdersToMeals()
        {
            int id = 2;
            var restaurants = new List<Restaurant>();
            var melas = new List<Meal>();
            var orders = new List<Order>();
            var orders1 = new List<Order>();
            var orders2 = new List<Order>();

            var rest = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas
            };
            restaurants.Add(rest);

            var mel1 = new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                Restaurant = rest,
                RestaurantID=rest.ID,
                Active = true,
                Orders = orders1
            };
            melas.Add(mel1);

            var mel2 = new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant = rest,
                RestaurantID = rest.ID,
                Active = true,
                Orders = orders2
            };
            melas.Add(mel2);

            var ord1 = new Order()
            {
                ID = 1,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel1,
                MealID = mel1.ID
            };
            orders.Add(ord1);
            orders1.Add(ord1);

            var ord2 = new Order()
            {
                ID = 2,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel2,
                MealID = mel2.ID
            };
            orders.Add(ord2);
            orders2.Add(ord2);

            var ord3 = new Order()
            {
                ID = 3,
                Amount = 1,
                OrderTime = 11,
                Delivery = true,
                Meal = mel2,
                MealID = mel2.ID
            };
            orders.Add(ord3);
            orders2.Add(ord3);

            var ord4 = new Order()
            {
                ID = 8,                
                Amount = 1,
                OrderTime = 10,
                Delivery = true
            };

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).WithBuitinOrders(orders).Create();

            var repository = new UnitOfWork(mockContext);

            var ord = repository.OrderRepository.GetOrderByMeal(2);

            repository.OrderRepository.AddOrder(ord4, mel2);

            if (id == 1)
            {
                ord = repository.OrderRepository.GetOrderByMeal(id);
                orders1 = ord;

                Assert.AreEqual(4, orders.Count);
                Assert.AreEqual(1, orders1.Count);
            }

            else
            {
                ord = repository.OrderRepository.GetOrderByMeal(id);
                orders2 = ord;
                Assert.AreEqual(4, orders.Count);
                Assert.AreEqual(3, orders2.Count);
            }
           


        }
        #endregion



    }
}

using System.Collections.Generic;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Data.Entity;
using System.Linq;
using System;
using Moq;


namespace OrderingFood.Test
{
    class MockDBContext
    {
        private ICollection<Administrator> mockAdministrators { get; set; }
        private ICollection<Restaurant> mockRestaurants { get; set; }
        private ICollection<Meal> mockMeals { get; set; }
        private ICollection<Order> mockOrders { get; set; }

        private Action saveChangesInvocationCallback { get; set; }

        public MockDBContext()
        {
            mockAdministrators = new List<Administrator>();
            mockRestaurants = new List<Restaurant>();
            mockMeals = new List<Meal>();
            mockOrders = new List<Order>();

            saveChangesInvocationCallback = () => { };
        }

        public MockDBContext WithBuitinAdministrators(ICollection<Administrator> admin)
        {
            mockAdministrators = admin;
            return this;
        }

        public MockDBContext WithBuitinRestaurants(ICollection<Restaurant> rest)
        {
            mockRestaurants = rest;
            return this;
        }

        public MockDBContext WithBuitinMeals(ICollection<Meal> meal)
        {
            mockMeals = meal;
            return this;
        }

        public MockDBContext WithBuitinOrders(ICollection<Order> order)
        {
            mockOrders = order;
            return this;
        }

        public MockDBContext WithSaveChangesCallback(Action callback)
        {
            saveChangesInvocationCallback = callback;
            return this;
        }

        public IOrderingContext Create()
        {
            var moq = new Mock<IOrderingContext>();

            var moqAdministrator = new Mock<DbSet<Administrator>>();
            var admins = mockAdministrators.AsQueryable();

            moqAdministrator.As<IQueryable<Administrator>>().Setup(a => a.Provider).Returns(admins.Provider);
            moqAdministrator.As<IQueryable<Administrator>>().Setup(a => a.Expression).Returns(admins.Expression);
            moqAdministrator.As<IQueryable<Administrator>>().Setup(a => a.ElementType).Returns(admins.ElementType);
            moqAdministrator.As<IQueryable<Administrator>>().Setup(a => a.GetEnumerator()).Returns(() =>
            {
                var it = admins.GetEnumerator();
                it.Reset();
                return it;
            });
            moqAdministrator.Setup(a => a.Add(It.IsAny<Administrator>())).Callback<Administrator>(arg => mockAdministrators.Add(arg));
            moqAdministrator.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(arg => mockAdministrators.SingleOrDefault(a => a.ID == (int)(arg[0])));
            moq.Setup(a => a.Administrators).Returns(moqAdministrator.Object);


            var moqMeal = new Mock<DbSet<Meal>>();
            var meals = mockMeals.AsQueryable();

            moqMeal.As<IQueryable<Meal>>().Setup(m => m.Provider).Returns(meals.Provider);
            moqMeal.As<IQueryable<Meal>>().Setup(m => m.Expression).Returns(meals.Expression);
            moqMeal.As<IQueryable<Meal>>().Setup(m => m.ElementType).Returns(meals.ElementType);
            moqMeal.As<IQueryable<Meal>>().Setup(m => m.GetEnumerator()).Returns(() => {
                var it = meals.GetEnumerator();
                it.Reset();
                return it;
            });
            moqMeal.Setup(m => m.Add(It.IsAny<Meal>())).Callback<Meal>(arg => mockMeals.Add(arg));
            moqMeal.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(arg => mockMeals.SingleOrDefault(m => m.ID == (int)(arg[0])));
            moq.Setup(m => m.Meals).Returns(moqMeal.Object);


            var moqOrder = new Mock<DbSet<Order>>();
            var orders = mockOrders.AsQueryable();

            moqOrder.As<IQueryable<Order>>().Setup(o => o.Provider).Returns(meals.Provider);
            moqOrder.As<IQueryable<Order>>().Setup(o => o.Expression).Returns(meals.Expression);
            moqOrder.As<IQueryable<Order>>().Setup(o => o.ElementType).Returns(meals.ElementType);
            moqOrder.As<IQueryable<Order>>().Setup(o => o.GetEnumerator()).Returns(() => {
                var it = orders.GetEnumerator();
                it.Reset();
                return it;
            });
            moqOrder.Setup(o => o.Add(It.IsAny<Order>())).Callback<Order>(arg => mockOrders.Add(arg));
            moqOrder.Setup(o => o.Find(It.IsAny<object[]>())).Returns<object[]>(arg => mockOrders.SingleOrDefault(o => o.ID == (int)(arg[0])));
            moq.Setup(o => o.Orders).Returns(moqOrder.Object);


            var moqRestaurant = new Mock<DbSet<Restaurant>>();
            var restaurants = mockRestaurants.AsQueryable();

            moqRestaurant.As<IQueryable<Restaurant>>().Setup(r => r.Provider).Returns(meals.Provider);
            moqRestaurant.As<IQueryable<Restaurant>>().Setup(r => r.Expression).Returns(meals.Expression);
            moqRestaurant.As<IQueryable<Restaurant>>().Setup(r => r.ElementType).Returns(meals.ElementType);
            moqRestaurant.As<IQueryable<Restaurant>>().Setup(r => r.GetEnumerator()).Returns(() => {
                var it = restaurants.GetEnumerator();
                it.Reset();
                return it;
            });
            moqRestaurant.Setup(r => r.Add(It.IsAny<Restaurant>())).Callback<Restaurant>(arg => mockRestaurants.Add(arg));
            moqRestaurant.Setup(r => r.Find(It.IsAny<object[]>())).Returns<object[]>(arg => mockRestaurants.SingleOrDefault(r => r.ID == (int)(arg[0])));
            moq.Setup(r => r.Restaurants).Returns(moqRestaurant.Object);

            moq.Setup(m => m.SaveChanges()).Returns(1).Callback(saveChangesInvocationCallback);

            return moq.Object;
        }
    }
}

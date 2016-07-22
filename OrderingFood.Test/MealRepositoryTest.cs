using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingFood.DataAccess.UnitOfWork;
using System.Collections.Generic;
using OrderingFood.Data.Models;

namespace OrderingFood.Test
{
    [TestClass]
    public class MealRepositoryTest
    {

        #region getting-meals-from-restaurant
        [TestMethod]
        public void GetMealByRestaurant()
        {
            var melas = new List<Meal>();
            melas.Add(new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                RestaurantID = 2,
                Active = true

            });

            melas.Add(new Meal()
            {
                ID = 5,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 2,
                Active = true
            });

            melas.Add(new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 1,
                Active = true
            });

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName="Kafana",
                Address="Trosna bb",
                Telephone="+38123555666",
                Active=false,
                Administrators=null,
                Meals=melas
            });

            restaurants.Add(new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = null,
                Meals=melas                
            });

           

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).Create();

            var repository = new UnitOfWork(mockContext);

            var mil = repository.MealRepository.GetMealByRestaurant(1);

            Assert.AreEqual(1, mil.Count);

        }
        #endregion

        #region adding-new-mels-to-restaurant
        [TestMethod]
        public void AddMealToRestaurants()
        {
            var melas = new List<Meal>();
            melas.Add(new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                RestaurantID = 2,
                Active = true

            });

            melas.Add(new Meal()
            {
                ID = 5,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 2,
                Active = true
            });

            melas.Add(new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                RestaurantID = 1,
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

            var obrok = new Meal();
            {
                obrok.ID = 10;
                obrok.MealName = "Capricoza";
                obrok.CategoryName = "Pizza";
                obrok.Price = 400.00;
                obrok.Active = true;
                obrok.RestaurantID = 1;
            }

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).Create();

            var repository = new UnitOfWork(mockContext);

            repository.MealRepository.AddMeal(obrok);

            var mil = repository.MealRepository.GetMealByRestaurant(1);

            Assert.AreEqual(4, melas.Count);
            Assert.AreEqual(2, mil.Count);


        }
        #endregion

    }
}

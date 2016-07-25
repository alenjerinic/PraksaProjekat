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
            var restaurants = new List<Restaurant>();
            var melas = new List<Meal>();

            var rest1 = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas
            };
            restaurants.Add(rest1);

            var rest2 = new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = null,
                Meals = melas
            };
            restaurants.Add(rest2);

            var obr1 = new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                Restaurant = rest1,
                RestaurantID = rest1.ID,
                Active = true
            };
            melas.Add(obr1);

            var obr2 = new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant = rest2,
                RestaurantID = rest2.ID,
                Active = true
            };
            melas.Add(obr2);
            
            var obr3 = new Meal()
            {
                ID = 3,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant = rest2,
                RestaurantID = rest2.ID,
                Active = true
            };
            melas.Add(obr3);           


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
            int id = 2;
            var melas = new List<Meal>();
            var melas1 = new List<Meal>();
            var melas2 = new List<Meal>();
            var restaurants = new List<Restaurant>();
            var mil = new List<Meal>();

            var rest1 = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = melas1          
            };
            restaurants.Add(rest1);

            var rest2 = new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = null,
                Meals = melas2
            };
            restaurants.Add(rest2);

            var obr1 = new Meal()
            {
                ID = 1,
                MealName = "Index",
                CategoryName = "Sendvic",
                Price = 200.00,
                Restaurant = rest1,
                RestaurantID=rest1.ID,
                Active = true
            };
            melas.Add(obr1);
            melas1.Add(obr1);


            var obr2 = new Meal()
            {
                ID = 2,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant=rest2,
                RestaurantID = rest2.ID,
                Active = true
            };
            melas.Add(obr2);
            melas2.Add(obr1);

            var obr3 = new Meal()
            {
                ID = 3,
                MealName = "Index Mali",
                CategoryName = "Sendvic",
                Price = 150.00,
                Restaurant=rest2,
                RestaurantID = rest2.ID,
                Active = true
            };
            melas.Add(obr3);
            melas2.Add(obr1);

            var obr4 = new Meal();
            {
                obr4.ID = 4;
                obr4.MealName = "Capricoza";
                obr4.CategoryName = "Pizza";
                obr4.Price = 400.00;
                obr4.Active = true;                               
            }

            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinMeals(melas).Create();

            var repository = new UnitOfWork(mockContext);

            repository.MealRepository.AddMeal(obr4,rest2);

            if (id == 1)
            {
                mil = repository.MealRepository.GetMealByRestaurant(id);
                melas1 = mil;
                
                Assert.AreEqual(4, melas.Count);
                Assert.AreEqual(1, melas1.Count);
            }

            else
            {
                mil = repository.MealRepository.GetMealByRestaurant(id);
                melas2 = mil;
                Assert.AreEqual(4, melas.Count);
                Assert.AreEqual(3, melas2.Count);
            }


        }
        #endregion

    }
}

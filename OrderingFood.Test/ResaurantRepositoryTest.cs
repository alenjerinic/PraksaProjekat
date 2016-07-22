using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingFood.DataAccess.UnitOfWork;
using System.Collections.Generic;
using OrderingFood.Data.Models;

namespace OrderingFood.Test
{
    [TestClass]
    public class ResaurantRepositoryTest
    {

        #region adding-new-restaurant-to-list-of-restaurants
        [TestMethod]
        public void AddRestaurantToRestaurants()
        {
            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = null,
                Meals = null
            });

            restaurants.Add(new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = null,
                Meals = null
            });

            var rest = new Restaurant()
            {
                ID=10,
                RestaurantName="Idemo",
                Address="Barska bb",
                Telephone="+381235856985",
                Active=true
            };


            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).Create();

            var repository = new UnitOfWork(mockContext);

            repository.RestaurantRepository.AddRestaurant(rest);

            Assert.AreEqual(3, restaurants.Count);
        }
        #endregion

    }
}

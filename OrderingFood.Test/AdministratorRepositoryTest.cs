using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingFood.DataAccess.UnitOfWork;
using System.Collections.Generic;
using OrderingFood.Data.Models;

namespace OrderingFood.Test
{
    [TestClass]
    public class AdministratorRepositoryTest
    {

        #region getting-admins-from-restaurant
        [TestMethod]
        public void GetAdministratorByRestaurant()
        {
            var admins = new List<Administrator>();
            admins.Add(new Administrator()
            {
                ID = 1,
                AdministratorName="Asad",
                RestaurantID=1

            });

            admins.Add(new Administrator()
            {
                ID = 2,
                AdministratorName = "Sadam",
                RestaurantID = 1

            });

            admins.Add(new Administrator()
            {
                ID = 3,
                AdministratorName = "Milun",
                RestaurantID = 2

            });

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = admins,
                Meals = null
            });

            restaurants.Add(new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = admins,
                Meals = null
            });



            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinAdministrators(admins).Create();

            var repository = new UnitOfWork(mockContext);

            var admini = repository.AdministratorRepository.GetAdministratorsByRestaurant(1);

            Assert.AreEqual(2, admini.Count);

        }
        #endregion

        #region adding-new-admins-to-restaurant
        [TestMethod]
        public void AddAdministratorsToRestaurant()
        {

            var admins = new List<Administrator>();
            admins.Add(new Administrator()
            {
                ID = 1,
                AdministratorName = "Asad",
                RestaurantID = 1

            });

            admins.Add(new Administrator()
            {
                ID = 2,
                AdministratorName = "Sadam",
                RestaurantID = 1

            });

            admins.Add(new Administrator()
            {
                ID = 3,
                AdministratorName = "Milun",
                RestaurantID = 2

            });

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = admins,
                Meals = null
            });

            restaurants.Add(new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = admins,
                Meals = null
            });

            var admini = new Administrator()
            {
                ID = 4,
                AdministratorName = "Zivadinka",
                RestaurantID = 1

            };



            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinAdministrators(admins).Create();

            var repository = new UnitOfWork(mockContext);

            repository.AdministratorRepository.AddAdministrator(admini);

            var admincic = repository.AdministratorRepository.GetAdministratorsByRestaurant(1);

            Assert.AreEqual(4, admins.Count);
            Assert.AreEqual(3, admincic.Count);


        }
        #endregion

    }
}

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
            var restaurants = new List<Restaurant>();
            var admins = new List<Administrator>();

            var rest1 = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = admins,
                Meals = null
            };
            restaurants.Add(rest1);

            var rest2 = new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = admins,
                Meals = null
            };
            restaurants.Add(rest1);


            var admin1=new Administrator()
            {
                ID = 1,
                AdministratorName="Asad",
                Restaurant=rest1,
                RestaurantID=rest1.ID
            };
            admins.Add(admin1);

            var admin2 = new Administrator()
            {
                ID = 2,
                AdministratorName = "Sadam",
                Restaurant = rest1,
                RestaurantID = rest1.ID
            };
            admins.Add(admin2);

            var admin3 = new Administrator()
            {
                ID = 3,
                AdministratorName = "Milun",
                Restaurant = rest2,
                RestaurantID = rest2.ID
            };
            admins.Add(admin3);

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
            int id = 2;
            var restaurants = new List<Restaurant>();
            var admins = new List<Administrator>();
            var admins1 = new List<Administrator>();
            var admins2 = new List<Administrator>();
            var admini = new List<Administrator>();

            var rest1 = new Restaurant()
            {
                ID = 1,
                RestaurantName = "Kafana",
                Address = "Trosna bb",
                Telephone = "+38123555666",
                Active = false,
                Administrators = admins1,
                Meals = null
            };
            restaurants.Add(rest1);

            var rest2 = new Restaurant()
            {
                ID = 2,
                RestaurantName = "Kafanica",
                Address = "sabanova",
                Telephone = "+38123444666",
                Active = true,
                Administrators = admins2,
                Meals = null
            };
            restaurants.Add(rest2);


            var admin1 = new Administrator()
            {
                ID = 1,
                AdministratorName = "Asad",
                Restaurant = rest1,
                RestaurantID=rest1.ID
            };
            admins.Add(admin1);
            admins1.Add(admin1);

            var admin2 = new Administrator()
            {
                ID = 2,
                AdministratorName = "Sadam",
                Restaurant = rest2,
                RestaurantID=rest2.ID
            };
            admins.Add(admin2);
            admins2.Add(admin2);

            var admin3 = new Administrator()
            {
                ID = 3,
                AdministratorName = "Milun",
                Restaurant=rest2,
                RestaurantID=rest2.ID
            };
            admins.Add(admin3);
            admins2.Add(admin3);


            var admin4 = new Administrator()
            {
                ID = 4,
                AdministratorName = "Zivadinka"
            };


            var mockContext = new MockDBContext().WithBuitinRestaurants(restaurants).WithBuitinAdministrators(admins).Create();

            var repository = new UnitOfWork(mockContext);

            repository.AdministratorRepository.AddAdministrator(admin4,rest2);

            if (id == 1)
            {
                admini = repository.AdministratorRepository.GetAdministratorsByRestaurant(id);
                admins1 = admini;

                Assert.AreEqual(4, admins.Count);
                Assert.AreEqual(3, admins1.Count);
            }

            else
            {
                admini = repository.AdministratorRepository.GetAdministratorsByRestaurant(id);
                admins2 = admini;

                Assert.AreEqual(4, admins.Count);
                Assert.AreEqual(3, admins2.Count);
            }

        }
        #endregion

    }
}

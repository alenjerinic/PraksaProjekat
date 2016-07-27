using System.Collections.Generic;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Linq;
using System;

namespace OrderingFood.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(IOrderingContext context) : base(context)
        {
        }

        //public List<Order> GetOrderByMeal(int id)
        //{
        //    var result = (from orders in _context.Orders
        //                  from meals in _context.Meals
        //                  where meals.ID == orders.MealID && meals.ID == id
        //                  select orders).ToList();
        //    return result;
        //}

        public List<Order> GetOrderByMeal(int idO)
        {
            

            var resO = new List<Order>();
            resO = _context.Orders.Where(o => o.Meal.ID == idO).ToList();
            return resO;
        }


        public void AddOrder(Order narudzba, Meal obrok)
        {
            var order = new Order()
            {
                ID = narudzba.ID,
                UserName = narudzba.UserName,
                Amount = narudzba.Amount,
                OrderTime = narudzba.OrderTime,
                Delivery = narudzba.Delivery,
                Meal = _context.Meals.Find(obrok.ID),
                MealID = obrok.ID
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

        }
    }
}
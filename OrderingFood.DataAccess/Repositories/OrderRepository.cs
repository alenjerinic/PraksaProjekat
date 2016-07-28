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
      

        public List<Order> GetOrdersByMeal(int idO)
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


        public void UpdateOrder(Order narudzba, Meal obrok)
        {
            var order = new Order()
            {
                ID = narudzba.ID,
                UserName = narudzba.UserName,
                Amount = narudzba.Amount,
                Delivery = narudzba.Delivery,
                OrderTime = narudzba.OrderTime,
                Meal = _context.Meals.Find(obrok.ID),
                MealID = obrok.ID
            };

            var o = _context.Orders.Find(narudzba.ID);

            o.ID = order.ID;
            o.UserName = order.UserName;
            o.OrderTime = order.OrderTime;
            o.Amount = order.Amount;
            o.Delivery = order.Delivery;
            o.Meal = order.Meal;
            o.MealID = order.MealID;

            _context.SaveChanges();
        }
    }
}
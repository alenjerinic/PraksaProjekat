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

        public List<Order> GetOrderByMeal(string name)
        {
            var result = (from orders in _context.Orders
                          from meals in _context.Meals
                          where meals.ID == orders.MealID && meals.MealName == name
                          select orders).ToList();
            return result;
        }


        public Order AddOrder(int amaunt, DateTime time,bool delivery, Meal meal)
        {
            var order = new Order()
            {
                Amount = amaunt,
                OrderTime=time.Hour,
                Delivery=delivery,
                Meal=meal
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;            
        } 


        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new ArgumentException("Specified order does not exist", nameof(id));
            }

            if (order.OrderTime > 12)
            {
                throw new InvalidOperationException("Order can not be deleted, order time expired");
            }
            order.Amount = 0;
            order.Meal = null;
            _context.SaveChanges();
        }


        public void UpdateOrder(int id, int amaunt, DateTime time, bool delivery, Meal meal)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new ArgumentException("Specified order does not exist", nameof(id));
            }

            order.Amount = amaunt;
            order.OrderTime = time.Hour;
            order.Delivery = delivery;
            order.Meal = meal;
            _context.SaveChanges();

        }

    }
}
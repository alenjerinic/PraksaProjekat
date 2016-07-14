using OrderingFood.Data.Models;
using OrderingFood.Data.Context;
using System.Collections.Generic;
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


        public void AddOrder(int id)
        {
            var order = _context.Orders.Find(id);
        }
        /*
         * public void FulfillOrder(int orderID, EmployeeRole worker)
        {
            if (worker==EmployeeRole.COOK)
            {
                throw new ArgumentException("Specified employee can't fulfill orders", nameof(worker));
            }
            var order = restourantContext.Orders.Find(orderID);
            if (order == null)
            {
                throw new ArgumentException("Specified order does not exist", nameof(orderID));
            }
            if (order.Delivered)
            {
                throw new ArgumentException("Order is already filfilled", nameof(order));
            }
            order.Delivered = true;
            restourantContext.SaveChanges();
        }
         * 
         */
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
    }
}










/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 *  public void FulfillOrder(int orderID, EmployeeRole worker)
        {
            if (worker==EmployeeRole.COOK)
            {
                throw new ArgumentException("Specified employee can't fulfill orders", nameof(worker));
            }
            var order = restourantContext.Orders.Find(orderID);
            if (order == null)
            {
                throw new ArgumentException("Specified order does not exist", nameof(orderID));
            }
            if (order.Delivered)
            {
                throw new ArgumentException("Order is already filfilled", nameof(order));
            }
            order.Delivered = true;
            restourantContext.SaveChanges();
        }
 * 
 * public void DeleteOrder(int orderID, EmployeeRole worker)
        {
            var order = restourantContext.Orders.Find(orderID);
            if (order == null)
            {
                throw new ArgumentException("Specified order does not exist", nameof(orderID));
            }
            if (order.Delivered)
            {
                throw new InvalidOperationException("Order is fulfilled and can't be deleted");
            }
            order.Quantity = 0;
            order.Delivered = true;
            restourantContext.SaveChanges();

 
     
     
     
     */

using OrderingFood.Data.Context;
using OrderingFood.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.DataAccess.UnitOfWork
{   
        public class UnitOfWork : IDisposable
        {

        private IOrderingContext _context = null;
        private AdministratorRepository _administratorRepository;
        private RestaurantRepository _restaurantRepository;
        private MealRepository _mealRepository;
        private OrderRepository _orderRepository;        

            public UnitOfWork(IOrderingContext context)
            {
                _context = context;
            }           

            public AdministratorRepository AdministratorRepository
            {
                get
                {

                    if (this._administratorRepository == null)
                    {
                        this._administratorRepository = new AdministratorRepository(_context);
                    }
                    return _administratorRepository;
                }
            }

        public RestaurantRepository RestaurantRepository
        {
            get
            {

                if (this._restaurantRepository == null)
                {
                    this._restaurantRepository = new RestaurantRepository(_context);
                }
                return _restaurantRepository;
            }
        }

        public MealRepository MealRepository
        {
            get
            {

                if (this._mealRepository == null)
                {
                    this._mealRepository = new MealRepository(_context);
                }
                return _mealRepository;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {

                if (this._orderRepository == null)
                {
                    this._orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        public void Save()
            {
                _context.SaveChanges();
            }

            private bool disposed = false;
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _context.Dispose();
                    }
                }
                this.disposed = true;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }



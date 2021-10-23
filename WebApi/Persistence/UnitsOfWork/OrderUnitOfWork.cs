using Application.Common.Interfaces;
using Domain.Entities;
using System;

namespace Persistence.UnitsOfWork
{
    public class OrderUnitOfWork : IDisposable, IOrderUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<DishOrder> _dishOrderRepository;
        public OrderUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ??= new GenericRepository<Order>(_context); }
        }

        public IGenericRepository<DishOrder> DishOrderRepository
        {
            get
            {
                return _dishOrderRepository ??= new GenericRepository<DishOrder>(_context);
            }
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
using Application.Common.Interfaces;
using Domain.Entities;
using System;

namespace Persistence.UnitsOfWork
{
    public class DishUnitOfWork : IDisposable, IDishUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Dish> _dishRepository;
        private IGenericRepository<DishIngredient> _dishIngredientRepository;
        public DishUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Dish> DishRepository
        {
            get { return _dishRepository ??= new GenericRepository<Dish>(_context); }
        }

        public IGenericRepository<DishIngredient> DishIngredientRepository
        {
            get
            {
                return _dishIngredientRepository ??= new GenericRepository<DishIngredient>(_context);
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
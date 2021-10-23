using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IOrderUnitOfWork
    {
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<DishOrder> DishOrderRepository { get; }

        public void Save();
    }
}
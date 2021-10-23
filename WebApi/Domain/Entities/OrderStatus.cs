using System.Collections.Generic;

namespace Domain.Entities
{
    public class OrderStatus: BaseEntity
    {
        public string OrderStatusName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

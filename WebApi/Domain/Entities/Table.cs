using System.Collections.Generic;

namespace Domain.Entities
{
    public class Table: BaseEntity
    {
        public string TableDescription { get; set; }
        public int WaiterId { get; set; }
        public Waiter Waiter { get; set; }
        public int TableStatusId { get; set; }
        public TableStatus TableStatus { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

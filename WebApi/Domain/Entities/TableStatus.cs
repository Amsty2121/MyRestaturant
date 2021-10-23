using System.Collections.Generic;

namespace Domain.Entities
{
    public class TableStatus: BaseEntity
    {
        public string TableStatusName { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}

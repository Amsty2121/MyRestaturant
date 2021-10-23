using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Waiter : BaseEntity
    {
        public int UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}

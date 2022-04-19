using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Kitchener:BaseEntity
    {
        public int UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
        public Order Order { get; set; }
    }
}

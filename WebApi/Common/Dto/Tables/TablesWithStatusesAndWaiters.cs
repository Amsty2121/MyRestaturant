using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Tables
{
    public class TablesWithStatusesAndWaiters
    {
        public int Id { get; set; }
        public string TableDescription { get; set; }
        public int WaiterId { get; set; }
        public string WaiterName { get; set; }
        public int TableStatusId { get; set; }
        public string TableStatusName { get; set; }
    }
}

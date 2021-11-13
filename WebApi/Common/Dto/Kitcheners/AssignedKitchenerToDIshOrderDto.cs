using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Kitcheners
{
    public class AssignedKitchenerToDIshOrderDto
    {
        public int Id { get; set; }
        public int OrderNrPortions { get; set; }
        public string OrderDescription { get; set; }
        public int WaiterId { get; set; }
        public string WaiterName { get; set; }
        public int TableId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int KitchenerId { get; set; }
        public string KitchenerName { get; set; }
    }
}

using System.Collections.Generic;

namespace Common.Dto.Kitcheners
{
	public class GetKitchenerDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<int> OrdersId { get; set; }
    }
}

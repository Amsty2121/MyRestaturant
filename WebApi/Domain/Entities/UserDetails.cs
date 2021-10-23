using Domain.Entities.Authorization;

namespace Domain.Entities
{
    public class UserDetails : BaseEntity
    {
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public Waiter Waiter { get; set; }
        public Kitchener Kitchener { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
	}
}
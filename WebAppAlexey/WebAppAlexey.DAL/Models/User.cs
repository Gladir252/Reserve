using System.Collections.Generic;

namespace WebAppAlexey.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Adress = new HashSet<Adress>();
            UserCarrier = new HashSet<UserCarrier>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public int SubscriptionStatusId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Role Role { get; set; }
        public virtual SubscriptionStatus SubscriptionStatus { get; set; }
        public virtual ICollection<Adress> Adress { get; set; }
        public virtual ICollection<UserCarrier> UserCarrier { get; set; }
    }
}

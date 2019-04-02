using System.Collections.Generic;

namespace WebAppAlexey.DAL.Models
{
    public partial class SubscriptionStatus
    {
        public SubscriptionStatus()
        {
            Carrier = new HashSet<Carrier>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Carrier> Carrier { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}

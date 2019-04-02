using System.Collections.Generic;

namespace WebAppAlexey.DAL.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            UserCarrier = new HashSet<UserCarrier>();
        }

        public int Id { get; set; }
        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }
        public string Phone { get; set; }
        public string CarrierLogo { get; set; }
        public bool? Active { get; set; }
        public int SubscriptionStatusId { get; set; }

        public virtual SubscriptionStatus SubscriptionStatus { get; set; }
        public virtual ICollection<UserCarrier> UserCarrier { get; set; }
    }
}

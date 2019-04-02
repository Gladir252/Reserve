namespace WebAppAlexey.BLL.ViewModels
{
    public class CarrierViewModel
    {
        public int CarrierId { get; set; }
        public int UserId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }
        public string Phone { get; set; }
        public string CarrierLogo { get; set; }
        public bool? Active { get; set; }
        public int SubscriptionStatusId { get; set; }
    }
}

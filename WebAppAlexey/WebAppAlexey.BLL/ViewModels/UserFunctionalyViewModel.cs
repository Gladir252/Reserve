namespace WebAppAlexey.BLL.ViewModels
{
    public class UserFunctionalyViewModel
    {
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
        public string CompanyName { get; set; }
        public string CarrierName { get; set; }
    }
}

namespace WebAppAlexey.BLL.ViewModels
{
    public class AdressViewModel
    {
        public int AdressId { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public bool? Active { get; set; }
    }
}

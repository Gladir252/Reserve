namespace WebAppAlexey.DAL.Models
{
    public partial class Adress
    {
        public int Id { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public bool? Active { get; set; }

        public virtual User User { get; set; }
    }
}

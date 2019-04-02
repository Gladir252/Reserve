namespace WebAppAlexey.DAL.Models
{
    public partial class UserCarrier
    {
        public int UserId { get; set; }
        public int CarrierId { get; set; }

        public virtual Carrier Carrier { get; set; }
        public virtual User User { get; set; }
    }
}

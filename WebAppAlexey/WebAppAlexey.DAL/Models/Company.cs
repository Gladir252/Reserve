using System.Collections.Generic;

namespace WebAppAlexey.DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}

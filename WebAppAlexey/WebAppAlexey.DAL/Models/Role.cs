using System.Collections.Generic;

namespace WebAppAlexey.DAL.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}

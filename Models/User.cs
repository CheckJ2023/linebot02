using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class User
    {
        public User()
        {
            UserAliases = new HashSet<UserAlias>();
        }

        public long UserId { get; set; }
        public string? UserName { get; set; }

        public virtual ICollection<UserAlias> UserAliases { get; set; }
    }
}

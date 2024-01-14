using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class UserAlias
    {
        public UserAlias()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long UserAliasId { get; set; }
        public long? UserId { get; set; }
        public string? UserNameAlias { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

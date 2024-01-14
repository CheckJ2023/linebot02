using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long UserAliasId { get; set; }
        public long MealAliasId { get; set; }
        public int? Quantity { get; set; }
        public DateTime Datetime { get; set; }

        public virtual MealAlias MealAlias { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual UserAlias UserAlias { get; set; } = null!;
    }
}

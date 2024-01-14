using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class MealAlias
    {
        public MealAlias()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long MealAliasId { get; set; }
        public long? MealId { get; set; }
        public string? Alias { get; set; }
        public int? AliasPrice { get; set; }
        public string? AliasPriceUnit { get; set; }
        public string? Description { get; set; }

        public virtual Meal? Meal { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

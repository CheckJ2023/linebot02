using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long OrderId { get; set; }
        public long RestaurantId { get; set; }
        public DateTime Datetime { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

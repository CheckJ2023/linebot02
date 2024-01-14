using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Orders = new HashSet<Order>();
            RestaurantAliases = new HashSet<RestaurantAlias>();
        }

        public long RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RestaurantAlias> RestaurantAliases { get; set; }
    }
}

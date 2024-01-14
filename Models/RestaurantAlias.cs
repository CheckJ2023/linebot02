using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class RestaurantAlias
    {
        public long RestaurantAliasId { get; set; }
        public long? RestaurantId { get; set; }
        public string? Alias { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
    }
}

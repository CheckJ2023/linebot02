using System;
using System.Collections.Generic;

namespace linebot02.Models
{
    public partial class Meal
    {
        public Meal()
        {
            MealAliases = new HashSet<MealAlias>();
        }

        public long MealId { get; set; }
        public string? MealName { get; set; }
        public int? MealPrice { get; set; }
        public string? MealPriceUnit { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<MealAlias> MealAliases { get; set; }
    }
}

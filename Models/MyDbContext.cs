using Microsoft.EntityFrameworkCore;

namespace linebot02.Services;

public class MyDbContext: DbContext
{

    public MyDbContext(DbContextOptions<MyDbContext> options) 
    : base(options)
    {

    }

    DbSet<RestaurantDAO>? Restaurants {get;set;}
    DbSet<MealDAO>? Meals {get;set;}
    DbSet<MealAliasDAO>? Meal_alias {get;set;}


}
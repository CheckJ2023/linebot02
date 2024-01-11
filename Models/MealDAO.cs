namespace linebot02.Models;

public class MealDAO
{
    public long MealId {get; set;}
    public string? MealName {get; set;}
    public List<MealAliasDAO>? MealAliasList {get;set;}
}


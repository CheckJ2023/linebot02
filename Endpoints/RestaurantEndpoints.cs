namespace linebot02.Endpoints;
public static class RestaurantEndpoints
{
    private static readonly RestaurantService restaurantService= new();
   
    public static WebApplication MapRestaurantEndpoints(this WebApplication app) {
        
        app.MapGet("/restaurant/GetAll", async () =>
        {
                    try{
                    var result = await restaurantService.GetAll();
                    Console.WriteLine(result);
                        return result == null?
                        new RestaurantResponse(1,"無資料") :
                        new RestaurantResponse(0,"取得成功",result);
                    }catch (Exception ex)
                    {
                        //回覆訊息
                        return new RestaurantResponse(1,"取得失敗:"+ex.Message);
                    }
        }).WithName("GetAllRestaurant");

        app.MapGet("/restaurant/Get", async (int id) =>
        {
                    try{
                        var result = await restaurantService.Get(id);
                        // var result=restaurantService.RestaurantData;
                        return result == null?
                        new RestaurantResponse(1,"無資料") :
                        new RestaurantResponse(0,"取得成功",result);
                    }catch (Exception ex)
                    {
                        //回覆訊息
                        return new RestaurantResponse(1,"取得失敗:"+ex.Message);
                    }
        }).WithName("GetRestaurant");

        app.MapPost("/restaurant/Post", async (RestaurantDTO RestaurantData) =>
        {
                    try{
                        var result = await restaurantService.Add(RestaurantData);
                        // var result=restaurantService.RestaurantData;
                        
                        return result.Length == 0 ?
                        new RestaurantResponse(0,"新增成功"):
                        new RestaurantResponse(1,"新增失敗:"+result);
                    }catch (Exception ex)
                    {
                        //回覆訊息
                        return new RestaurantResponse(1,"新增失敗:"+ex.Message);
                    }
        }).WithName("PostRestaurant");

        app.MapPut("/restaurant/Put", async (RestaurantDTO RestaurantData) =>
        {
                    try{
                        if (await restaurantService.Get(RestaurantData.RestaurantId) == null)
                            return new RestaurantResponse(1,"無此資料");

                        var result = await restaurantService.Update(RestaurantData);
                        // var result=restaurantService.RestaurantData;
                        
                        return result.Length == 0 ?
                        new RestaurantResponse(0,"更新成功"):
                        new RestaurantResponse(1,"更新失敗:"+result);
                    }catch (Exception ex)
                    {
                        //回覆訊息
                        return new RestaurantResponse(1,"更新失敗:"+ex.Message);
                    }
        }).WithName("PutRestaurant");

        app.MapDelete("/restaurant/Delete", async (int id) =>
        {
                    try{
                        
                        if (await restaurantService.Get(id) == null)
                            return new RestaurantResponse(1,"無此資料");

                        var result = await restaurantService.Delete(id);
                
                        return result.Length == 0 ?
                        new RestaurantResponse(0,"刪除成功"):
                        new RestaurantResponse(1,"刪除失敗:"+result);
                    }catch (Exception ex)
                    {
                        //回覆訊息
                        return new RestaurantResponse(1,"刪除失敗:"+ex.Message);
                    }
        }).WithName("NameRestaurant");


        app.MapGet("/config", (IConfiguration configuration) =>
        {
            var defaultConnetion = configuration["ConnectionStrings:DefaultConnection"];
            var database = configuration["ConnectionStrings:Database"];
            //沒有特別設定時，development有相同的會先抓development.json的檔

            if (!string.IsNullOrEmpty(defaultConnetion) && !string.IsNullOrEmpty(database))
            {
                var response = new
                {
                    DefaultCon = defaultConnetion,
                    Databa = database
                };
                return Results.Ok(response);

            }
            else {
                return Results.Text("取JSON設定檔的API未成功");
            }
        });

        return app;
    }
}
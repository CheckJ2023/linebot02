namespace linebot02.Models;

public class RestaurantResponse

{
    public int Code { get; set; }
    public string? Message { get; set; }
    public RestaurantDTO? RestaurantData { get; set; }
    public List<RestaurantDTO>? RestaurantDataList { get; set; }
    public RestaurantResponse(int Code,string? Message){
        this.Code=Code;
        this.Message=Message;
    }
    public RestaurantResponse(int Code,string? Message,RestaurantDTO? RestaurantData){
        this.Code=Code;
        this.Message=Message;
        this.RestaurantData=RestaurantData;
    }
    public RestaurantResponse(int Code,string? Message,List<RestaurantDTO>? RestaurantDataList){
        this.Code=Code;
        this.Message=Message;
        this.RestaurantDataList=RestaurantDataList;
    }

}
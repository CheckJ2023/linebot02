// using Microsoft.EntityFrameworkCore;
namespace linebot02.Startup;
public static class MyConfiguration
{
    
    
    public static IConfiguration SetMyConfiguration(this IConfiguration Configuration, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            Console.WriteLine(Configuration["WebHookConnectionStrings:DefaultConnectionId"]);
            Console.WriteLine(Configuration["WebHookConnectionStrings:DefaultConnection"]);
            Console.WriteLine(Configuration["ChatGPTConnectionStrings:DefaultConnection"]);
        }
        //本來想在這拿環境設定但沒有,所以沒做什麼
        return Configuration;
    }
}
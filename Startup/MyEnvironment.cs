// using Microsoft.EntityFrameworkCore;
namespace linebot02.Services;
public static class MyEnvironment
{
    public static IHostEnvironment SetMyConfiguration(this IHostEnvironment env)
    {       
        if (env.IsDevelopment())
        {
            Console.WriteLine(env.EnvironmentName);
        }
        //本來想在這拿環境設定但沒有,所以沒做什麼
        return env;
    }
}
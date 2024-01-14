namespace linebot02.Endpoints;
public static class ChatBotEndpoints
{
    // private const string AdminUserId = ""; //👉repleace it with your Admin User Id
    
    private static readonly LineWebHookLibrary LineWebHookLibrary=new();

    private static string RemovePrefixMethod(string? prefixStr,string? originalStr){
        
        var result ="";
        Console.WriteLine(originalStr);
        if (prefixStr!=null && originalStr!=null){
            if (originalStr.StartsWith(prefixStr)){
                int startIndex = prefixStr.Length;
                result = originalStr.Remove(0,startIndex);
            }
        }
        Console.WriteLine(result);
        return result; 
    }
    public static WebApplication MapChatBotEndpoints(this WebApplication app) {
        
        app.MapPost("/MyLineBotWebHook", (HttpRequest request) =>
        {
            Console.WriteLine(request);
                Console.WriteLine(app.Configuration["WebHookConnectionStrings:DefaultConnectionId"]);
                string AdminUserId=
                RemovePrefixMethod("AdminUserId=",app.Configuration["WebHookConnectionStrings:DefaultConnectionId"]);
                LineWebHookLibrary.ChannelAccessToken=
                RemovePrefixMethod("ChannelAccessToken=",app.Configuration["WebHookConnectionStrings:DefaultConnection"]);
                string OpenAIAPIKey=
                RemovePrefixMethod("OpenAIAPIKey=",app.Configuration["ChatGPTConnectionStrings:DefaultConnection"]);
                Console.WriteLine(OpenAIAPIKey);

            try
            {
                var ReceivedMessage = LineWebHookLibrary.GetReceivedMessage(request);
                
                //設定ChannelAccessToken
                // LineWebHookLibrary.ChannelAccessToken = ""; //👉repleace it with your Channel Access Token
                //配合Line Verify
                if (ReceivedMessage.events == null || ReceivedMessage.events.Count() <= 0 ||
                    ReceivedMessage.events.FirstOrDefault()?.replyToken == "00000000000000000000000000000000") return Results.Ok();
                //取得Line Event
                var LineEvent = ReceivedMessage.events.FirstOrDefault();
                var responseMsg = "";

                //準備回覆訊息
                if (LineEvent?.type.ToLower() == "message" && LineEvent.message.type == "text")
                {
                    if (LineEvent.message.text.ToLower().StartsWith("\\"))
                    {
                        responseMsg = OpenAIService.getResponseFromGPT(LineEvent.message.text,OpenAIAPIKey);
                        LineWebHookLibrary.ReplyMessage(LineEvent.replyToken, responseMsg);

                    }
                }
                else if (LineEvent?.type.ToLower() == "message")
                {
                    responseMsg = $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ";
                    // this.PushMessage(AdminUserId, responseMsg);
                    Console.WriteLine(AdminUserId + $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ");
                }
                else
                {
                    responseMsg = $"收到 event : {LineEvent?.type} ";
                    // this.PushMessage(AdminUserId, responseMsg);
                    Console.WriteLine(AdminUserId + $"收到 event : {LineEvent?.type} ");
                }
                //回覆訊息

            }
            catch (Exception ex)
            {
                //回覆訊息
                // this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                Console.WriteLine(AdminUserId + ": 發生錯誤:\n" + ex.Message);
                //response OK

            }
            return Results.Ok();
        });

        return app;
    }
}
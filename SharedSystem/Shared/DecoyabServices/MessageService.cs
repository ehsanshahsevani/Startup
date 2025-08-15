using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DecoyabServices;

public class MessageProperty
{
    public string SinglePhoneNumber { get; }
    public string[] MultiPhoneNumber { get; }
    public string ContentText { get; }
    public string LineNumber => "0985000145";
}

public interface IMessageService
{
    Task<int> GetRemainChargeAccountAsync(string apiKey);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="pattern"></param>
    /// <param name="number">شماره ای که پیام به آن ارسال می شود</param>
    /// <param name="token">شماره توکن</param>
    /// <param name="adminNumber">شماره موبایل ادمین</param>
    /// <param name="token2">فامیل شخص واریز کننده</param>
    /// <param name="token3">مبلغ واریز</param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendLookupAsync(string apiKey, string pattern, string number, string token, string token2 = "", string token3 = "");
    
    Task<HttpResponseMessage> SendLookupAsync(string apiKey, string pattern, string number, int token, string adminNumber = "", string token2 = "", string token3 = "");
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="pattern"></param>
    /// <param name="number">شماره ای که پیام به آن ارسال می شود</param>
    /// <param name="token">شماره فاکتور</param>
    /// <param name="token2">کد پیگیری</param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendChangeSendAsync(string apiKey, string pattern, string number, long token, string token2, string token3);
    Task<HttpResponseMessage> SendChangeSendAsync(string apiKey, string pattern, string number, string token, string token2 = "", string token3 = "");
    void SendSMS(MessageProperty message);
}

public class MessageService : IMessageService
{
    public void SendSMS(MessageProperty message)
    {
        var request = WebRequest.Create("http://ippanel.com/services.jspd");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        string numbers = JsonConvert.SerializeObject(string.IsNullOrEmpty(message.SinglePhoneNumber) ? message.MultiPhoneNumber : new[] { message.SinglePhoneNumber });
        byte[] content = Encoding.UTF8.GetBytes($"op=send&uname=vestas&pass=abc12345&message={message.ContentText}&to={numbers}&from=+985000145");
        request.ContentLength = content.Length;
        var dataStream = request.GetRequestStream();
        dataStream.Write(content, 0, content.Length);
        dataStream.Close();
        dataStream.Dispose();
    }

    public async Task<int> GetRemainChargeAccountAsync(string apiKey)
    {
        try
        {
            string jsonResult = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.kavenegar.com");
                jsonResult = await client.GetStringAsync($"/v1/{apiKey}/account/info.json");
            }
            var jsonFormat = JObject.Parse(jsonResult);
            if (jsonFormat.SelectToken("return.status").Value<short>() == 200)
            {
                return jsonFormat.SelectToken("entries.remaincredit").Value<int>() / 10;
            }
            return 0;
        }
        catch
        {
            return 0;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="pattern"></param>
    /// <param name="number">شماره ای که پیام به آن ارسال می شود</param>
    /// <param name="token">شماره توکن</param>
    /// <param name="adminNumber">شماره موبایل ادمین</param>
    /// <param name="token2">فامیل شخص واریز کننده</param>
    /// <param name="token3">مبلغ واریز</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> SendLookupAsync(string apiKey, string pattern, string number, int token, string adminNumber, string token2 = "", string token3 = "")
    {
        string existToken = string.Empty;

        if (token == 1)
        {
            existToken = "پرداخت";
        }
        else if (token == 2)
        {
            existToken = "عملیات";
        }
        else if (token == 3)
        {
            existToken = adminNumber;
        }
        else if (token == 4)
        {
            existToken = adminNumber;
        }
        else
        {
            existToken = token.ToString();
        }   
        
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://api.kavenegar.com");
            var data = new Dictionary<string, string>
            {
                { "type", "sms" },
                { "receptor", number },
                { "token", existToken},
                { "token2", token2},
                { "token3", token3},
                { "template", pattern },
            };
            return await client.PostAsync($"/v1/{apiKey}/verify/lookup.json", new FormUrlEncodedContent(data));
        }
    }

    public async Task<HttpResponseMessage> SendChangeSendAsync(string apiKey, string pattern, string number, long token, string token2, string token3)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://api.kavenegar.com");
            var data = new Dictionary<string, string>
            {
                { "type", "sms" },
                { "receptor", number },
                { "token", token.ToString()},
                { "token2", token2},
                { "token3", token3},
                { "template", pattern },
            };
            return await client.PostAsync($"/v1/{apiKey}/verify/lookup.json", new FormUrlEncodedContent(data));
        }
    }

    public async Task<HttpResponseMessage> SendLookupAsync(string apiKey, string pattern, string number, string token, string token2 = "", string token3 = "")
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.kavenegar.com");

        var data = new Dictionary<string, string>
            {
                { "type", "sms" },
                { "receptor", number },
                { "token", token},
                { "token2", token2},
                { "token3", token3},
                { "template", pattern },
            };

        var result = await client.PostAsync(
            $"/v1/{apiKey}/verify/lookup.json",
            new FormUrlEncodedContent(data));

        return result;
    }

    public async Task<HttpResponseMessage> SendChangeSendAsync(
        string apiKey, string pattern, string number, string token, string token2 = "", string token3 = "")
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.kavenegar.com");
        var data = new Dictionary<string, string>
            {
                { "type", "sms" },
                { "receptor", number },
                { "token", token.ToString()},
                { "token2", token2},
                { "token3", token3},
                { "template", pattern },
            };
        return await client.PostAsync($"/v1/{apiKey}/verify/lookup.json", new FormUrlEncodedContent(data));
    }
}

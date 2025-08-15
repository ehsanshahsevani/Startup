using RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Services;

public class UrlQueryService
{
    private readonly NavigationManager _navigationManager;

    public UrlQueryService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    // خواندن کل پارامترهای کوئری از URL
    public IDictionary<string, string> GetQueryParameters()
    {
        var uri = new Uri(_navigationManager.Uri);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

        return query.AllKeys
            .Where(k => k != null)
            .ToDictionary(k => k!, k => query[k!]!);
    }

    // خواندن یک پارامتر خاص
    public string? GetParameter(string key)
    {
        var parameters = GetQueryParameters();

        if (parameters.TryGetValue(key, out var value))
        {
            return value;
        }

        return null;
    }

    // ست کردن یا حذف چند پارامتر همزمان و فقط یک بار NavigateTo زدن
    public void SetParameters(IDictionary<string, string?> parameters)
    {
        var uri = new Uri(_navigationManager.Uri);
        var baseUri = uri.GetLeftPart(UriPartial.Path);

        var query = GetQueryParameters();

        foreach (var kvp in parameters)
        {
            if (string.IsNullOrEmpty(kvp.Value))
            {
                query.Remove(kvp.Key);
            }
            else
            {
                query[kvp.Key] = kvp.Value;
            }
        }

        var newQuery = string.Join("&", query.Select(kvp =>
            $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

        var newUri = string.IsNullOrEmpty(newQuery) ? baseUri : $"{baseUri}?{newQuery}";

        if (_navigationManager.Uri != newUri)
        {
            _navigationManager.NavigateTo(newUri, forceLoad: false);
        }
    }

    // تابع برای گرفتن و تنظیم پارامترهای URL در کلاس‌های RequestParameters
    public T? GetRequestParameters<T>(T requestParams) where T : RequestParameters
    {
        var queryParams = GetQueryParameters();

        // برای هر ویژگی از کلاس، مقدار پارامترهای URL را تنظیم می‌کنیم
        foreach (var prop in typeof(T).GetProperties())
        {
            // نام ویژگی کلاس را به کوچک تبدیل می‌کنیم
            var propName = prop.Name.ToLower(); 

            // بررسی اگر نام ویژگی با پارامتر "currentpage" در URL تطابق داشت
            if (propName == "pagenumber" && queryParams.ContainsKey("currentpage"))
            {
                var queryValue = queryParams["currentpage"];
                prop.SetValue(requestParams, Convert.ToInt32(queryValue)); // تخصیص مقدار به PageNumber
            }
            else if (queryParams.ContainsKey(propName))
            {
                var queryValue = queryParams[propName];
                var propType = prop.PropertyType;

                // برای تنظیم مقدار ویژگی‌ها
                if (propType == typeof(int))
                {
                    if (int.TryParse(queryValue, out int intValue))
                    {
                        prop.SetValue(requestParams, intValue);
                    }
                }
                else if (propType == typeof(string))
                {
                    prop.SetValue(requestParams, queryValue);
                }
            }
        }

        return requestParams;
    }
}
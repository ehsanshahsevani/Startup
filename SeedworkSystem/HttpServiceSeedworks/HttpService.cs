using System.Net;
using System.Text;
using System.Text.Json;
using System.Collections;
using Enums.SharedService;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using SampleResult;

namespace HttpServiceSeedworks;

public abstract class HttpService
{
	private readonly HttpClient _httpClient;
	protected string BaseUrl { get; private set; }
	protected string? BaseApi { get; private set; }

	/// <summary>
	/// Initializes the HTTP service with a base URL.
	/// </summary>
	protected HttpService(string baseUrl)
	{
		BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
		_httpClient = new HttpClient(new HttpClientHandler());
	}

	/// <summary>
	/// Sets the base API path segment used in request URLs.
	/// </summary>
	protected void SetBaseApi(string baseApi)
	{
		if (string.IsNullOrWhiteSpace(baseApi))
		{
			throw new ArgumentException("Base URL cannot be empty or null.", nameof(baseApi));
		}

		BaseApi = baseApi;
	}

	/// <summary>
	/// Changes the base URL of the HTTP client.
	/// </summary>
	protected void ChangeBaseUrl(string newBaseUrl)
	{
		if (string.IsNullOrWhiteSpace(newBaseUrl))
		{
			throw new ArgumentException("Base URL cannot be empty or null.", nameof(newBaseUrl));
		}

		BaseUrl = newBaseUrl;
	}

	/// <summary>
	/// Sends a GET request and deserializes the response to type O.
	/// </summary>
	protected async Task<O?> GetAsync<O>(string route, Dictionary<string, string>? queryParams = null)
	{
		string url = BuildUrl(route, queryParams);
		return await SendRequest<O>(HttpMethod.Get, url);
	}

	/// <summary>
	/// Sends a POST request with input data, supporting JSON or multipart form-data content.
	/// </summary>
	protected async Task<O?> PostAsync<I, O>(string url, I data, ContentType contentType = ContentType.Json,
		Dictionary<string, IFormFile>? fileDict = null, List<KeyValuePair<string, IFormFile>>? fileList = null)
	{
		return await SendRequest<I, O>(HttpMethod.Post, url, data, contentType, fileDict, fileList);
	}

	/// <summary>
	/// Sends a POST request without input model, used when no body is required.
	/// </summary>
	protected async Task<O?> PostAsync<O>(string url, ContentType contentType = ContentType.Json)
	{
		return await SendRequest<object, O>(HttpMethod.Post, url, default, contentType);
	}

	/// <summary>
	/// Sends a PUT request with optional files and form data.
	/// </summary>
	protected async Task<O?> PutAsync<I, O>(string url, I data, ContentType contentType = ContentType.Json,
		Dictionary<string, IFormFile>? fileDict = null, List<KeyValuePair<string, IFormFile>>? fileList = null)
	{
		try
		{
			return await SendRequest<I, O>(HttpMethod.Put, url, data, contentType, fileDict, fileList);
		}
		catch (Exception ex)
		{
			throw new Exception($"[PutAsync] Error: {ex.Message}");
		}
	}

	/// <summary>
	/// Sends a DELETE request and returns a deserialized response.
	/// </summary>
	protected async Task<O?> DeleteAsync<O>(string route, Dictionary<string, string>? queryParams = null)
	{
		string url = BuildUrl(route, queryParams);
		return await SendRequest<O>(HttpMethod.Delete, url);
	}

	/// <summary>
	/// Overload for sending a request without binding an input model.
	/// </summary>
	private async Task<O?> SendRequest<O>(HttpMethod method, string url, object? data = null,
		ContentType contentType = ContentType.Json)
	{
		return await SendRequest<object, O>(method, url, data, contentType);
	}

	/// <summary>
	/// Core method to send HTTP requests supporting input data, file uploads, and different content types.
	/// </summary>
	private async Task<O?> SendRequest<I, O>(HttpMethod method, string url, I? data = default,
		ContentType contentType = ContentType.Json, Dictionary<string, IFormFile>? fileDict = null,
		List<KeyValuePair<string, IFormFile>>? fileList = null)
	{
		try
		{
			using var request = new HttpRequestMessage(method, $"{BaseUrl}/{BaseApi}/{url}");

			// Skip body for GET/DELETE. For others, handle JSON or multipart.
			if (method != HttpMethod.Get && method != HttpMethod.Delete)
			{
				if (contentType == ContentType.MultipartFormData)
				{
					var multipart = new MultipartFormDataContent();
					AddFormData(multipart, data);
					AddFiles(multipart, fileDict, fileList);
					request.Content = multipart;
				}
				else
				{
					request.Content = contentType switch
					{
						ContentType.Json => new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8,
							"application/json"),
						ContentType.FormUrlEncoded when data is Dictionary<string, string> formData =>
							new FormUrlEncodedContent(formData),
						_ => null
					};
				}
			}

			using var response = await _httpClient.SendAsync(request);
			string responseData = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				throw new Exception($"Resource not found: {responseData}: 404");
			}

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				Converters = { new NullableDateTimeConverter() }
			};

			return JsonSerializer.Deserialize<O>(responseData, options);
		}
		catch (Exception ex)
		{
			throw new Exception($"Request failed: {ex.Message}");
		}
	}


	/// <summary>
	/// Builds a full query string from route and parameters.
	/// </summary>
	private string BuildUrl(string route, Dictionary<string, string>? queryParams)
	{
		if (queryParams == null || queryParams.Count == 0)
		{
			return route;
		}

		var query = string.Join("&",
			queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
		return $"{route}?{query}";
	}

	/// <summary>
	/// Adds form data properties from the input model (excluding file fields).
	/// </summary>
	private void AddFormData<I>(MultipartFormDataContent multipart, I data)
	{
		if (data == null) return;

		var props = typeof(I).GetProperties();
		foreach (var prop in props)
		{
			var value = prop.GetValue(data);
			if (value == null) continue;

			// Skip files to handle them separately.
			if (value is IFormFile || value is IEnumerable<IFormFile>) continue;

			if (value is IDictionary dict)
			{
				foreach (var key in dict.Keys)
				{
					if (dict[key] != null)
					{
						multipart.Add(new StringContent(dict[key]?.ToString() ?? string.Empty), $"{prop.Name}[{key}]");
					}
				}
			}
			else if (value is IEnumerable enumerable && value is not string)
			{
				int i = 0;
				foreach (var item in enumerable)
				{
					if (item != null)
					{
						multipart.Add(new StringContent(item?.ToString() ?? string.Empty), $"{prop.Name}[{i++}]");
					}
				}
			}
			else
			{
				multipart.Add(new StringContent(value.ToString()), prop.Name);
			}
		}
	}

	/// <summary>
	/// Adds files into multipart content from either dictionary or list.
	/// </summary>
	private void AddFiles(MultipartFormDataContent multipart, Dictionary<string, IFormFile>? fileDict,
		List<KeyValuePair<string, IFormFile>>? fileList)
	{
		if (fileDict != null)
		{
			foreach (var kvp in fileDict)
			{
				var file = kvp.Value;
				var fileContent = new StreamContent(file.OpenReadStream());
				fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
				multipart.Add(fileContent, kvp.Key, file.FileName);
			}
		}

		if (fileList != null)
		{
			foreach (var kvp in fileList)
			{
				var file = kvp.Value;
				var fileContent = new StreamContent(file.OpenReadStream());
				fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
				multipart.Add(fileContent, kvp.Key, file.FileName);
			}
		}
	}

	/// <summary>
	/// Extracts properties (integers >= 0 or non-empty strings) from object into dictionary.
	/// </summary>
	protected Dictionary<string, string> GetPropertiesWithValues(object obj)
	{
		var result = new Dictionary<string, string>();
		var objType = obj.GetType();
		var properties = objType.GetProperties();

		foreach (var property in properties)
		{
			var value = property.GetValue(obj);

			if (value is int intValue && intValue >= 0 ||
			    value is string stringValue && !string.IsNullOrEmpty(stringValue))
			{
				result[property.Name] = value.ToString()!;
			}
		}

		return result;
	}

	private class NullableDateTimeConverter : JsonConverter<DateTime?>
	{
		public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var value = reader.GetString();
				if (DateTime.TryParse(value, out var result))
					return result;
			}
			else if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			throw new JsonException("Invalid DateTime format for nullable DateTime.");
		}

		public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
		{
			if (value.HasValue)
				writer.WriteStringValue(value.Value.ToString("o")); // ISO 8601
			else
				writer.WriteNullValue();
		}
	}
}
using System;
using System.Text.Json;

namespace FreeERP.Services
{
	public class MyServices
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public MyServices(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
        }

		public async Task<Dictionary<string, object>?> Method()
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new (){
					RequestUri = new Uri("url"),
					Method = HttpMethod.Get
				};
				HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

				Stream stream = httpResponseMessage.Content.ReadAsStream();
				StreamReader streamReader = new(stream);
				string response = streamReader.ReadToEnd();
				Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

				return responseDictionary;
            }
		}
	}
}


//The main goal of this class is to provide HTML from valid webpages. Its only function receive a webpage link and return its HTML as a String.
using System.Net.Http;

namespace DigimonAPI.services;

public static class HP // Stands for HTML Provider
{
	private static readonly HttpClient _httpClient = new HttpClient
	{
		Timeout = TimeSpan.FromSeconds(15) //This is a good practice to deal with timeout.
	};

	
	//Main function
	public static async Task<String?> GetHTML(String url)
	{
		try
		{
			//User agent config. This is necessary because some webpage may need an User Agent for security reasons
			_httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
			//try to acces the webpage
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			//Ensure the access success
			response.EnsureSuccessStatusCode();
			//Convert the HTML into a String
			string htmlContent = await response.Content.ReadAsStringAsync();
			//Return the content
			return htmlContent;
		}
		catch (TaskCanceledException timeoutEx)
		{
			//Timeout exception
			Console.WriteLine("[ERROR]HTML PROVIDER - Timeout: " + timeoutEx.Message);
			return null;
		}
		catch (Exception ex)
		{
			Console.WriteLine("[ERROR]HTML PROVIDER: " + ex.Message);
			//The null return will be properly treated in the usage of the function.
			return null;
		}
	}
}
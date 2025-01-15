using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DigimonAPI.Services;

public static class OpenAIService
{
	private const string OpenAIEndpoint = "https://api.openai.com/v1/chat/completions";
	private const string apiKey = "";

	public static async Task<string?> GetSkillDescription(string skillName)
	{
		try
		{
				if (string.IsNullOrWhiteSpace(apiKey))
			{
				throw new ArgumentException("OpenAI API key cannot be null or empty.", nameof(apiKey));
			}

			if (string.IsNullOrWhiteSpace(skillName))
			{
				throw new ArgumentException("Skill name cannot be null or empty.", nameof(skillName));
			}

			string prompt = $"Generate a description for a skill in the Digimon universe. The skill is called {skillName}. Use the style, and terminology consistent with official Digimon references, ensuring it fits within the lore and mechanics of the franchise. The response must be a string containing only the skill description, keep the description under 200 characters, avoid mentioning any specific Digimon names in the description or battle mechanics, focus on visual description.";

			var requestBody = new
			{
				model = "gpt-4-turbo",
				messages = new[]
				{
				new { role = "user", content = prompt }
    },
				max_tokens = 50, 
				temperature = 0.7 
			};

			using var httpClient = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(OpenAIEndpoint),
				Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
			};

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);


			var response = await httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			var jsonResponse = JsonDocument.Parse(responseBody);
			return jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine($"Error fetching skill description: {ex.Message}");
			return "An error occurred while generating the skill description.";
		}
	}
}

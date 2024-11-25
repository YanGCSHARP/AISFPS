using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TechEase.Models;

public class AmChat
{
    static string questionTemplate = @"{
        ""model"": ""gpt-3.5-turbo"",
        ""messages"": [
            {
                ""role"": ""system"",
                ""content"": ""Если ты понимаешь что пользователь просит тебя подобрать конкретное комплектующее, то в первой строке ты обязан написать 1, а далее написать три варианта, каждый из которых в новой строке, каждый из вариантов будет названием продукта. Первый продукт бюджетный, второй средний и третий дорогой. ТОЛЬКО названия, без лишних слов. Если ты понимаешь что пользователь просит тебя подобрать сборку, или помочь собрать компьютер ты ОБЯЗАН в первой строке написать 2, а далее написать три сборки: бюджетная, средняя, максимальнаяя. Каждая в отдельной строке в виде Видеокарта/процессор/RAM(только обьем и DDR). Без лишних слов.""
            },
            {
                ""role"": ""user"",
                ""content"": ""{0}""
            }
        ]
    }";

    public async Task<string> GetAnswer(string question)
    {
        //question = CleanseString(question);
        string jsonContent = questionTemplate.Replace("{0}", question);

        var httpClient = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
            Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), "application/json" },
                { HttpRequestHeader.Authorization.ToString(), "Bearer " + "API_KEY"}, ///////////////////////
            },
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };

        var response = await httpClient.SendAsync(request);
        string answer = await GetClearAnswerFromResponse(response);

        // Сохранение в DB, по итогу имеем question и answer
        return answer;
    }

    static async Task<string> GetClearAnswerFromResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var parsedResponse = JObject.Parse(responseContent);

            var choices = parsedResponse["choices"];
            if (choices == null) return "*** 1";
            if (choices.Count() == 0) return "*** 2";
            if (choices[0] == null) return "*** 3";
            var message = choices![0]!["message"];
            if (message == null) return "*** 4";
            if (message["content"] == null) return "*** 5";
            var answer = message["content"]!.ToString();

            return answer;
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        return $"Error: {response.StatusCode}, Content: {errorContent}";
    }
    static string CleanseString(string input)
    {
        return JsonConvert.ToString(input);
    }
}
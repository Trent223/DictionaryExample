
//API Interaction Setup
using DictionaryExample;
using System.Text.Json;

string baseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";

HttpClient client = new HttpClient
{
    BaseAddress = new Uri(baseUrl)
};

Console.WriteLine("Enter a word to receive the definition");
Console.WriteLine("Enter 'q' to quit");
var userInput = Console.ReadLine();


while(userInput != "q")
{
    var response = await client.GetAsync(userInput);
    var content = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    List<DictionaryResponseModel> definitionInfoList;
    try
    {
        definitionInfoList = JsonSerializer.Deserialize<List<DictionaryResponseModel>>(content, options);
    }
    catch(JsonException)
    {
        Console.WriteLine("Not a real word sorry");
        Console.WriteLine("Enter a word to receive the definition");
        Console.WriteLine("Enter 'q' to quit");
        userInput = Console.ReadLine();
        continue;
    }
    

    Console.WriteLine($"Word: {definitionInfoList.FirstOrDefault().Word}");

    foreach (var definitionInfo in definitionInfoList)
    {
        foreach(var meaning in definitionInfo.Meanings)
        {
            var partOfSpeech = meaning.PartOfSpeech;
            foreach(var definition in meaning.Definitions)
            {
                Console.WriteLine($"{partOfSpeech} : {definition.Definition}");
            }
        }
    }    

    Console.WriteLine("Enter a word to receive the definition");
    Console.WriteLine("Enter 'q' to quit");
    userInput = Console.ReadLine();
}
 
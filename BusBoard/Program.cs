using Newtonsoft.Json;
using BusBoard.Models;

namespace BusBoard;

internal class Program
{
    async static Task BusBoard()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.tfl.gov.uk/StopPoint/");
        var json = await client.GetStringAsync("490000129R/Arrivals");
        var predictions = JsonConvert.DeserializeObject<List<ArrivalPrediction>>(json);

        if (predictions is null)
        {
            throw new Exception("Unable to retrieve predictions from JSON response");
        }

        Console.WriteLine($"The bus {predictions[0].LineName}, destination: {predictions[0].DestinationName} is arriving in {predictions[0].TimeToStation/60} minutes.");
    }
    
    async static Task Main(string[] args)
    {
        await BusBoard();
    }
}
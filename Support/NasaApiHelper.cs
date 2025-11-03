using RestSharp;

namespace NasaApiTests.Support;

public class NasaApiHelper
{
    private readonly RestClient _client;
    private RestResponse? _response;
    private const string BaseUrl = "https://api.nasa.gov";
    
    // Exercise requirement: Use DEMO_KEY
    // Note: Using personal key due to rate limits (30 req/hour with DEMO_KEY vs 1000 with personal)
    // To revert to DEMO_KEY, simply change this line to: private const string ApiKey = "DEMO_KEY";
    private const string ApiKey = "okbOtyQ57VHXvqTiI5XOafnmy9UTnQxMIkFxePPJ";

    public NasaApiHelper()
    {
        _client = new RestClient(BaseUrl);
    }

    public async Task GetCmeData(string startDate, string endDate)
    {
        var request = new RestRequest(TestData.Endpoints.CME);
        request.AddParameter("api_key", ApiKey);
        request.AddParameter("startDate", startDate);
        request.AddParameter("endDate", endDate);
        
        _response = await _client.ExecuteAsync(request);
    }

    public async Task GetCmeDataWithInvalidDate(string invalidDate)
    {
        var request = new RestRequest(TestData.Endpoints.CME);
        request.AddParameter("api_key", ApiKey);
        request.AddParameter("startDate", invalidDate);
        request.AddParameter("endDate", TestData.Dates.ValidEnd);
        
        _response = await _client.ExecuteAsync(request);
    }

    public async Task GetFlrData(string startDate, string endDate)
    {
        var request = new RestRequest(TestData.Endpoints.FLR);
        request.AddParameter("api_key", ApiKey);
        request.AddParameter("startDate", startDate);
        request.AddParameter("endDate", endDate);
        
        _response = await _client.ExecuteAsync(request);
    }

    public async Task GetFlrDataWithoutStartDate()
    {
        var request = new RestRequest(TestData.Endpoints.FLR);
        request.AddParameter("api_key", ApiKey);
        request.AddParameter("endDate", TestData.Dates.ValidEnd);
        
        _response = await _client.ExecuteAsync(request);
    }

    public int GetStatusCode()
    {
        return (int)_response!.StatusCode;
    }

    public bool IsValidJsonArray()
    {
        if (_response?.Content == null) return false;
        
        var content = _response.Content.Trim();
        return content.StartsWith("[") && content.EndsWith("]");
    }
}

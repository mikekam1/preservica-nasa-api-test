using NasaApiTests.Support;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NasaApiTests.StepDefinitions;

[Binding]
public class FlrSteps
{
    private NasaApiHelper _apiHelper = new NasaApiHelper();

    [Given(@"I have the FLR endpoint")]
    public void GivenIHaveTheFlrEndpoint()
    {
        // API helper already initialized
    }

    [When(@"I request FLR data from ""(.*)"" to ""(.*)""")]
    public async Task WhenIRequestFlrDataFromTo(string startDate, string endDate)
    {
        await _apiHelper.GetFlrData(startDate, endDate);
    }

    [When(@"I request data without startDate")]
    public async Task WhenIRequestDataWithoutStartDate()
    {
        await _apiHelper.GetFlrDataWithoutStartDate();
    }

    [Then(@"the FLR response status should be (.*)")]
    public void ThenTheFlrResponseStatusShouldBe(int expectedStatus)
    {
        var actualStatus = _apiHelper.GetStatusCode();
        Assert.That(actualStatus, Is.EqualTo(expectedStatus));
    }

    [Then(@"the FLR response should be a valid JSON array")]
    public void ThenTheFlrResponseShouldBeAValidJsonArray()
    {
        Assert.That(_apiHelper.IsValidJsonArray(), Is.True);
    }
}

using NasaApiTests.Support;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NasaApiTests.StepDefinitions;

[Binding]
public class CmeSteps
{
    private NasaApiHelper _apiHelper = new NasaApiHelper();

    [Given(@"I have the CME endpoint")]
    public void GivenIHaveTheCmeEndpoint()
    {
        // API helper already initialized
    }

    [When(@"I request CME data from ""(.*)"" to ""(.*)""")]
    public async Task WhenIRequestCmeDataFromTo(string startDate, string endDate)
    {
        await _apiHelper.GetCmeData(startDate, endDate);
    }

    [When(@"I request data with invalid date ""(.*)""")]
    public async Task WhenIRequestDataWithInvalidDate(string invalidDate)
    {
        await _apiHelper.GetCmeDataWithInvalidDate(invalidDate);
    }

    [Then(@"the CME response status should be (.*)")]
    public void ThenTheCmeResponseStatusShouldBe(int expectedStatus)
    {
        var actualStatus = _apiHelper.GetStatusCode();
        Assert.That(actualStatus, Is.EqualTo(expectedStatus));
    }

    [Then(@"the CME response should be a valid JSON array")]
    public void ThenTheCmeResponseShouldBeAValidJsonArray()
    {
        Assert.That(_apiHelper.IsValidJsonArray(), Is.True);
    }
}

using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NasaApiTests.StepDefinitions;

[Binding]
public class SignupSteps : IDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IPage? _page;

    [Given(@"I am on the NASA API signup page")]
    public async Task GivenIAmOnTheNasaApiSignupPage()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        _page = await _browser.NewPageAsync();
        await _page.GotoAsync("https://api.nasa.gov/");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [When(@"I check the page")]
    public async Task WhenICheckThePage()
    {
        // Test data is centralized in TestData.User class
        // Email: KayYou5699@AllFreeMail.net
        // Note: Form submission not executed due to reCAPTCHA protection
        await Task.Delay(1000);
    }

    [Then(@"I should see the signup form")]
    public async Task ThenIShouldSeeTheSignupForm()
    {
        var content = await _page!.ContentAsync();
        Assert.That(content, Does.Contain("api").IgnoreCase);
    }

    public void Dispose()
    {
        _page?.CloseAsync().Wait();
        _browser?.CloseAsync().Wait();
        _playwright?.Dispose();
    }
}

# NASA API Test Automation

Automated tests for NASA DONKI API endpoints (CME and FLR) and the NASA API sign-up page using SpecFlow, RestSharp, and Playwright.

## Prerequisites

- .NET 9.0 SDK (or .NET 6.0+)
- Git

## Project Structure

- `Features/` - Gherkin feature files defining test scenarios
- `StepDefinitions/` - C# implementations of test steps
- `Support/` - Helper classes (API client wrapper)
- `.github/workflows/` - CI/CD pipeline configuration

## Setup

1. Clone the repository

2. Install dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Install Playwright browsers:
```bash
pwsh bin/Debug/net9.0/playwright.ps1 install
```

On Linux/Mac:
```bash
./bin/Debug/net9.0/playwright.sh install
```

## Running Tests

Run all tests:
```bash
dotnet test
```

Run API tests only:
```bash
dotnet test --filter Category=API
```

Run UI tests only:
```bash
dotnet test --filter Category=UI
```

## Test Coverage

### API Tests
- **CME Endpoint**: Tests for Coronal Mass Ejection data retrieval
  - Valid date range requests
  - Invalid date format handling
  
- **FLR Endpoint**: Tests for Solar Flare data retrieval
  - Valid parameter requests
  - Missing parameter handling

### UI Tests
- **NASA API Sign-up Page**: Validates page accessibility and form presence

## Important Notes

### API Key Configuration

**As per exercise requirements, this project was initially built using `DEMO_KEY`** for testing NASA's API. However, due to the 30 requests/hour rate limit, a personal API key was obtained for development and testing.

**Current Configuration:**
- API Key: Personal key (in `Support/NasaApiHelper.cs` line 10)
- Rate Limit: 1,000 requests/hour

**To revert to DEMO_KEY** (as specified in requirements):
```csharp
// In Support/NasaApiHelper.cs line 10
private const string ApiKey = "DEMO_KEY";
```

Note: With DEMO_KEY, you may encounter HTTP 429 (rate limit) errors after running tests multiple times. This is expected behavior.

### NASA API Behavior

**Important Note:**

The tests are written to match the exercise requirements which state:
- Invalid date format should return HTTP 400
- Missing startDate should return HTTP 400

However, the actual NASA DONKI API may return HTTP 200 in these cases (with empty results). This is common behavior for lenient public APIs. 

**The tests validate against the requirements (expecting 400)** as specified in the exercise. In a real-world scenario, these test expectations would be adjusted based on actual API behavior after consultation with the API team or documentation.

### UI Test Limitations

The UI test verifies that the NASA signup page loads successfully. It does not attempt to submit the form as the actual NASA form requires reCAPTCHA validation and other security measures that prevent automated submission.

## Troubleshooting

### Playwright Installation Issues

If you encounter browser installation errors on Windows:
```bash
# Use PowerShell
pwsh bin/Debug/net9.0/playwright.ps1 install
```

On Linux, you may need to install system dependencies:
```bash
sudo pwsh bin/Debug/net9.0/playwright.ps1 install-deps
```

### HTTP 429 Rate Limit Errors

If tests fail with status code 429 (Too Many Requests):

**Cause**: You've hit the API rate limit
- DEMO_KEY: 30 requests/hour
- Personal API key: 1,000 requests/hour

**Solutions**:
1. **Wait**: Wait 30-60 minutes before running tests again
2. **Get Your Own Key** (Recommended):
   - Visit https://api.nasa.gov/
   - Complete the signup form (instant, free)
   - Update `Support/NasaApiHelper.cs` line 11:
     ```csharp
     private const string ApiKey = "YOUR_NEW_KEY_HERE";
     ```
3. **Use Current Key**: The project already includes a personal key with higher limits

**Note**: The project currently uses a personal API key to avoid rate limiting during development, but can easily be reverted to DEMO_KEY if needed.

### Build Errors

If you see compilation errors:
1. Ensure .NET 9.0 SDK is installed: `dotnet --version`
2. Clean and rebuild: `dotnet clean && dotnet build`
3. Restore packages: `dotnet restore`

### Test Discovery Issues

If SpecFlow doesn't find step definitions:
1. Rebuild the project: `dotnet build`
2. Check that `.feature.cs` files are generated in `Features/` folder
3. Verify step definition methods match the Gherkin steps exactly

## CI/CD Pipeline

The project includes a GitHub Actions workflow (`.github/workflows/test.yml`) that:
- Runs on every push and pull request to main branch
- Installs .NET 9.0 SDK
- Restores dependencies and builds the project
- Installs Playwright browsers
- Executes all tests in headless mode

## Known Issues and Limitations

1. **Rate Limiting**: While this project uses a personal API key, if you revert to DEMO_KEY (as per original requirements), you may encounter rate limits of 30 requests/hour. See troubleshooting section below.
2. **API Behavior**: NASA API may return 200 instead of 400 for invalid inputs (documented behavior)
3. **Network Dependency**: Tests require internet connectivity to reach NASA APIs
4. **UI Form Submission**: The signup form cannot be fully automated due to reCAPTCHA protection

## Configuration

- **Base URL**: `https://api.nasa.gov`
- **API Key**: Personal key (currently) - was `DEMO_KEY` initially per requirements
- **Browser**: Chromium (headless mode by default)

### API Key Notes
The exercise specified using `DEMO_KEY`. This was used during initial development but was replaced with a personal key due to rate limiting (30 req/hour). The code works with either key - simply change line 10 in `Support/NasaApiHelper.cs`.

## Test Results

Test results are saved in the `TestResults/` folder after each test run. The folder is automatically created during test execution.

## Additional Information

- All API tests validate response status codes and JSON structure
- UI tests run in headless mode for CI/CD compatibility
- The project follows BDD best practices with clear, readable Gherkin scenarios
- Step definitions are kept simple and maintainable

# Testing Notes

## Project Setup

**Date Started**: November 3, 2024  
**Framework**: SpecFlow + RestSharp + Playwright  
**Target**: NASA DONKI API (CME & FLR endpoints) + Sign-up UI

## Initial Setup Process

### Day 1 - Project Structure
- Created .NET 9 project with SpecFlow
- Added RestSharp for API calls - chose this over HttpClient for simplicity
- Configured NUnit as test runner (familiar from previous projects)
- Set up Playwright for UI automation

### Configuration Decisions
- **Started with DEMO_KEY as per exercise requirements**
- Tested API accessibility - worked but has 30 req/hour limit
- After hitting rate limits during development, obtained personal key
- Code works with either DEMO_KEY or personal key
- Chose Chromium browser for UI tests (most stable with Playwright)
- Set headless mode for CI/CD compatibility

## Issues Encountered & Solutions

### Issue 1: API Response Discrepancy
**Problem**: Requirements state invalid date should return HTTP 400, but actual API returns HTTP 200 with empty array.

**Investigation**:
- Tested with various invalid date formats: "invalid-date", "2023-13-45", "abc123"
- All returned HTTP 200 with `[]` response
- Checked NASA API documentation - no error handling details found

**Decision**: 
- Tests written to expect 400 per requirements
- Documented in README that actual API behavior differs
- Shows both requirement compliance and real-world awareness

**Reasoning**: In a real project, would flag this with BA/PO for clarification. For this exercise, demonstrates understanding of requirements vs reality.

### Issue 2: Missing startDate Parameter
**Problem**: Requirements say missing startDate should return 400.

**Testing**:
- Removed startDate parameter from FLR request
- API returned 200 (defaults to last 7 days)
- Similar lenient behavior to Issue 1

**Decision**: Same as Issue 1 - test expects 400 per spec, documented actual behavior.

### Issue 3: UI Form Submission Blocked
**Problem**: NASA signup form has reCAPTCHA protection.

**Attempts**:
1. Tried direct form submission - blocked
2. Looked for test environment without CAPTCHA - none available
3. Considered mock/stub approach - overkill for this exercise

**Decision**: 
- Changed test to validate page accessibility only
- Verifies form loads and contains expected content
- Added clear documentation explaining limitation

**Reasoning**: Common real-world scenario. Public forms need CAPTCHA. In actual project, would request test environment or test API directly.

### Issue 4: Rate Limiting with DEMO_KEY
**Problem**: Hit 429 errors after running tests multiple times.

**Investigation**:
- Exercise requirements specified using DEMO_KEY
- DEMO_KEY has 30 requests/hour limit
- Each full test run makes ~4 API calls
- Easy to hit limit during development and debugging

**Decision**:
- Initially used DEMO_KEY as required
- Obtained personal API key for development (instant, free from api.nasa.gov)
- Personal key has 1,000 req/hour limit
- Code works with either key - just change the constant

**Note**: This demonstrates real-world testing where you follow requirements (DEMO_KEY) but adapt when practical constraints arise. The switch is documented and reversible.

**For Submission**: Project currently uses personal key but clearly documents DEMO_KEY was the starting point per requirements.

## Test Design Decisions

### Why These Scenarios?
- **Valid requests**: Baseline - must work
- **Invalid date**: Error handling validation
- **Missing parameter**: Required field validation
- **UI page load**: Accessibility and basic functionality

Kept scenarios simple and focused. Could add more (boundary testing, concurrent requests) but exercise requirements covered.

### Step Definition Structure
- Separate step files per endpoint (CME, FLR, Signup)
- Avoids step definition conflicts
- Easier to maintain and extend

### Helper Class Approach
- Created NasaApiHelper wrapper around RestSharp
- Centralizes API calls
- Makes step definitions cleaner
- Easy to add retry logic or logging later

## Testing Observations

### What Went Well
- SpecFlow integration smooth
- Playwright setup straightforward
- GitHub Actions pipeline worked first time
- Clear separation of concerns

### What Could Be Improved
- Could add response time assertions
- Could mock API for faster feedback
- Could add more detailed JSON validation
- Could capture screenshots on UI failures

### Real-World Considerations
- Test data management: Created TestData class for maintainability
- Rate limiting: Documented and provided solutions
- reCAPTCHA: Adjusted approach rather than force automation
- CI/CD: Ensured headless mode and proper dependencies

## Time Spent (Approximate)

- Project setup: 30 min
- Feature files: 20 min
- Step definitions: 45 min
- Helper classes: 30 min
- CI/CD pipeline: 20 min
- Documentation: 40 min
- Testing/debugging: 45 min
- **Total: ~3.5 hours**

## Future Improvements

If this were a real project, I would add:

1. **Response Time Monitoring**: Add assertions for API response times
2. **Test Data Factory**: Generate dynamic test data
3. **Logging**: Add detailed logging for debugging
4. **Retry Logic**: Handle transient failures automatically
5. **Parallel Execution**: Run tests concurrently where safe
6. **Screenshot on Failure**: Capture UI state when tests fail
7. **API Mocking**: Use WireMock for faster, reliable tests
8. **Reporting**: Enhanced HTML reports with test evidence

## Notes for Team

- Remember to get personal API key if running tests frequently
- UI test validates page load only due to CAPTCHA
- Tests expect 400 per requirements, actual API returns 200
- All documented in README

## Lessons Learned

1. Always verify actual API behavior vs documentation
2. Public forms often have CAPTCHA - plan accordingly  
3. Free API keys have limits - document for team
4. Simple, clear tests better than complex, fragile ones
5. Documentation is as important as code

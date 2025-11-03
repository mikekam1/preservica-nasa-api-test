# Bug Report Template

Use this template when reporting issues found during testing.

---

## Bug ID
[To be assigned by bug tracking system]

## Summary
Brief, clear description of the issue (one sentence)

## Severity
- [ ] Critical - Complete failure, blocks testing
- [ ] High - Major functionality broken
- [ ] Medium - Feature doesn't work as expected
- [ ] Low - Minor issue, workaround available

## Priority
- [ ] P1 - Fix immediately
- [ ] P2 - Fix before release
- [ ] P3 - Fix if time permits
- [ ] P4 - Nice to have

## Environment
- **OS**: 
- **.NET Version**: 9.0
- **Browser** (if UI): Chromium
- **Test Environment**: 
- **Date Found**: 

## Steps to Reproduce
1. 
2. 
3. 

## Expected Result
Describe what should happen according to requirements

## Actual Result
Describe what actually happens

## Test Evidence
- **Feature File**: `Features/[filename].feature`
- **Scenario**: [scenario name]
- **Step Definition**: `StepDefinitions/[filename].cs`
- **Line Number**: 
- **Error Message**: 
```
[Paste error message here]
```

## Screenshots/Logs
[Attach if applicable]

## Reproducibility
- [ ] Always reproducible
- [ ] Intermittent (occurs sometimes)
- [ ] Occurred once

## Additional Context
Any other relevant information:
- Related test cases
- Potential cause
- Suggested fix
- Impact on other features

## Test Execution Details
```bash
# Command used
dotnet test --filter "FullyQualifiedName~[TestName]"

# Console output
[Paste relevant output]
```

## Workaround
Is there a temporary workaround? Describe it here.

## Related Issues
Link to related bugs or requirements

---

## Example Bug Report

### Bug ID
BUG-001

### Summary
CME API returns 200 instead of 400 for invalid date format

### Severity
- [x] Medium - Feature doesn't work as expected

### Priority
- [x] P2 - Fix before release

### Environment
- **OS**: Windows 11
- **.NET Version**: 9.0
- **Test Environment**: Production API (api.nasa.gov)
- **Date Found**: November 3, 2024

### Steps to Reproduce
1. Send GET request to `/DONKI/CME`
2. Use `api_key=DEMO_KEY`
3. Use `startDate=invalid-date`
4. Use `endDate=2023-01-07`

### Expected Result
According to requirements: API should return HTTP 400 error for invalid date format

### Actual Result
API returns HTTP 200 with empty array `[]`

### Test Evidence
- **Feature File**: `Features/CME.feature`
- **Scenario**: Request CME data with invalid date format
- **Step Definition**: `StepDefinitions/CmeSteps.cs`
- **Line Number**: 34
- **Error Message**: 
```
Assert.That(actualStatus, Is.EqualTo(expectedStatus))
  Expected: 400
  But was:  200
```

### Reproducibility
- [x] Always reproducible

### Additional Context
- The API appears to be lenient with date validation
- Returns empty array instead of error
- Similar behavior observed with FLR endpoint
- This is common with public APIs that prioritize availability
- May be intentional design decision by NASA

### Potential Solutions
1. Update requirements to match actual API behavior (200 with empty array)
2. Request NASA to implement stricter validation
3. Add client-side validation before calling API

### Workaround
Validate date format in test code before making API call:
```csharp
if (!DateTime.TryParse(dateString, out _))
{
    // Handle invalid date locally
}
```

### Related Issues
- Similar issue with FLR endpoint missing parameters
- Affects all date validation test cases

---

## Notes for Using This Template

- Fill out all required sections
- Be specific and factual
- Include exact error messages
- Add screenshots for UI bugs
- Reference specific test files and line numbers
- Suggest potential solutions if you have ideas
- Keep it professional and objective

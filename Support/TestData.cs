namespace NasaApiTests.Support;

public static class TestData
{
    public static class Dates
    {
        public const string ValidStart = "2023-01-01";
        public const string ValidEnd = "2023-01-07";
        public const string InvalidDate = "invalid-date";
    }
    
    public static class User
    {
        public const string Email = "KayYou5699@AllFreeMail.net";
        public const string FirstName = "Test";
        public const string LastName = "User";
    }
    
    public static class Endpoints
    {
        public const string CME = "/DONKI/CME";
        public const string FLR = "/DONKI/FLR";
    }
}

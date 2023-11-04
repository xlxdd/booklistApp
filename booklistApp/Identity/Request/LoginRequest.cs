namespace booklistAPI.Identity.Request
{
    public class LoginRequest
    {
        public string phoneNumber { get; set; }
        public string code { get; set; }
    }
}

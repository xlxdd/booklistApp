namespace booklistDomain.Services
{
    public interface ISMSService
    {
        public Task SendAsync(string phoneNum, params string[] args);
    }
}

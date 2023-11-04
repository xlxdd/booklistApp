using booklistDomain.Services;

namespace booklistInfrastructure.Services
{
    public class SMSService : ISMSService
    {
        public async Task SendAsync(string phoneNum, params string[] args)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(phoneNum + ":");
                foreach (var item in args)
                {
                    Console.WriteLine(item.ToString());
                }
            }) ;
        }
    }
}

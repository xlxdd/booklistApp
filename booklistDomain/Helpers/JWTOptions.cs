using System.ComponentModel.DataAnnotations.Schema;

namespace booklistDomain.Helpers
{
    [NotMapped]
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpireSeconds { get; set; }
    }
}

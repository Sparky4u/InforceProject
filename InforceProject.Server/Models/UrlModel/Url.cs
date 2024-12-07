using Microsoft.EntityFrameworkCore;

namespace InforceProject.Server.Models.UrlModel
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string GenerateShortUrl()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("+", "")
                .Replace("/", "")
                .Substring(0, 8); 
        }
    }
}

using InforceProject.Server.Data;
using InforceProject.Server.Models.UrlModel;
using Microsoft.EntityFrameworkCore;

namespace InforceProject.Server.Services
{
    public class UrlService
    {
        private readonly UrlContext _context;

        public UrlService(UrlContext context)
        {
            _context = context;
        }

        public async Task<Url> CreateShortUrl(string originalUrl, string createdBy)
        {
            if (!IsValidUrl(originalUrl))
                throw new Exception("Invalid URL format.");

            if (await _context.Urls.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl) != null)
                throw new Exception("URL already exists");

            var shortUrl = GenerateShortUrl();
            var url = new Url
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
            };

            _context.Urls.Add(url);
            await _context.SaveChangesAsync();

            return url;
        }

        public async Task<Url> GetUrlById(int id) => await _context.Urls.FindAsync(id);

        public async Task<IEnumerable<Url>> GetAllUrls(string createdBy = null)
        {
            return createdBy != null
                ? await _context.Urls.Where(u => u.CreatedBy == createdBy).ToListAsync()
                : await _context.Urls.ToListAsync();
        }

        public async Task DeleteUrl(int id, string createdBy)
        {
            var url = await _context.Urls.FindAsync(id);
            if (url == null || (url.CreatedBy != createdBy && !IsAdmin(createdBy)))
                throw new Exception("You're not authorized to delete this URL.");

            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
        }

        private string GenerateShortUrl()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _) &&
                   (url.StartsWith("http://") || url.StartsWith("https://"));
        }

        private bool IsAdmin(string user) => user == "admin";
    }
}

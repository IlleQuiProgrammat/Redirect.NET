using Microsoft.EntityFrameworkCore;

namespace Redirect.NET.Models
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {
        }
        public DbSet<Url> Urls { get; set; }
    }
}
using DocumentServiceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentServiceApi.Data
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DocumentEntity> Documents { get; set; }
    }
}

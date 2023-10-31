using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly TimeTrackingContext _context;

        public DocumentRepository(TimeTrackingContext context)
        {
            _context = context;
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetDocumentsNamesAsync(Guid userId, DateTime date)
        {
            return await _context.Documents
                //.Include(document => document.DaysAccounting
                    //.Where(day => day.Date.Date == date.Date))
                //.Where(x => x.UserId == userId)
                .Where(x => x.UserId == userId && x.DaysAccounting.Any(day => day.Date.Date == date.Date))
                .Select(x => x.Name).ToListAsync();
        }

        public async Task<Document> GetUserDocByName(Guid userId, string docName)
        {
            return await _context.Documents.FirstOrDefaultAsync(x => x.UserId == userId && x.Name == docName);
        }

        public async Task UpdateDocument(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }
    }
}

using DocumentServiceApi.Data;
using DocumentServiceApi.Interfaces.Repositories;
using DocumentServiceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentServiceApi.Repositiries
{
    public class DocumentRepository : IDocumentRepository 
    {
        private readonly DocumentContext _context;

        public DocumentRepository(DocumentContext context)
        {
            _context = context;
        }

        public async Task<bool> IsDocumestExist(string fileName, Guid userId)
        {
            var existance = await _context.Documents
                .FirstOrDefaultAsync(x => x.Name == fileName && x.UserId == userId);
            return existance != null ? true : false;
        }

        public async Task<List<DocumentEntity>> GetUserDocuments(Guid userId)
        {
            return await _context.Documents.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<string>> GetUserDocumentsNames(Guid userId)
        {
            return await _context.Documents
                .Where(x => x.UserId == userId && x.Type == "time_tracking")
                .Select(x => x.Name).ToListAsync();
        }

        public async Task AddDocumentAsync(DocumentEntity documentEntity)
        {
            await _context.Documents.AddAsync(documentEntity);
        }
    }
}

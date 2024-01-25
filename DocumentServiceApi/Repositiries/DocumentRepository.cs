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

        public async Task<bool> IsDocumentExist(Guid documentId)
        {
            return await _context.Documents.AsNoTracking()
                .AnyAsync(x => x.Id == documentId);
        }

        public async Task<bool> IsDocumentExist(string documentName, Guid userId)
        {
            return await _context.Documents.AsNoTracking()
                .AnyAsync(x => x.Name == documentName && x.UserId == userId);
        }

        public async Task<List<DocumentEntity>> GetUserDocuments(Guid userId)
        {
            return await _context.Documents.AsNoTracking()
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task AddDocumentAsync(DocumentEntity documentEntity)
        {
            await _context.Documents.AddAsync(documentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<DocumentEntity> GetUserDocumentByName(string name, Guid userId)
        {
            return await _context.Documents.AsNoTracking().
                FirstOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        }

        public async Task<DocumentEntity> GetDocumentById(Guid documentId)
        {
            return await _context.Documents.AsNoTracking().
                FirstOrDefaultAsync(x => x.Id == documentId);
        }
    }
}

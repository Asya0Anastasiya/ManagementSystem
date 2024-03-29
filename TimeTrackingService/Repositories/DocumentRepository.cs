﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Document>> GetAttachedDocumentsNamesAsync(Guid userId, DateTime date)
        {
            return await _context.Documents
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.DaysAccounting.Any(day => day.Date.Date == date.Date))
                .ToListAsync();
        }

        public async Task<List<Document>> GetUserTimeTrackDocsAsync(Guid userId)
        {
            return await _context.Documents
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Document?> GetUserDocByName(Guid userId, string docName)
        {
            return await _context.Documents.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Name == docName);
        }

        public async Task<Document?> GetDocumentById(Guid documentId)
        {
            return await _context.Documents.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == documentId);
        }

        public async Task UpdateDocument(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }
    }
}

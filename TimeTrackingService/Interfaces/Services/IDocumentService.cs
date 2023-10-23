﻿using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDocumentService
    {
        public Task AddDocumentAsync(Document document);

        public Task<List<string>> GetUsersDocumentsNames(Guid userId, DateTime date);
    }
}

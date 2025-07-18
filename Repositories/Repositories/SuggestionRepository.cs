﻿using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DBContext;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SuggestionRepository : GenericRepository<Suggestion>, ISuggestionRepository
    {
        public SuggestionRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Suggestion>> GetAllAsync()
        {
            return await _context.suggestion
            .Where(s => !s.IsDeleted).OrderByDescending(s => s.SentDate)
                .ToListAsync();
        }

        public async Task<Suggestion> GetByIdAsync(Guid id)
        {
            return await _context.suggestion
                .FirstOrDefaultAsync(s => s.SuggestionID == id && !s.IsDeleted);
        }

        public async Task AddAsync(Suggestion suggestion)
        {
            suggestion.SuggestionID = Guid.NewGuid();
            suggestion.SentDate = DateTime.Now;
            suggestion.ActionDate = DateTime.Now;
            await CreateAsync(suggestion);
        }

        public async Task UpdateAsync(Suggestion suggestion)
        {
            suggestion.ActionDate = DateTime.Now;
            await base.UpdateAsync(suggestion);
        }

        public async Task DeleteAsync(Guid id)
        {
            var suggestion = await GetByIdAsync(id);
            if (suggestion != null)
            {
                suggestion.IsDeleted = true;
                await UpdateAsync(suggestion);
            }
        }
    }
}

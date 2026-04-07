using BugManagementAPI.Data;
using BugManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugManagementAPI.Services
{
    public class BugService : IBugService
    {
        private readonly BugDbContext _context;

        public BugService(BugDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bug>> GetAllBugsAsync()
        {
            return await _context.Bugs.ToListAsync();
        }

        public async Task<Bug?> GetBugByIdAsync(int id)
        {
            return await _context.Bugs.FindAsync(id);
        }

        public async Task<Bug> CreateBugAsync(Bug bug)
        {
            bug.CreatedAt = DateTime.UtcNow;
            bug.UpdatedAt = DateTime.UtcNow;
            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();
            return bug;
        }

        public async Task<Bug?> UpdateBugAsync(int id, Bug bug)
        {
            var existingBug = await _context.Bugs.FindAsync(id);
            if (existingBug == null) return null;

            existingBug.Title = bug.Title;
            existingBug.Description = bug.Description;
            existingBug.Status = bug.Status;
            existingBug.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingBug;
        }

        public async Task<bool> DeleteBugAsync(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null) return false;

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

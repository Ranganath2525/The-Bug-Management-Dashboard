using BugManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugManagementAPI.Services
{
    public interface IBugService
    {
        Task<IEnumerable<Bug>> GetAllBugsAsync();
        Task<Bug?> GetBugByIdAsync(int id);
        Task<Bug> CreateBugAsync(Bug bug);
        Task<Bug?> UpdateBugAsync(int id, Bug bug);
        Task<bool> DeleteBugAsync(int id);
    }
}

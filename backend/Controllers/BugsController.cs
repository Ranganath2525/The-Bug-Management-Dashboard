using BugManagementAPI.Models;
using BugManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugsController : ControllerBase
    {
        private readonly IBugService _bugService;

        public BugsController(IBugService bugService)
        {
            _bugService = bugService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugs()
        {
            var bugs = await _bugService.GetAllBugsAsync();
            return Ok(bugs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBug(int id)
        {
            var bug = await _bugService.GetBugByIdAsync(id);
            if (bug == null) return NotFound();
            return Ok(bug);
        }

        [HttpPost]
        public async Task<ActionResult<Bug>> CreateBug(Bug bug)
        {
            var createdBug = await _bugService.CreateBugAsync(bug);
            return CreatedAtAction(nameof(GetBug), new { id = createdBug.Id }, createdBug);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBug(int id, Bug bug)
        {
            var updatedBug = await _bugService.UpdateBugAsync(id, bug);
            if (updatedBug == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(int id)
        {
            var success = await _bugService.DeleteBugAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

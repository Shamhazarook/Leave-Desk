
using LeaveManagement.Api.Data;
using LeaveManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/leaves")]
public class LeavesController : ControllerBase
{
    private readonly AppDbContext _context;

    public LeavesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /api/leaves
    [HttpGet]
    public async Task<IActionResult> GetLeaves()
    {
        var leaves = await _context.LeaveRequests
            .Include(l => l.Employee)
            .OrderByDescending(l => l.Id)
            .ToListAsync();

        return Ok(leaves);
    }

    // POST: /api/leaves
    [HttpPost]
    public async Task<IActionResult> CreateLeave(LeaveRequest leave)
    {
        if (leave.EndDate.Date < leave.StartDate.Date)
        {
            return BadRequest(new
            {
                message = "End date cannot be before start date."
            });
        }

        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == leave.EmployeeId);

        if (!employeeExists)
        {
            return BadRequest(new
            {
                message = "Employee does not exist."
            });
        }

        var validTypes = new[] { "Annual", "Casual", "Medical" };

        if (!validTypes.Contains(leave.LeaveType))
        {
            return BadRequest(new
            {
                message = "Leave type must be Annual, Casual, or Medical."
            });
        }

        leave.Status = "Pending";
        leave.RejectionReason = null;

        _context.LeaveRequests.Add(leave);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLeaves),
            new { id = leave.Id },
            leave);
    }

    // PATCH: /api/leaves/{id}/approve
    [HttpPatch("{id}/approve")]
    public async Task<IActionResult> ApproveLeave(int id)
    {
        var leave = await _context.LeaveRequests.FindAsync(id);

        if (leave == null)
        {
            return NotFound(new { message = "Leave request not found." });
        }

        if (leave.Status != "Pending")
        {
            return BadRequest(new
            {
                message = "Only pending leave requests can be approved."
            });
        }

        leave.Status = "Approved";
        await _context.SaveChangesAsync();

        return Ok(leave);
    }

    // PATCH: /api/leaves/{id}/reject
    [HttpPatch("{id}/reject")]
    public async Task<IActionResult> RejectLeave(
        int id,
        [FromBody] RejectLeaveRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            return BadRequest(new
            {
                message = "A rejection reason is required."
            });
        }

        var leave = await _context.LeaveRequests.FindAsync(id);

        if (leave == null)
        {
            return NotFound(new { message = "Leave request not found." });
        }

        if (leave.Status != "Pending")
        {
            return BadRequest(new
            {
                message = "Only pending leave requests can be rejected."
            });
        }

        leave.Status = "Rejected";
        leave.RejectionReason = request.Reason.Trim();

        await _context.SaveChangesAsync();

        return Ok(leave);
    }
}

public class RejectLeaveRequest
{
    public string Reason { get; set; } = string.Empty;
}

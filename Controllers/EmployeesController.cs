
using LeaveManagement.Api.Data;
using LeaveManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _context.Employees
            .OrderBy(e => e.Name)
            .ToListAsync();

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        var existingEmployee = await _context.Employees
            .AnyAsync(e => e.Email == employee.Email);

        if (existingEmployee)
        {
            return Conflict(new
            {
                message = "An employee with this email already exists."
            });
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEmployees),
            new { id = employee.Id },
            employee);
    }
}

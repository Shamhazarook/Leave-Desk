
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Api.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    public List<LeaveRequest> LeaveRequests { get; set; } = new();
}


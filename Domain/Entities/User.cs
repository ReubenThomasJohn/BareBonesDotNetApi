using System.ComponentModel.DataAnnotations;

namespace BareBonesDotNetApi.Entities;

public class User
{
    [Key]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    // Add other user-related properties as needed

    // Navigation property for UserStatus
    public int UserStatusId { get; set; }
    public UserStatus Status { get; set; }
}
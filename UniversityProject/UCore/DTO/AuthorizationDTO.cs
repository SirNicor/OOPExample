namespace UCore;
using System.Security.Cryptography;

public class AuthorizationDto
{
    public long? Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string? Salt { get; set; }
    public int[] Role { get; set; }
    public long? IdAdmin { get; set; }
    public long? IdTeacher { get; set; }
    public bool? BlackList { get; set; }
}

public class AuthorizationForGetJwtToken
{
    public long? Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class PasswordAndSalt
{
    public string Password { get; set; }
    public string Salt { get; set; }
}
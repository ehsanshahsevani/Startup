namespace JwtSettings.Models;

public class Jwt : object
{
    public Jwt() : base()
    {
    }

    public string? SecretKey { get; set; }
    public int TokenExpiresInMinutes { get; set; }
}
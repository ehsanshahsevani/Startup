namespace JwtSettings.Models;

public class TokenAttachment : object
{
    public TokenAttachment(string fileUrl) : base()
    {
        FileUrl = fileUrl;
    }
    
    public string FileUrl { get; set; }
}
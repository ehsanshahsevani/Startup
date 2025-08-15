namespace Domain.Base;

public static class ServerKeyConstant
{
    public const string Key = "A3F25A42-048B-43B8-A40F-DB6D4E65DE77";
}

public class BaseEntity : DomainSeedworks.BaseEntity
{
    public BaseEntity() : base()
    {
        ServerId = ServerKeyConstant.Key;

        if (string.IsNullOrEmpty(ServerId) == true)
        {
            throw new NullReferenceException("ServerId cannot be null");
        }
    }
}

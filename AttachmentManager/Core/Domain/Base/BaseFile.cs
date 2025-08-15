namespace Domain.Base;

public static class ServerKeyConstant
{
    public const string Key = "0E2783BA-D831-4ADB-8D19-BDB044C8C0A4";
}

public abstract class BaseEntity : DomainSeedworks.BaseEntity
{
    public BaseEntity()
    {
        ServerId = ServerKeyConstant.Key;
    }
}
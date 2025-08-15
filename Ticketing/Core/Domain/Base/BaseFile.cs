using HttpServices.ProjectManager;
using ViewModels.ProjectManager;

namespace Domain.Base;

public static class ServerKeyConstant
{
    public const string Key = "15414A3E-250F-4C01-B648-19176642BB54";
}

public abstract class BaseEntity : DomainSeedworks.BaseEntity
{
    public BaseEntity()
    {
        ServerId = ServerKeyConstant.Key;
    }

    public async Task SendUserRelationToTopServerAsync(string fieldName, string userId, string subSystemId)
    {
        var service = new UserRelationService();

        var userRelation =
            new UserRelationResponseViewModel(ServerId, subSystemId, userId, Id, fieldName);

        var result = await service.AddAsync(userRelation);

        if (result.IsFailed) throw new Exception("Failed to send user relation");
    }
}

public abstract class BaseSubSystem : DomainSeedworks.BaseSubSystem
{
}

public abstract class BaseAttachment : DomainSeedworks.BaseAttachment
{
}
using InfrastructureSeedworks;
using ViewModels.ProjectManager;
using HttpServices.ProjectManager;

namespace Domain.Base;

public static class ServerKeyConstant
{
    public const string Key = "C620E381-9CDE-4A6F-90E3-ACD03D2128BA";
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

        if (result.IsFailed == true)
        {
            throw new Exception("Failed to send user relation");
        }
    }
}

public abstract class BaseSubSystem : DomainSeedworks.BaseSubSystem
{
}

public abstract class BaseAttachment : DomainSeedworks.BaseAttachment
{
}
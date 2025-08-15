using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IAttachmentRepository : IRepository<Attachment>
{
    Task<List<Attachment>> GetAllAttachmentsByShopId(
        string id, string subSystemName, CancellationToken cancellationToken = default);

    Task<List<Attachment>> FindAllAttachmentsByIdsAndSubSystemName(
        List<string> ids, string subSystemName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Configures products by identifying and processing specific attachment subjects
    /// associated with product profiles.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if required subsystems or attachment subjects are not found.
    /// </exception>
    Task ConfigureProductsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// find all attachments with subject by sub system local id and relation id for other tables
    /// </summary>
    /// <param name="subSystemId">subSystemLocalId</param>
    /// <param name="relationId">EntityId</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>attachments list</returns>
    Task<List<Attachment>> FindBySubSystemIdAndRelationIdAsync(
        string subSystemId,
         string relationId,
          CancellationToken cancellationToken = default);
}
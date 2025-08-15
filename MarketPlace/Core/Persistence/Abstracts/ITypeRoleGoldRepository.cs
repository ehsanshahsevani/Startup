using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ITypeRoleGoldRepository : IRepository<TypeRoleGold>
{
    Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);
}
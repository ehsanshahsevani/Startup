using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ITypeRoleMoneyRepository : IRepository<TypeRoleMoney>
{
	Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);
}
using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IOrderStatusRepository : IRepository<OrderStatus>
{
    /// <summary>
    /// نمایش DropDown های جدول
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
    Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);

    
}
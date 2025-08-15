using AutoMapper;
using Persistence;
using FluentResults;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using BaseProject.Model.ViewModel.Public;

namespace RestFullApi.Controllers;

/// <summary>
/// نوع قوانین خرید و فروش طلا
/// </summary>
public class TypeRoleGoldController : BaseControllerApi
{
    #region DI Settings & Constructor

    public TypeRoleGoldController(
        IMapper mapper,
        HttpClient httpClient, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
        : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
    {
    }

    #endregion

    #region GET : /dropdown-data

    /// <summary>
    ///     دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
    ///     - dropdown, ect
    /// </summary>
    /// <returns></returns>
    [HttpGet("dropdown-data")]
    public async Task<IActionResult> GetDropDownDataAsync()
    {
        var result = new Result<List<UiSelectModel>>();

        var value =
            await UnitOfWork.TypeRoleGoldRepository.GetSelectValues();

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /dropdown-data
}
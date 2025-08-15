using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using ViewModels.Marketplace;

namespace RestFullApi.Controllers;

/// <summary>
/// نوع قوانین برداشت و واریز پول
/// </summary>
public class TypeRoleMoneyController : BaseControllerApi
{
    #region DI Settings & Constructor

    public TypeRoleMoneyController(
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
    /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
    ///     - dropdown, ect
    /// </summary>
    /// <returns></returns>
    [HttpGet("dropdown-data")]
    public async Task<IActionResult> GetDropDownDataAsync()
    {
        var result = new Result<List<UiSelectModel>>();

        var value =
            await UnitOfWork.TypeRoleMoneyRepository.GetSelectValues();

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /dropdown-data
}
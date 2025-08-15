using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using Domain.Base;
using FluentResults;
using HttpServices.AttachmentManager;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace RestFullApi.Controllers;

public class ProvinceController : BaseControllerApi
{
    #region DI Settings & Constructor

    public ProvinceController(
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
            await UnitOfWork.ProvinceRepository.GetSelectValuesAsync();

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /dropdown-data
}
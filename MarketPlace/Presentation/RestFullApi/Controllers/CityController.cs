using AutoMapper;
using Persistence;
using FluentResults;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using BaseProject.Model.ViewModel.Public;

namespace RestFullApi.Controllers;

public class CityController : BaseControllerApi
{
    #region DI Settings & Constructor

    public CityController(
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
    [HttpGet("dropdown-data/{provinceId}")]
    public async Task<IActionResult> GetDropDownDataAsync([FromRoute] string provinceId)
    {
        var result = new Result<List<UiSelectModel>>();

        var value =
            await UnitOfWork.CityRepository.GetSelectValuesAsync(provinceId);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /dropdown-data
}
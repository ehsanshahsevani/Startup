using AutoMapper;
using FluentResults;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using ViewModels.Shared;
using ViewModels.Ticketing;
using ViewModels.Ticketing.ModelParameters;

namespace RestFullApi.Controllers;

public class StatusController : BaseControllerApi
{
    #region DI Settings & Constructor

    public StatusController(
        IMapper mapper,
        HttpClient httpClient, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
        : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
    {
    }

    #endregion

    #region GET : /

    /// <summary>
    ///     دریافت لیست همه به صورت صفحه بندی شده
    ///     مدل خروجی با دو کلید با نام های
    ///     - data, metaData
    ///     ارسال میشوند
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] StatusParameters parameters)
    {
        var result = new Result<PagedListResult<StatusResponseViewModel>>();

        var entities =
            await UnitOfWork.StatusRepository.GetAllInPageAsync(parameters);

        var values =
            Mapper.Map<PagedList<StatusResponseViewModel>>(entities);

        result.WithValue(new PagedListResult<StatusResponseViewModel>(values, entities.MetaData));

        return FluentResult(result);
    }

    #endregion GET : /
}
using Domain;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;
using ViewModels.ProjectManager;
using IUnitOfWork = Persistence.IUnitOfWork;

namespace RestFullApi.Controllers;

/// <summary>
/// تنظیمات ریلیشن و روابط کاربران
/// </summary>
public class UserRelationController : Infrastructure.BaseControllerIdentity
{
    #region DI Settings & Constructor
    
    public UserRelationController(IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base(mapper, unitOfWork, httpClient, configuration, httpContextAccessor, userManager, roleManager, signInManager, logDetailManager, logServerManager)
    {
    }

    #endregion
    
    #region POST : /add
    
    /// <summary>
    /// ثبت یک ارتباط موقف برای کاربری که در پروژه های دیگر برایشان یک ریلیشن ایجاد شده است
    /// </summary>
    /// <param name="model.serverId">شناسه سرور</param>
    /// <param name="model.subSystemId">شناسه زیر سیستم</param>
    /// <param name="model.userId">شناسه کاربر</param>
    /// <param name="model.relationId">شناسه مربوط به زیر سیستم اعلام شده در سرور اعلام شده</param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost(template: "add")]
    public async Task<IActionResult> AddAsync(UserRelationResponseViewModel model)
    {
        if (ModelState.IsValid == false)
        {
            throw new ApplicationException(Resources.Messages.RequestNotValid);
        }
        
        var result = new Result();
        
        var entity = Mapper.Map<UserRelationTemp>(model);
        
        SubSystem? subSystem =
            await UnitOfWork.SubSystemRepository.FindByNameAsync(model.SubSystemName);
        
        if (subSystem == null)
        {
            var errorMessage = 
                string.Format(Resources.Messages.NotFoundError, Resources.DataDictionary.SubSystem);

            result.WithError(errorMessage);
        }

        if (result.IsSuccess == true)
        {
            entity.SubSystemId = subSystem!.Id;
        
            await UnitOfWork.UserRelationTempRepository.AddAsync(entity);
        
            await UnitOfWork.SaveAsync();
        }
        
        return FluentResult(result);
    }
    
    #endregion
    
    #region POST : /save-changes

    /// <summary>
    /// نهایی کردن ارتباطات مربوط به کاربران
    /// جدول تمپ ارتباطات خالی میشود و به ارتباطات اصلی کاربران انتقال پیدا میکنند
    /// این روابط کمک میکند تا مدیریت کاربران بسیار راحت و دقیق شود
    /// </summary>
    /// <param name="serverId">شناسه سرور مورد نظر</param>
    /// <returns></returns>
    [HttpPost(template: "save-changes/{serverId}")]
    public async Task<IActionResult> SaveChangesAsync(string serverId)
    {
        if (string.IsNullOrWhiteSpace(serverId) == true)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError, nameof(serverId)); 
            
            throw new NullReferenceException(errorMessage);
        }
        
        var result = new Result();
        
        var temp =
            await UnitOfWork.UserRelationTempRepository.FindByServerIdAsync(serverId);
        
        var userRelations = Mapper.Map<List<UserRelation>>(temp);

        await UnitOfWork.UserRelationRepository.AddRangeAsync(userRelations);
        
        await UnitOfWork.UserRelationTempRepository.ClearTempAsync(serverId);
        
        await UnitOfWork.SaveAsync();
        
        return FluentResult(result);
    }
    
    #endregion
}
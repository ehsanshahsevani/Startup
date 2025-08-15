using AutoMapper;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using Resources;
using ViewModels.Marketplace;

namespace RestFullApi.Controllers;

public class CartItemController : BaseControllerApi
{
    #region DI Settings & Constructor

    public CartItemController(
        IMapper mapper,
        HttpClient httpClient, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
        : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
    {
    }

    #endregion

    #region GET : {id}

    /// <summary>
    ///     دریافت با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ServiceFilter<CheckCartItemIdActionFilter>]
    public IActionResult GetByIdAsync([FromRoute] string id)
    {
        var result = new Result<CartItemResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as CartItem;

        var value = Mapper.Map<CartItemResponseViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : by-id

    #region GET : {by-productId}

    /// <summary>
    ///     دریافت  لیست سبد با استفاده از شناسه محصول
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet("list/{productId}")]
    [ServiceFilter<CheckCartItemProductIdActionFilter>]
    public IActionResult GetByProductIdAsync([FromRoute] string productId)
    {
        var result = new Result<List<CartItemResponseViewModel>>();

        var entities =
            HttpContext.Items[ProjectKeyName.ObjectKey] as CartItem;

        var value = Mapper.Map<List<CartItemResponseViewModel>>(entities);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : by-productId

    #region POST : /

    /// <summary>
    ///     ایجاد
    /// </summary>
    /// <param name="model">مدل با ویژگی های لازم</param>
    /// <returns></returns>
    [HttpPost]
    [ServiceFilter<CartItemViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] CartItemRequestViewModel model)
    {
        var result = new Result<CartItemResponseViewModel>();
        var entity = Mapper.Map<CartItem>(model);

        await UnitOfWork.CartItemRepository.AddAsync(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.CreateSuccessMessage,
            DataDictionary.CartItem);

        var value =
            Mapper.Map<CartItemResponseViewModel>(entity);

        result.WithValue(value);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion POST : /

    #region PUT : /

    /// <summary>
    ///     ویرایش
    /// </summary>
    /// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
    /// <returns>مدل نهایی و جدید</returns>
    [HttpPut]
    [ServiceFilter<CartItemViewModelFilterAction>]
    public async Task<IActionResult> UpdateAsync([FromForm] CartItemRequestViewModel model)
    {
        var result = new Result<CartItemResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as CartItem;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Mapper.Map(model, entity);

        await UnitOfWork.SaveAsync();

        var value =
            Mapper.Map<CartItemResponseViewModel>(entity);

        result.WithValue(value);

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.CartItem);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion /PUT : /

    #region DELETE : /

    /// <summary>
    ///     حذف با شناسه آن
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
    [HttpDelete("{id}")]
    [ServiceFilter<CheckCartItemIdActionFilter>]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        var result = new Result<string>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as CartItem;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (result.IsSuccess)
        {
            await UnitOfWork.CartItemRepository.RemoveAsync(entity);
            await UnitOfWork.SaveAsync();

            var successMessage =
                string.Format(Messages.DeleteMessageSuccess, DataDictionary.CartItem);

            result.WithSuccess(successMessage);

            result.WithValue(entity.Id);
        }

        return FluentResult(result);
    }

    #endregion /DELETE : /
}
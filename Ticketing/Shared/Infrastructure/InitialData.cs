using Domain;
using Microsoft.Extensions.Configuration;
using Persistence;
using Resources;

namespace Infrastructure;

public class InitialData : object
{
// **************************************************
    public async Task CreateStatusAsync()
    {
        var reviewPending = await UnitOfWork
            .StatusRepository.FindByNameAsync(nameof(DataDictionary.ReviewPending));

        if (reviewPending is null)
        {
            reviewPending = new Status(nameof(DataDictionary.ReviewPending))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 1,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.StatusRepository.AddAsync(reviewPending);
        }

        var underReview = await UnitOfWork
            .StatusRepository.FindByNameAsync(nameof(DataDictionary.UnderReview));

        if (underReview is null)
        {
            underReview = new Status(nameof(DataDictionary.UnderReview))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 2,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.StatusRepository.AddAsync(underReview);
        }

        var answered = await UnitOfWork
            .StatusRepository.FindByNameAsync(nameof(DataDictionary.Answered));

        if (answered is null)
        {
            answered = new Status(nameof(DataDictionary.Answered))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 3,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.StatusRepository.AddAsync(answered);
        }

        var closed = await UnitOfWork
            .StatusRepository.FindByNameAsync(nameof(DataDictionary.Closed));

        if (closed is null)
        {
            closed = new Status(nameof(DataDictionary.Closed))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 4,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.StatusRepository.AddAsync(closed);
        }

        await UnitOfWork.SaveAsync();
    }


    public async Task CreateTicketSubjectAsync()
    {
        var technicalSupport = await UnitOfWork
            .TicketSubjectRepository.FindByNameAsync(nameof(DataDictionary.TechnicalSupport));

        if (technicalSupport is null)
        {
            technicalSupport = new TicketSubject(nameof(DataDictionary.TechnicalSupport))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 4,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.TicketSubjectRepository.AddAsync(technicalSupport);
        }

        var financialSupport = await UnitOfWork
            .TicketSubjectRepository.FindByNameAsync(nameof(DataDictionary.FinancialSupport));

        if (financialSupport is null)
        {
            financialSupport = new TicketSubject(nameof(DataDictionary.FinancialSupport))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 2,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.TicketSubjectRepository.AddAsync(financialSupport);
        }

        var customerAffairs = await UnitOfWork
            .TicketSubjectRepository.FindByNameAsync(nameof(DataDictionary.CustomerAffairs));

        if (customerAffairs is null)
        {
            customerAffairs = new TicketSubject(nameof(DataDictionary.CustomerAffairs))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 3,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.TicketSubjectRepository.AddAsync(customerAffairs);
        }

        var qualityControl = await UnitOfWork
            .TicketSubjectRepository.FindByNameAsync(nameof(DataDictionary.QualityControl));

        if (qualityControl is null)
        {
            qualityControl = new TicketSubject(nameof(DataDictionary.QualityControl))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 4,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.TicketSubjectRepository.AddAsync(qualityControl);
        }

        var errorReport = await UnitOfWork
            .TicketSubjectRepository.FindByNameAsync(nameof(DataDictionary.ErrorReport));

        if (errorReport is null)
        {
            errorReport = new TicketSubject(nameof(DataDictionary.ErrorReport))
            {
                IsActive = true,
                IsDeleted = false,
                Ordering = 5,

                Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد"
            };

            await UnitOfWork.TicketSubjectRepository.AddAsync(errorReport);
        }

        await UnitOfWork.SaveAsync();
    }

    #region Settings

    public InitialData(
        IConfiguration configuration,
        IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected IConfiguration Configuration { get; }
    protected IUnitOfWork UnitOfWork { get; }

    #endregion
}
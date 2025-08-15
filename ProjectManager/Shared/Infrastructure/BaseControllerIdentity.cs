using Domain;
using AutoMapper;
using ControllerSeedworks;
using Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PersistenceSeedworks.LogManager;

namespace RestFullApi.Infrastructure;

public class BaseControllerIdentity : BaseApiIdentityController<User, Role>
{
    public IUnitOfWork UnitOfWork { get; }

    public BaseControllerIdentity(IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
        RoleManager<Role> roleManager, SignInManager<User> signInManager, ILogDetailManager logDetailManager, 
        ILogServerManager logServerManager) : base(mapper, unitOfWork, httpClient, configuration, httpContextAccessor, userManager, roleManager, signInManager, logDetailManager, logServerManager)
    {
        UnitOfWork = unitOfWork;
    }
}
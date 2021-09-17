using CartoPrime.Application.Application;
using CartoPrime.Application.Interfaces;
using CartoPrime.Data.CA.Http;
using CartoPrime.Data.CA.Http.Interfaces;
using CartoPrime.Data.Repositories;
using CartoPrime.Domain.Interfaces.Repositories;
using CartoPrime.Domain.Interfaces.Services;
using CartoPrime.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using TM.Utils.Bases;
using TM.Utils.Bases.Interface;

namespace CartoPrime.Application
{
    public class DependenciesInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {
            #region Application
            svcCollection.AddScoped<IUserApplication, UserApplication>();
            svcCollection.AddScoped<IPushApplication, PushApplication>();
            svcCollection.AddScoped<IBonsDeBicoApplication, BonsDeBicoApplication>();
            #endregion

            #region Domain
            svcCollection.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            svcCollection.AddScoped<IPushService, PushService>();
            svcCollection.AddScoped<IUserService, UserService>();
            svcCollection.AddScoped<ITeamService, TeamService>();
            svcCollection.AddScoped<IAthleteService, AthleteService>();
            svcCollection.AddScoped<IClubService, ClubService>();
            svcCollection.AddScoped<IPositionsService, PositionsService>();
            svcCollection.AddScoped<IMarketService, MarketService>();
            #endregion

            #region Repository
            svcCollection.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            svcCollection.AddScoped<IPushRepository, PushRepository>();
            svcCollection.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}

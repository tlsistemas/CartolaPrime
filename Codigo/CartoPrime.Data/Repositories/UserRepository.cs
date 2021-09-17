using CartoPrime.Data.Context;
using CartoPrime.Domain.Entities;
using CartoPrime.Domain.Interfaces.Repositories;
using TM.Utils.Bases;

namespace CartoPrime.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DSContext ctx) : base(ctx)
        {
        }
    }
}

using CartoPrime.Domain.Entities;
using CartoPrime.Domain.Interfaces.Repositories;
using CartoPrime.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}

using CartoPrime.Domain.Entities;
using CartoPrime.Domain.Interfaces.Repositories;
using CartoPrime.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Domain.Services
{
    public class PushService : ServiceBase<PushNotification>, IPushService
    {
        public readonly IPushRepository _repository;

        public PushService(IPushRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
using CartoPrime.Data.Context;
using CartoPrime.Domain.Entities;
using CartoPrime.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Data.Repositories
{
    public class PushRepository : RepositoryBase<PushNotification>, IPushRepository
    {
        public PushRepository(DSContext ctx) : base(ctx)
        {
        }
    }
}
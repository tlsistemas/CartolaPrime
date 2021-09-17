using CartoPrime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases.Interface;

namespace CartoPrime.Domain.Interfaces.Repositories
{
    public interface IPushRepository : IRepositoryBase<PushNotification>
    {
    }
}

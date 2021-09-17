using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IPositionsService
    {
        Task<List<string>> GetAll();
        Task<string> Get(int id);
        Task<int> Get(string position);
    }
}

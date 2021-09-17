using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface INewsService
    {
        Task<List<News>> GetNewsAsync();
    }
}

using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IClassificationBRAService
    {
        Task<ClassificationBRA> GetClassication(int year);
    }
}

using System;
using System.Threading.Tasks;

namespace CartoPrime.Interfaces
{
    public interface ICustomDisplayAlert
    {
        Task<bool> Show(string title, string message, string accept);

        Task<bool> Show(string title, string message, string accept, string cancel);
    }
}
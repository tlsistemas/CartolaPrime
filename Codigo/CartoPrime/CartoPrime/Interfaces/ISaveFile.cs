using System.Threading.Tasks;

namespace CartoPrime.Interfaces
{
    public interface ISaveFile
    {
        Task<string> SaveFiles(string filename, byte[] bytes);

    }
}

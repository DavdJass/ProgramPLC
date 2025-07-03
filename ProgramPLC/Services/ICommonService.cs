using ProgramPLC.DTOs;
using ProgramPLC.Models;

namespace ProgramPLC.Services
{
    public interface ICommonService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(string name, string value, int engine);
        //IEnumerable<T> Search(Func<T, bool> filter);
        Task<T> GetHzSetup(string name);
        Task<T> GetOneDevice(string name);

        Task Update(string hertzName);
        Task UpdateFalse(string hertzName);

        Task<List<InitMonitorReadDTO>> GetActived();
    }
}

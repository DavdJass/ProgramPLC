using System.Collections;
using ProgramPLC.DTOs;

namespace ProgramPLC.Repository
{
    public interface IRepository<TEntity>
    {
        Task Add(string name, string value, int engine);
        Task<IEnumerable> GetAll(TEntity entity);
       // TI Search(Func<TEntity, bool> filter, string data);
        Task Save();

        Task<TEntity> GetHzSetup(string name);
        Task<TEntity> GetOneDevice(string name);

        Task UpdateTrue(string hertzName);
        Task UpdateFalse(string hertzName);
        Task<List<InitMonitorReadDTO>> GetActived();
    }
}

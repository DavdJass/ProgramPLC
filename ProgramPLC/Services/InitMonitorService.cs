using ProgramPLC.DTOs;
using ProgramPLC.Models;
using ProgramPLC.Repository;

namespace ProgramPLC.Services
{
    public class InitMonitorService : ICommonService<InitMonitor>
    {
        private IRepository<InitMonitor> _repository;
        public InitMonitorService(IRepository<InitMonitor> repository)
        {
            _repository = repository;
        }

        public Task Add(string name, string value, int engine)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InitMonitorReadDTO>> GetActived() =>
               await _repository.GetActived();

        public Task<IEnumerable<InitMonitor>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<InitMonitor> GetHzSetup(string name)
         => await _repository.GetHzSetup(name);

        public async Task<InitMonitor> GetOneDevice(string name)
        {
            var data = await _repository.GetOneDevice(name);
            return data;
        }

        public async Task Update(string name)
        {
            await _repository.UpdateTrue(name);
            await _repository.Save();

        }

        public async Task UpdateFalse(string hertzName)
        {
            await _repository.UpdateFalse(hertzName);
            await _repository.Save();
        }
    }
}

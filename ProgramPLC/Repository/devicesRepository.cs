using ProgramPLC.Database;
using ProgramPLC.DTOs;
using ProgramPLC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ProgramPLC.Repository
{
    public class devicesRepository : IRepository<Device>
    {
        private AnalysisPlcContext _dbContext;

        public devicesRepository(AnalysisPlcContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(string name, string value, int engine)
        {
            Device device = new Device()
            {
                DeviceName = name,
                DeviceValue = value,
                EngineId = engine
            };
            await _dbContext.Devices.AddAsync(device);
        }
        public Task<IEnumerable> GetAll(Device entity)
        {
            throw new NotImplementedException();
        }

        public Task<Device> GetHzSetup(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Save() =>
            await _dbContext.SaveChangesAsync();

        public DeviceReadDTO Search(Func<Device, bool> filter, string d)
        {
            var data = _dbContext.Devices.Where(filter).FirstOrDefault();
            var device = new DeviceReadDTO
            {
                DeviceId = data.DeviceId, 
                DeviceName = data.DeviceName,
                DeviceValue = data.DeviceValue,
                EngineId = data.EngineId,
            };
            return device;
               
        }
        public async Task<Device> GetOneDevice(string name)
        {
            var device = await _dbContext.Devices.Where(d => d.DeviceName == name).FirstOrDefaultAsync();
            return device;
        }

        public Task UpdateTrue(string hertzName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFalse(string hertzName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InitMonitorReadDTO>> GetActived()
        {
            throw new NotImplementedException();
        }
    }
}

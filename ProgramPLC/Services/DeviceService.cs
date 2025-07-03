using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgramPLC.DTOs;
using ProgramPLC.Models;
using ProgramPLC.Repository;

namespace ProgramPLC.Services
{
    public class DeviceService : ICommonService<Device>
    {
        private IRepository<Device> _repository;
        private IMapper _mapper;
        public DeviceService(IRepository<Device> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }
        public async Task Add(string name, string value, int engine)
        {
            //var device = _mapper.Map<Device>(entityInsertDTO);
            await _repository.Add(name, value, engine);
            await _repository.Save();
            //return device;
        }

        public Task<List<InitMonitorReadDTO>> GetActived()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Device> GetHzSetup(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> GetOneDevice(string name) =>
            await _repository.GetOneDevice(name);

        public Task Update(string entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFalse(string hertzName)
        {
            throw new NotImplementedException();
        }
    }
}

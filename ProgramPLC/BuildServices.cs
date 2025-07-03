using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgramPLC.Database;
using ProgramPLC.Mappers;
using ProgramPLC.Models;
using ProgramPLC.Repository;
using ProgramPLC.Services;

namespace ProgramPLC
{
    public class BuildServices
    {
        private  AnalysisPlcContext _analysisPlcContext;
        //private IMapper _mapper;
        //public BuildServices(AnalysisPlcContext analysisPlcContext)
        //{
        //    _analysisPlcContext = analysisPlcContext;
        //}
        
        public ICommonService<InitMonitor> ServiceInitMonitor()
        {
            BuildServices bdservice = new BuildServices();
            ICommonService<InitMonitor> service = new InitMonitorService(bdservice.BuildRepoInit());
            return service;
        }
        public ICommonService<Device> ServiceDevice()
        {
            BuildServices bdservice = new BuildServices();
            ICommonService<Device> service = new DeviceService(bdservice.BuildRepoDevice(), BuildServices.buildMapper());
            return service;
        }

        private static IMapper buildMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Crea una instancia de IMapper
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        private IRepository<InitMonitor> BuildRepoInit()
        {
            _analysisPlcContext = new AnalysisPlcContext();
            IRepository<InitMonitor> repository = new initMonitorRepository(_analysisPlcContext);
            return repository;
        }
        private IRepository<Device> BuildRepoDevice()
        {
            _analysisPlcContext = new AnalysisPlcContext();
            IRepository<Device> repository2 = new devicesRepository(_analysisPlcContext);
            return repository2;
        }
    }
}

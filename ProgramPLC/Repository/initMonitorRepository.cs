using System.Collections;
using Microsoft.EntityFrameworkCore;
using ProgramPLC.Database;
using ProgramPLC.DTOs;
using ProgramPLC.Models;

namespace ProgramPLC.Repository
{
    public class initMonitorRepository : IRepository<InitMonitor>
    {
        private AnalysisPlcContext _context;
        public initMonitorRepository(AnalysisPlcContext context)
        {
            _context = context;
        }
        public async Task Add(InitMonitor entity) =>
            await _context.InitMonitors.AddAsync(entity);

        public async Task<IEnumerable> GetAll(InitMonitor entity) =>
            await _context.InitMonitors.ToListAsync();

        public async Task Save() =>
            await _context.SaveChangesAsync();

        public IEnumerable<InitMonitor> Search(Func<InitMonitor, bool> filter) =>
            _context.InitMonitors.Where(filter).ToList();

        public async Task<InitMonitor> GetHzSetup(string name)
        {
            var engines = await _context.InitMonitors.Where(e => e.Hertz == name).FirstOrDefaultAsync();

            if (engines != null)
            {
                InitMonitor init = new InitMonitor()
                {
                    HertzSetup = engines.HertzSetup
                };
                return init;
            }
            return null;
            
        }

        public async Task<InitMonitor> GetOneDevice(string name)
        {
            var device = await _context.InitMonitors.Where(e => e.StatusM == true &&
            (e.Hertz == name || e.Amper == name || e.Volts == name)).FirstOrDefaultAsync();

            if (device == null)
                return null;

            //Console.WriteLine(device.HertzSetup);
            return device;
        }
        public async Task UpdateTrue(string hertzName)
        {
            var engines = await _context.InitMonitors.Where(e => e.Hertz == hertzName).FirstOrDefaultAsync();

            if (engines == null)
            {
                return;
            }

            engines.StatusM = true;
            _context.InitMonitors.Attach(engines);
            _context.InitMonitors.Entry(engines).State = EntityState.Modified;
        }

        public async Task UpdateFalse(string hertzName)
        {
            var engines = await _context.InitMonitors.Where(e => e.Hertz == hertzName).FirstOrDefaultAsync();
            if(engines == null)
            {
                return;
            }
            engines.StatusM = false;
            _context.InitMonitors.Attach(engines);
            _context.InitMonitors.Entry(engines).State = EntityState.Modified;
        }

        public Task Add(string name, string value, string engine)
        {
            throw new NotImplementedException();
        }

        public Task Add(string name, string value, int engine)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InitMonitorReadDTO>> GetActived()
        {
            var response = await _context.InitMonitorDTO
                .FromSqlRaw("EXEC sp_initMonitoringEngine")
    .ToListAsync();
            return response;

        }
    }
}

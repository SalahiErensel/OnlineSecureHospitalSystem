using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services
{
    public class LogService
    {
        private readonly AppDbContext _appDbContext;
        public LogService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddLogAsync(string action, byte[] inputParameters, byte[] outputParameters)
        {
            var log = new Logs
            {
                Action = action,
                InputParameters = inputParameters,
                OutputParameters = outputParameters,
                Timestamp = DateTime.UtcNow
            };
            await _appDbContext.Logs.AddAsync(log);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<List<Logs>> GetLogsAsync()
        {
            return await _appDbContext.Logs.ToListAsync();
        }
    }
}
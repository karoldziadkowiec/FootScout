using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class SalaryRangeRepository : ISalaryRangeRepository
    {
        private readonly AppDbContext _dbContext;

        public SalaryRangeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateSalaryRange(SalaryRange salaryRange)
        {
            await _dbContext.SalaryRanges.AddAsync(salaryRange);
            await _dbContext.SaveChangesAsync();
        }
    }
}
using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Repositories.Interfaces
{
    public interface ISalaryRangeRepository
    {
        Task CreateSalaryRange(SalaryRange salaryRange);
    }
}
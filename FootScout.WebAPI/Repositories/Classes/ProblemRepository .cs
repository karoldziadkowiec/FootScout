using FootScout.WebAPI.DbManager;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.Repositories.Classes
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly AppDbContext _dbContext;

        public ProblemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Problem> GetProblem(int problemId)
        {
            return await _dbContext.Problems
                .Include(p => p.Requester)
                .FirstOrDefaultAsync(p => p.Id == problemId);
        }

        public async Task<IEnumerable<Problem>> GetAllProblems()
        {
            return await _dbContext.Problems
                .Include(p => p.Requester)
                .OrderByDescending(p => p.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Problem>> GetSolvedProblems()
        {
            return await _dbContext.Problems
                .Include(p => p.Requester)
                .Where(p => p.IsSolved == true)
                .OrderByDescending(p => p.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Problem>> GetUnsolvedProblems()
        {
            return await _dbContext.Problems
                .Include(p => p.Requester)
                .Where(pa => pa.IsSolved == false)
                .OrderByDescending(p => p.CreationDate)
                .ToListAsync();
        }

        public async Task<int> GetUnsolvedProblemCount()
        {
            return await _dbContext.Problems.Where(p => p.IsSolved == false).CountAsync();
        }

        public async Task CreateProblem(Problem problem)
        {
            problem.CreationDate = DateTime.Now;
            problem.IsSolved = false;

            await _dbContext.Problems.AddAsync(problem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CheckProblemSolved(Problem problem)
        {
            problem.IsSolved = true;

            _dbContext.Problems.Update(problem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
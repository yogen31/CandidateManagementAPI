using Microsoft.EntityFrameworkCore;
using WebApplication1.Database.Context;
using WebApplication1.Model;
using WebApplication1.Repository.Interface;

namespace WebApplication1.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateManagementAPIDbContext _context;
        public CandidateRepository(CandidateManagementAPIDbContext context)
        {
            _context = context;
        }
        public async Task<Candidate> AddCandidate(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;

        }

        public async Task<Candidate?> GetCandidateByEmail(string candidateEmail)
        {
            var candidate = await _context.Candidates.Where(x => x.Email == candidateEmail).FirstOrDefaultAsync();
            return candidate;
        }

        public async Task<Candidate?> GetCandidateById(int candidateId)
        {
            var candidate = await _context.Candidates.FindAsync(candidateId);
            return candidate;
        }

        public async Task<Candidate> UpdateCandidate(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }
    }
}

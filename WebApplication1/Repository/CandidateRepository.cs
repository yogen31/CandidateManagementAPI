using Microsoft.EntityFrameworkCore;
using CandidateManagementAPI.Database.Context;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.Repository.Interface;

namespace CandidateManagementAPI.Repository
{
    public class CandidateRepository(CandidateManagementAPIDbContext context) : ICandidateRepository
    {
        public async Task<Candidate> AddCandidate(Candidate candidate)
        {
            await context.Candidates.AddAsync(candidate);
            await context.SaveChangesAsync();
            return candidate;

        }

        public async Task<Candidate?> GetCandidateByEmail(string candidateEmail)
        {
            var candidate = await context.Candidates.Where(x => x.Email == candidateEmail).FirstOrDefaultAsync();
            return candidate;
        }

        public async Task<Candidate?> GetCandidateById(int candidateId)
        {
            var candidate = await context.Candidates.FindAsync(candidateId);
            return candidate;
        }

        public async Task<Candidate> UpdateCandidate(Candidate candidate)
        {
            context.Candidates.Update(candidate);
            await context.SaveChangesAsync();
            return candidate;
        }
    }
}

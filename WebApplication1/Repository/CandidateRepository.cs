using Microsoft.EntityFrameworkCore;
using CandidateManagementAPI.Database.Context;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateManagementAPI.Repository
{
    public class CandidateRepository(CandidateManagementAPIDbContext context, IMemoryCache cache) : ICandidateRepository
    {
        #region Add Candidate
        public async Task<Candidate> AddCandidate(Candidate candidate)
        {
            await context.Candidates.AddAsync(candidate);
            await context.SaveChangesAsync();
            //Update cache with the new candidate
            cache.Set($"Candidate_{candidate.Email}", candidate, TimeSpan.FromMinutes(10));
            return candidate;

        }
        #endregion

        #region Get Candidate By Email
        public async Task<Candidate?> GetCandidateByEmail(string candidateEmail)
        {
            if (cache.TryGetValue($"Candidate_{candidateEmail}", out Candidate? cachedCandidate))
            {
                return cachedCandidate;
            }
            var candidate = await context.Candidates.Where(x => x.Email == candidateEmail).FirstOrDefaultAsync();
            // If data not found in cache then add this data to cache
            if (candidate != null)
            {
                cache.Set($"Candidate_{candidateEmail}", candidate, TimeSpan.FromMinutes(10));
            }
            return candidate;
        }
        #endregion

        #region Update Candidate
        public async Task<Candidate> UpdateCandidate(Candidate candidate)
        {
            context.Candidates.Update(candidate);
            await context.SaveChangesAsync();
            // Update cache with updated candidate data
            cache.Set($"Candidate_{candidate.Email}", candidate, TimeSpan.FromMinutes(10));
            return candidate;
        }
        #endregion
    }
}

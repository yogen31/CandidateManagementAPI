using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;
using WebApplication1.Repository.Interface;
using WebApplication1.Services.Interface;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<CandidateRequest> AddOrUpdateCandidate(CandidateRequest candidate)
        {
            var existingCandidate = await _candidateRepository.GetCandidateByEmail(candidate.Email);

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInURL = candidate.LinkedInURL;
                existingCandidate.GitHubURL = candidate.GitHubURL;
                existingCandidate.FreeComment = candidate.FreeComment;

                await _candidateRepository.UpdateCandidate(existingCandidate);
            }
            else
            {
                var newCandidate = new Candidate
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    Email = candidate.Email,
                    PhoneNumber = candidate.PhoneNumber,
                    CallTimeInterval = candidate.CallTimeInterval,
                    LinkedInURL = candidate.LinkedInURL,
                    GitHubURL = candidate.GitHubURL,
                    FreeComment = candidate.FreeComment
                };
                await _candidateRepository.AddCandidate(newCandidate);
            }

            return candidate;
        }
    }
}

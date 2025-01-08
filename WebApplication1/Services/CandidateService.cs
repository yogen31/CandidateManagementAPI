using Microsoft.EntityFrameworkCore;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.Repository.Interface;
using CandidateManagementAPI.Services.Interface;
using CandidateManagementAPI.ViewModels;

namespace CandidateManagementAPI.Services
{
    public class CandidateService(ICandidateRepository candidateRepository) : ICandidateService
    {
        public async Task<CandidateRequest> AddOrUpdateCandidate(CandidateRequest candidate)
        {
            var existingCandidate = await candidateRepository.GetCandidateByEmail(candidate.Email);

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInURL = candidate.LinkedInURL;
                existingCandidate.GitHubURL = candidate.GitHubURL;
                existingCandidate.FreeComment = candidate.FreeComment;

                await candidateRepository.UpdateCandidate(existingCandidate);
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
                await candidateRepository.AddCandidate(newCandidate);
            }

            return candidate;
        }
    }
}

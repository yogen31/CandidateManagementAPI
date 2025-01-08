using CandidateManagementAPI.Model;

namespace CandidateManagementAPI.Repository.Interface
{
    public interface ICandidateRepository
    {
        Task<Candidate> AddCandidate(Candidate candidate);
        Task<Candidate> UpdateCandidate(Candidate candidate);
        Task<Candidate?> GetCandidateByEmail(string email);
    }
}

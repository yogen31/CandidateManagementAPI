using WebApplication1.Model;

namespace WebApplication1.Repository.Interface
{
    public interface ICandidateRepository
    {
        Task<Candidate> AddCandidate(Candidate candidate);
        Task<Candidate> UpdateCandidate(Candidate candidate);
        Task<Candidate?> GetCandidateById(int candidateId);
        Task<Candidate?> GetCandidateByEmail(string email);
    }
}

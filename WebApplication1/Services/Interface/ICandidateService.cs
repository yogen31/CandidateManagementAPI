using WebApplication1.Model;
using WebApplication1.ViewModels;

namespace WebApplication1.Services.Interface
{
    public interface ICandidateService
    {
        Task<CandidateRequest> AddOrUpdateCandidate(CandidateRequest candidate); 
    }
}

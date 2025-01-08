using CandidateManagementAPI.Model;
using CandidateManagementAPI.ViewModels;

namespace CandidateManagementAPI.Services.Interface
{
    public interface ICandidateService
    {
        Task<CandidateRequest> AddOrUpdateCandidate(CandidateRequest candidate); 
    }
}

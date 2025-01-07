using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CandidateViewModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public string? CallTimeInterval { get; set; }
        public string? LinkedInURL { get; set; }
        public string? GitHubURL { get; set; }
        public required string FreeComment { get; set; }
    }
}

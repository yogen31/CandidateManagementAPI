﻿using System.ComponentModel.DataAnnotations;

namespace CandidateManagementAPI.Model
{
    public class Candidate
    {
        public int Id { get; set; }
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

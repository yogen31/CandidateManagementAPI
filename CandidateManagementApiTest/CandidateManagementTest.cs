using CandidateManagementAPI.Controllers;
using CandidateManagementAPI.Services.Interface;
using CandidateManagementAPI.utils;
using CandidateManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace CandidateManagementApiTest
{
    public class CandidateManagementTest
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly IMemoryCache _cache;

        public CandidateManagementTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public async void AddOrUpdateCandidate_Candidate()
        {
            // Arrange
            var candidateList = GetCandidatesData();
            _candidateServiceMock.Setup(x => x.AddOrUpdateCandidate(candidateList[2])).ReturnsAsync(candidateList[2]);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            // Act
            var candidateResult = await candidateController.AddOrUpdateCandidate(candidateList[2]);

            // Assert
            Assert.NotNull(candidateResult);
            var okResult = candidateResult as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ResponseModel<CandidateRequest>;
            Assert.NotNull(response);
            Assert.NotNull(response.Entity);
            Assert.Equal(candidateList[2].Email, response.Entity.Email);
            Assert.True(candidateList[2].Email == response.Entity.Email);
        }

        private List<CandidateRequest> GetCandidatesData()
        {
            const string cacheKey = "candidateList";
            if (!_cache.TryGetValue(cacheKey, out List<CandidateRequest>? candidatesData))
            {
                candidatesData = new List<CandidateRequest>
                {
                    new CandidateRequest
                    {
                        FirstName = "Anthony",
                        LastName = "Rock",
                        Email = "anthony.rock@gmail.com",
                        PhoneNumber = "1234567890",
                        CallTimeInterval = "10-5",
                        LinkedInURL = "https://linkedin.com/anthony",
                        GitHubURL = "https://github.com/anthony",
                        FreeComment = "Anthony is open to work!"
                    },
                    new CandidateRequest
                    {
                        FirstName = "Yogendra",
                        LastName = "Bhattarai",
                        Email = "yogendra.bhattarai@gmail.com",
                        PhoneNumber = "987989878",
                        CallTimeInterval = "9-6",
                        LinkedInURL = "https://linkedin.com/yogendra",
                        GitHubURL = "https://github.com/yogendra",
                        FreeComment = "Yogendra is open to work Remote!"
                    },
                    new CandidateRequest
                    {
                        FirstName = "Biraj",
                        LastName = "Pradhan",
                        Email = "biraj.pradhan@gmail.com",
                        PhoneNumber = "78787878",
                        CallTimeInterval = "8-5",
                        LinkedInURL = "https://linkedin.com/biraj",
                        GitHubURL = "https://github.com/biraj",
                        FreeComment = "Biraj is open to work On site!"
                    },
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, candidatesData, cacheEntryOptions);
            }

            return candidatesData!;
        }
    }
}

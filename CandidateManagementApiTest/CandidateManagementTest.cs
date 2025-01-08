using CandidateManagementAPI.Controllers;
using CandidateManagementAPI.Services.Interface;
using CandidateManagementAPI.utils;
using CandidateManagementAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CandidateManagementApiTest
{
    public class CandidateManagementTest
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;

        public CandidateManagementTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
        }

        #region Add or Update Candiate Test
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
        #endregion

        #region Validate Update
        [Fact]
        public async void AddOrUpdateCandidate_ValidUpdate_ReturnsOk()
        {
            // Arrange
            var candidate = GetCandidatesData()[0];
            _candidateServiceMock.Setup(x => x.AddOrUpdateCandidate(candidate)).ReturnsAsync(candidate);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            // Act
            var candidateResult = await candidateController.AddOrUpdateCandidate(candidate);

            // Assert
            Assert.NotNull(candidateResult);
            var okResult = candidateResult as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ResponseModel<CandidateRequest>;
            Assert.NotNull(response);
            Assert.NotNull(response.Entity);
            Assert.Equal(candidate.Email, response.Entity.Email);
            Assert.Equal("Candidate information saved successfully.", response.ReturnMessage);
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }
        #endregion

        #region Validate Add
        [Fact]
        public async void AddOrUpdateCandidate_ValidAdd_ReturnsOk()
        {
            // Arrange
            var newCandidate = new CandidateRequest
            {
                FirstName = "New",
                LastName = "Candidate",
                Email = "new.candidate@gmail.com",
                PhoneNumber = "123456789",
                CallTimeInterval = "10-6",
                LinkedInURL = "https://linkedin.com/new",
                GitHubURL = "https://github.com/new",
                FreeComment = "New candidate is available."
            };

            _candidateServiceMock.Setup(x => x.AddOrUpdateCandidate(newCandidate)).ReturnsAsync(newCandidate);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            // Act
            var candidateResult = await candidateController.AddOrUpdateCandidate(newCandidate);

            // Assert
            Assert.NotNull(candidateResult);
            var okResult = candidateResult as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ResponseModel<CandidateRequest>;
            Assert.NotNull(response);
            Assert.NotNull(response.Entity);
            Assert.Equal(newCandidate.Email, response.Entity.Email);
            Assert.Equal("Candidate information saved successfully.", response.ReturnMessage);
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }
        #endregion

        #region Validate Null Request
        [Fact]
        public async void AddOrUpdateCandidate_NullRequest_ReturnsBadRequest()
        {
            // Arrange
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            // Act
            var candidateResult = await candidateController.AddOrUpdateCandidate(null);

            // Assert
            Assert.NotNull(candidateResult);
            var badRequestResult = candidateResult as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var response = badRequestResult.Value as ResponseModel<CandidateRequest>;
            Assert.NotNull(response);
            Assert.Equal("Candidate data is required.", response.ReturnMessage);
            Assert.Equal(StatusCodes.Status400BadRequest, response.Status);
        }
        #endregion

        #region Validate Not Found when Service returns Null
        [Fact]
        public async void AddOrUpdateCandidate_ServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            var candidate = GetCandidatesData()[0];
            _candidateServiceMock.Setup(x => x.AddOrUpdateCandidate(candidate)).ReturnsAsync((CandidateRequest?)null);
            var candidateController = new CandidateController(_candidateServiceMock.Object);

            // Act
            var candidateResult = await candidateController.AddOrUpdateCandidate(candidate);

            // Assert
            Assert.NotNull(candidateResult);
            var notFoundResult = candidateResult as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            var response = notFoundResult.Value as ResponseModel<CandidateRequest>;
            Assert.NotNull(response);
            Assert.Equal("Cannot save candidate information", response.ReturnMessage);
            Assert.Equal(StatusCodes.Status404NotFound, response.Status);
        }
        #endregion

        #region Get Candidates List
        private List<CandidateRequest> GetCandidatesData()
        {
            var candidatesData = new List<CandidateRequest>
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

            return candidatesData!;
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using CandidateManagementAPI.Services.Interface;
using CandidateManagementAPI.utils;
using CandidateManagementAPI.ViewModels;

namespace CandidateManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController(ICandidateService candidateService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateRequest request)
        {
            ResponseModel<CandidateRequest> response = new();

            try
            {
                if (request == null)
                {
                    response.ReturnMessage = "Candidate data is required.";
                    response.Status = StatusCodes.Status400BadRequest;
                    return BadRequest(response);
                }

                var result = await candidateService.AddOrUpdateCandidate(request);
                if (result != null)
                {
                    response.Entity = result;
                    response.ReturnMessage = "Candidate information saved successfully.";
                    response.Status = StatusCodes.Status200OK;
                    return Ok(response);
                }
                response.ReturnMessage = "Cannot save candidate information";
                response.Status = StatusCodes.Status404NotFound;
                return NotFound(response);
            }
            catch(Exception ex)
            {
                response.ReturnMessage = ex.Message;
                response.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(response);
            }
        }
    }
}

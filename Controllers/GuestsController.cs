using Guest.Interfaces;
using Guest.Models;
using Guest.Util;
using Microsoft.AspNetCore.Mvc;

namespace Guest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : Controller
    {
        private readonly IGuestsService _serviceProvider;
        private readonly ILogger<GuestsController> _logger;

        public GuestsController(IGuestsService serviceProvider, ILogger<GuestsController> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Get Request Executed Suceesfully");

                return Ok(await _serviceProvider.GetAllGuestsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get All Guests: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Get All Guests");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                _logger.LogError($"Get Request Executed Suceesfully for {id}");

                string error;
                var validation = _serviceProvider.ValidateGuestById(id, out error);
                if (validation.HasFlag(GuestsValidationResult.Ok))
                    return Ok(await _serviceProvider.GetGuestAsync(id));
                else
                    return NotFound(Helper.NotFoundObject(error));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Guest {id}: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Get Guest {id}");
            }

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Post([FromBody] GuestsItem newGuest)
        {
            try
            {
                _logger.LogError($"Add Request Executed Suceesfully");

                List<string> errors;
                var validation = _serviceProvider.ValidateNewGuest(newGuest, out errors);
                if (validation.HasFlag(GuestsValidationResult.Ok))
                    return Ok(await _serviceProvider.CreateGuestAsync(newGuest));
                else
                    return BadRequest(Helper.BadRequestObject(errors));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Guest: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Add Guest");
            }
        }

        [HttpPost("AddPhoneNumbers")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Post([FromBody] GuestsPhoneItem newGuest)
        {
            try
            {
                _logger.LogError($"Add Phone Request Executed Suceesfully");

                List<string> errors;
                var validation = _serviceProvider.ValidateGuestPhone(newGuest, out errors);
                if (validation.HasFlag(GuestsValidationResult.Ok))
                    return Ok(await _serviceProvider.AddGuestPhoneAsync(newGuest));
                else if (validation.HasFlag(GuestsValidationResult.GuestNotExists))
                    return BadRequest(Helper.BadRequestObject(errors));
                else
                    return Conflict(Helper.AlreadyFoundObject(errors));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Guest Phone: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Add Guest Phone");
            }
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Edit([FromBody] GuestsItem Guest)
        {
            try
            {
                _logger.LogError($"Update Request Executed Suceesfully");

                List<string> errors;
                var validation = _serviceProvider.ValidateExistingGuest(Guest, out errors);
                if (validation.HasFlag(GuestsValidationResult.Ok))
                    return Ok(await _serviceProvider.UpdateGuestAsync(Guest));
                else
                    return BadRequest(Helper.BadRequestObject(errors));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Guest: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Guest");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogError($"Delete Request Executed Suceesfully for {id}");

                string error;
                var validation = _serviceProvider.ValidateGuestById(id, out error);
                if (validation.HasFlag(GuestsValidationResult.Ok))
                    return Ok(await _serviceProvider.DeleteGuestAsync(id));
                else
                    return NotFound(Helper.NotFoundObject(error));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Delete Guest {id}: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete Guest {id}");
            }

        }
    }
}

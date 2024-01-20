using Guest.CQRS.Commands;
using Guest.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Guest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsCQRSController : Controller
    {
        private readonly ILogger<GuestsController> _logger;
        private IMediator mediator;
        public GuestsCQRSController(ILogger<GuestsController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await mediator.Send(new GetGuestByIdQuery { Id = id });
                return response is not null ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Guest {id}: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Get Guest {id}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await mediator.Send(new GetAllGuestQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get All Guests: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Get All Guests");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGuestCommand command)
        {
            try
            {
                return Ok(await mediator.Send(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Guest: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Add Guest");
            }
        }

        [HttpPost("AddPhone")]
        public async Task<IActionResult> AddPhone(AddPhoneCommand command)
        {
            try
            {
                return Ok(await mediator.Send(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Guest Phone: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Add Guest Phone");
            }
        }
    }
}

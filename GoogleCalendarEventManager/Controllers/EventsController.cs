using GoogleCalendarEventManager.DTO;
using GoogleCalendarEventManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace GoogleCalendarEventManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IGoogleCalendarService _calendarService;

        public EventsController(IGoogleCalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetAllEvents(string pageToken = null, int pageSize = 10)
        {
            try
            {
                return Ok(await _calendarService.GetAllEvents(pageToken, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("events/{searchKey}")]
        public async Task<IActionResult> GetEventsFilterByName(string searchKey, string pageToken = null, int pageSize = 10)
        {
            try
            {
                return Ok(await _calendarService.GetEventsFilterByName(searchKey, pageToken, pageSize));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("events/date")]
        public async Task<IActionResult> GetEventsFilterByDate(DateTime startDate, DateTime endDate, string pageToken = null, int pageSize = 10)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("Start date cannot be greater than end date.");
                }

                var filteredEvents = await _calendarService.GetEventsFilterByDate(startDate, endDate, pageToken, pageSize);

                return Ok(filteredEvents);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("events")]
        public async Task<IActionResult> AddEvent([FromBody] EventDto newEvent)
        {
            try
            {
                var validateRe = newEvent.CalendarEventValidator();
                if (!string.IsNullOrEmpty(validateRe))
                {
                    return BadRequest(validateRe);
                }

                var createEvent = await _calendarService.CreateEvent(newEvent);
                return Created("", createEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            try
            {
                var result = await _calendarService.DeleteEvent(id);
                if (!result)
                {
                    return BadRequest("ERROR: Event Not Found");

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

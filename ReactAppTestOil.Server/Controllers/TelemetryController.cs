using Microsoft.AspNetCore.Mvc;
using ReactAppTestOil.Interfaces;
using ReactAppTestOil.Models;
using ReactAppTestOil.Dto;

namespace ReactAppTestOil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryRepository _telemetryRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="telemetryRepository"></param>
        public TelemetryController(ITelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        #region HTTP request methods
        /// <summary>
        /// GET: api/Telemetries
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTelemetries")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Telemetry>))]
        public async Task<ActionResult<List<Telemetry>>> GetTelemetries()
        {
            var telemetries = await _telemetryRepository.GetTelemetries();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(telemetries);
        }


        /// <summary>
        /// Create POST: api/Telemetry
        /// </summary>
        /// <param name="newTelemetryDto"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostTelemetry([FromBody] TelemetryDto newTelemetryDto, int wellId)
        {
            if (newTelemetryDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _telemetryRepository.CreateTelemetry(newTelemetryDto, wellId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        /// <summary>
        /// Update PUT: api/Telemetries/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTelemetryDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutTelemetry([FromBody] TelemetryDto updateTelemetryDto, int id)
        {
            if (updateTelemetryDto == null || id != updateTelemetryDto.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _telemetryRepository.UpdateTelemetry(updateTelemetryDto, id))
            {
                ModelState.AddModelError("", "Something went wrong updating telemetry");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        /// <summary>
        /// DELETE: api/Telemetries/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTelemetry(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _telemetryRepository.DeleteTelemetry(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting telemetry");
            }

            return NoContent();
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using ReactAppTestOil.Interfaces;
using ReactAppTestOil.Models;
using ReactAppTestOil.Dto;

namespace ReactAppTestOil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WellController : ControllerBase
    {
        private readonly IWellRepository _wellRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="wellRepository"></param>
        public WellController(IWellRepository wellRepository)
        {
            _wellRepository = wellRepository;
        }

        #region HTTP request methods

        /// <summary>
        /// GET: api/Wells
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWells")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Well>))]
        public async Task<ActionResult<List<Well>>> GetWells()
        {
            var wells = await _wellRepository.GetWells();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wells);
        }

        /// <summary>
        /// GET: api/Well/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetWell/{id}")]
        [ProducesResponseType(200, Type = typeof(Well))]
        public async Task<ActionResult<Well>> GetWell(int id)
        {
            var well = await _wellRepository.GetWellById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(well);
        }

        /// <summary>
        /// GET: api/ActiveWells
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActiveWells")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Well>))]
        public async Task<ActionResult<List<Well>>> GetActiveWells()
        {
            var wells = await _wellRepository.GetActiveWells();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wells);
        }

        /// <summary>
        /// GET: api/ActiveWellsByCompany
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("GetActiveWellsByCompany")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Well>))]
        public async Task<ActionResult<List<Well>>> GetActiveWellsByCompany(int companyId)
        {
            var wells = await _wellRepository.GetActiveWellsByCompany(companyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wells);
        }

        ///// <summary>
        ///// GET: api/GetWell/{id}/depth
        ///// </summary>
        ///// <param name="id">The ID of the well</param>
        ///// <param name="fromDateTime">The start date and time</param>
        ///// <param name="toDateTime">The end date and time</param>
        ///// <returns>The total depth for the specified well and date range</returns>
        //[HttpGet("GetWell/{id}/depth")]
        // [ProducesResponseType(200, Type = typeof(float))]
        //public async Task<IActionResult<float>> GetTotalDepthByIdAndDates(int id, DateTime fromDateTime, DateTime toDateTime)
        //{
        //    var depth = await _wellRepository.GetTotalDepthByIdAndDates(id, fromDateTime, toDateTime);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(depth);
        //}

        /// <summary>
        /// GET: api/ActiveWellsByCompany
        /// 5.	Показать по каждой АКТИВНОЙ скважине прохождение суммарной глубины по ид компании
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("GetTotalDepthByCompanyIdAndActiveStatus")]
        [ProducesResponseType(200, Type = typeof(float))]
        public async Task<ActionResult<float>> GetTotalDepthByCompanyIdAndActiveStatus(int companyId)
        {
            var totalDepth = await _wellRepository.GetTotalDepthByCompanyIdAndActiveStatus(companyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(totalDepth);
        }

        /// <summary>
        /// Create POST: api/Wells 
        /// </summary>
        /// <param name="newWell"></param>
        /// <param name="companyIds"></param>
        /// <param name="telemetryId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostWell([FromBody] WellDto newWellDto, [FromQuery] int[] companyIds, [FromQuery] int telemetryId)
        {
            if (newWellDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _wellRepository.CreateWell(newWellDto, companyIds, telemetryId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        /// <summary>
        /// Update PUT: api/Wells/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateWellDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutWell(int id, [FromBody] WellDto updateWellDto)
        {
            if (updateWellDto == null || id != updateWellDto.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _wellRepository.UpdateWell(updateWellDto, id))
            {
                ModelState.AddModelError("", "Something went wrong updating well");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        /// <summary>
        /// DELETE: api/Wells/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteWell(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _wellRepository.DeleteWell(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting well");
            }

            return NoContent();
        }

     

        #endregion
    }
}

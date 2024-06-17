using ReactAppTestOil.Dto;
using Microsoft.EntityFrameworkCore;
using ReactAppTestOil.Data;
using ReactAppTestOil.Interfaces;
using ReactAppTestOil.Models;

namespace ReactAppTestOil.Repositories
{
    public class TelemetryRepository : ITelemetryRepository
    {

        private readonly AppDbContext _context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        public TelemetryRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        #region Requests

        /// <summary>
        /// Get telemetries
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Telemetry>> GetTelemetries()
        {
            return await _context.Telemetries.ToListAsync();
        }

        /// <summary>
        /// Create tlemetry
        /// </summary>
        /// <param name="telemetryDto"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        public async Task<bool> CreateTelemetry(TelemetryDto telemetryDto, int wellId)
        {
            var well = _context.Wells.Where(a => a.Id == wellId).FirstOrDefault();

            if (well == null )
                throw new Exception("No such well. :/");

            Telemetry telemetry = new Telemetry
            {
                CustomDate = telemetryDto.CustomDate,
                Depth = telemetryDto.Depth,
            };

            //telemetry.Wells.Add(well);

            _context.Telemetries.Add(telemetry);
            return await Save();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="telemetryDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> UpdateTelemetry(TelemetryDto telemetryDto, int id)
        {
            var dbTelemetry = await _context.Telemetries.FindAsync(id);
            if (dbTelemetry == null)
                throw new Exception("No such telemetry. :/");

            dbTelemetry.CustomDate = telemetryDto.CustomDate;
            dbTelemetry.Depth = telemetryDto.Depth;

            return await Save();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteTelemetry(int id)
        {
            var dbTelemetry = await _context.Telemetries.FindAsync(id);
            if (dbTelemetry == null)
                throw new Exception("No such telemetry. :/");

            _context.Telemetries.Remove(dbTelemetry);
            return await Save();
        }

        /// <summary>
        /// Save in database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        #endregion
    }
}

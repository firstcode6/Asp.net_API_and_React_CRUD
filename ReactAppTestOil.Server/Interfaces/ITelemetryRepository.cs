using ReactAppTestOil.Dto;
using ReactAppTestOil.Models;

namespace ReactAppTestOil.Interfaces
{
    public interface ITelemetryRepository
    {
        /// <summary>
        /// Get all telemetries
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Telemetry>> GetTelemetries();

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="telemetryDto"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        Task<bool> CreateTelemetry(TelemetryDto telemetryDto, int wellId);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="telemetryDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateTelemetry(TelemetryDto telemetryDto, int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteTelemetry(int id);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns></returns>
        Task<bool> Save();
    }
}

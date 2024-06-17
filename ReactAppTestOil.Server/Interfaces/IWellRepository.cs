using ReactAppTestOil.Models;
using ReactAppTestOil.Dto;

namespace ReactAppTestOil.Interfaces
{
    public interface IWellRepository
    {
        /// <summary>
        /// Get all wells
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Well>> GetWells();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Well> GetWellById(int id);

        /// <summary>
        /// Get active wells
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Well>> GetActiveWells();

        /// <summary>
        /// Get active wells by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<ICollection<Well>> GetActiveWellsByCompany(int companyId);

        /// <summary>
        /// Get total depth by id and dates
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        Task<float> GetTotalDepthByIdAndDates(int id, DateTime fromDateTime, DateTime toDateTime);


        /// <summary>
        /// Get Total Depth By CompanyId And Active Status
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<float> GetTotalDepthByCompanyIdAndActiveStatus(int companyId);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="wellDto"></param>
        /// <param name="companyIds"></param>
        /// <param name="telemetryId"></param>
        /// <returns></returns>
        Task<bool> CreateWell(WellDto wellDto, int[] companyIds, int telemetryId);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="wellDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateWell(WellDto wellDto, int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteWell(int id);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns></returns>
        Task<bool> Save();

    }
}

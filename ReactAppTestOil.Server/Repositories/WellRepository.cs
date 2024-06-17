using Microsoft.EntityFrameworkCore;
using ReactAppTestOil.Data;
using ReactAppTestOil.Interfaces;
using ReactAppTestOil.Models;
using ReactAppTestOil.Dto;

namespace ReactAppTestOil.Repositories
{
    public class WellRepository : IWellRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        public WellRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        #region Requests

        /// <summary>
        /// Get Wells
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Well>> GetWells()
        {
            return await _context.Wells.ToListAsync();

            //return await _context.Wells
            //    .Include(w => w.Telemetry)
            //    .Include(w => w.CompanyWells)
            //        .ThenInclude(cw => cw.Company)
            //    .ToListAsync();
        }

        /// <summary>
        /// Get well by id
        /// 1.	Показать список скважин по id  ИЛИ по имени компании 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Well> GetWellById(int id)
        {
            var well = await _context.Wells.FindAsync(id);
            
            if (well == null)
                throw new Exception("No such well. :/");
            return well;
        }

        /// <summary>
        /// Get active wells
        /// 2.	Показать список Активных скважин с работающими на них подрядчиками(компаниями) 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ICollection<Well>> GetActiveWells()
        {
            var wells = await _context.Wells.Where(w => w.Active == true).ToListAsync();
           
            if (wells == null || wells.Count == 0)
                throw new Exception("No such wells. :/");
            return wells;
        }

        /// <summary>
        /// Get active wells by company id
        /// 3.	Показать список АКТИВНЫХ скважин по id  ИЛИ по имени компании 
        /// </summary>
        /// <returns></returns>
        /// <param name="companyId"></param>
        /// <exception cref="Exception"></exception>
        public async Task<ICollection<Well>> GetActiveWellsByCompany(int companyId)
        {
            var wells = await _context.CompanyWells
                .Where(cw => cw.CompanyId == companyId && cw.Well.Active)
                .Select(cw => cw.Well)
                .ToListAsync();

            if (wells == null || wells.Count == 0)
                throw new Exception("No such wells. :/");
            return wells;
        }

        /// <summary>
        /// Get total depth by id and dates
        /// 4.	По ИДу скважины и периоду(от\до) показать прохождение суммарной глубины За период времени (включительно) ????
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<float> GetTotalDepthByIdAndDates(int id, DateTime fromDateTime, DateTime toDateTime)
        {
            var totalDepth = await _context.Telemetries
                .Where(t => t.Wells.Any(w => w.Id == id) && t.CustomDate >= fromDateTime && t.CustomDate <= toDateTime)
                .SumAsync(t => (float?)t.Depth);


            if (totalDepth == null)
                throw new Exception("No such well. :/");

            return totalDepth.Value;
        }

        /// <summary>
        /// Get Total Depth By CompanyId And Active Status.
        /// 5.	Показать по каждой АКТИВНОЙ скважине прохождение суммарной глубиныпо ид компании
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<float> GetTotalDepthByCompanyIdAndActiveStatus(int companyId)
        {
            var totalDepth = await _context.CompanyWells
                .Where(cw => cw.CompanyId == companyId && cw.Well.Active)
                .Select(cw => cw.Well.Telemetry)
                .SumAsync(t => (float?)t.Depth);

            if (totalDepth == null)
                throw new Exception("Not found.");

            return totalDepth.Value;
        }

        /// <summary>
        /// Create well
        /// </summary>
        /// <param name="wellDto"></param>
        /// <param name="companyIds"></param>
        /// <param name="telemetryId"></param>
        /// <returns></returns>
        public async Task<bool> CreateWell(WellDto wellDto, int[] companyIds, int telemetryId)
        {
            //var company = _context.Companies.Where(a => a.Id == companyId).FirstOrDefault();
            var companies = _context.Companies.Where(a => companyIds.Contains(a.Id)).ToList();
            var telemetry = _context.Telemetries.Where(a => a.Id == telemetryId).FirstOrDefault();

            if (companies == null || !companies.Any() || telemetry == null)
                throw new Exception("No such company or telemetry. :/");

            Well well = new Well 
            {
                Name = wellDto.Name,
                Active = wellDto.Active,
                Telemetry = telemetry 
            };

            foreach (var company in companies)
            {
                var companyWell = new CompanyWell()
                {
                    Company = company,
                    Well = well,
                };

                _context.CompanyWells.Add(companyWell);
            }

            _context.Wells.Add(well);
            return await Save();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="wellDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> UpdateWell(WellDto wellDto, int id)
        {
            var dbWell = await _context.Wells.FindAsync(id);
            if (dbWell == null)
                throw new Exception("No such well. :/");

            dbWell.Name = wellDto.Name;
            dbWell.Active = wellDto.Active;

            return await Save();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteWell(int id)
        {
            var dbWell = await _context.Wells.FindAsync(id);
            if (dbWell == null)
                throw new Exception("No such well. :/");

            _context.Wells.Remove(dbWell);
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

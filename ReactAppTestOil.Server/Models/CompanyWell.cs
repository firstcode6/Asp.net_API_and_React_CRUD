namespace ReactAppTestOil.Models
{
    public class CompanyWell
    {
        /// <summary>
        /// CompanyId
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// WellId
        /// </summary>
        public int WellId { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Well
        /// </summary>
        public Well Well { get; set; }
    }
}

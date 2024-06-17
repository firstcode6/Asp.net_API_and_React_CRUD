using System.ComponentModel.DataAnnotations;

namespace ReactAppTestOil.Models
{
    public class Company
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Company()
        {

        }

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of company
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Many to many relationship
        /// </summary>
        public ICollection<CompanyWell> CompanyWells { get; set; } = new List<CompanyWell>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactAppTestOil.Models
{
    public class Well
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Well()
        {

        }

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// active=1, inactive=0
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("Telemetry")]
        public int TelemetryId { get; set; }

        /// <summary>
        /// The object of Telemetry
        /// </summary>
        public Telemetry Telemetry { get; set; }

        /// <summary>
        /// Many to many
        /// </summary>
        public ICollection<CompanyWell> CompanyWells { get; set; } = new List<CompanyWell>();

    }
}

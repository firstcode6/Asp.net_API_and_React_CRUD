using System.ComponentModel.DataAnnotations;

namespace ReactAppTestOil.Models
{
    public class Telemetry
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Telemetry()
        {
            
        }

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime CustomDate { get; set; }

        /// <summary>
        /// Depth
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// ICollection of Wells
        /// </summary>
        public ICollection<Well> Wells { get; set; } = new List<Well>();
    }
}

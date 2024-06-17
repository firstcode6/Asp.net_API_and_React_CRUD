using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Dto
{
    public class TelemetryDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public TelemetryDto()
        {

        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime CustomDate { get; set; }

        /// <summary>
        /// Depth
        /// </summary>
        public float Depth { get; set; }
    }
}

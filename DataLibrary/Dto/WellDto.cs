using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Dto
{
    public class WellDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public WellDto()
        {

        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// active=1, inactive=0
        /// </summary>
        public bool Active { get; set; }
    }
}

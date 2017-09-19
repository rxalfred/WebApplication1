using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContosoUniversity.BusinessLogic.ApiLogic.BusinessObjects.Data
{
    public class GetTripRequest
    {
        /// <summary>
        /// Departure start date
        /// </summary>
        [Required]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// From Place Id
        /// </summary>
        [Required]
        public int FromPlaceId { get; set; }

        /// <summary>
        /// To Place Id
        /// </summary>
        [Required]
        public int ToPlaceId { get; set; }

        /// <summary>
        /// From SubPlace Id
        /// </summary>
        public int? FromSubPlaceId { get; set; }

        /// <summary>
        /// To SubPlace Id
        /// </summary>
        public int? ToSubPlaceId { get; set; }

        /// <summary>
        /// Company Ids
        /// </summary>
        public List<int> CompanyIds { get; set; }

        /// <summary>
        /// Currency for ticket fare
        /// </summary>
        public string Currency { get; set; }
    }
}

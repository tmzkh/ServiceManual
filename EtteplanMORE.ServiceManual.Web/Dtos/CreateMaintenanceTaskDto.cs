using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.Web.Dtos {
    /// <summary>
    /// Model for create maintenance task request
    /// </summary>
    public class CreateMaintenanceTaskDto {
        [Required]
        public int? FactoryDeviceId { get; set; }
        [Required, RegularExpression(
            @"^(?<year>[0-9]{4})-?(?<month>1[0-2]|0[1-9])-?(?<day>3[01]|0[1-9]|[12][0-9]) (?<hour>2[0-3]|[01][0-9]):?(?<minute>[0-5][0-9]):?(?<second>[0-5][0-9])$",
            ErrorMessage = "Time must be in format: YYYY-MM-DD HH:MM:SS")]
        public string Time { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, Range(0, 2, ErrorMessage = "Criticality must be in range of 0...2")]
        public int Criticality { get; set; }
    }
}

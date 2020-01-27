using System.ComponentModel.DataAnnotations;

namespace EtteplanMORE.ServiceManual.Web.Dtos {
    /// <summary>
    /// Model for update maintenance task request
    /// </summary>
    public class UpdateMaintenanceTaskDto {
        [Required]
        public int? Id { get; set; }
        [Required]
        public int FactoryDeviceId { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, Range(0, 2)]
        public int? Criticality { get; set; }
        [Required]
        public bool? Done { get; set; }
    }
}

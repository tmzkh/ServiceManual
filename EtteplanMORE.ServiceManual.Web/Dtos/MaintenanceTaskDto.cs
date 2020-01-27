using System;

namespace EtteplanMORE.ServiceManual.Web.Dtos {
    /// <summary>
    /// Model for maintenance task result
    /// </summary>
    public class MaintenanceTaskDto {
        public int Id { get; set; }
        public int FactoryDeviceId { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public string Criticality { get; set; }
        public bool Done { get; set; }
    }
}

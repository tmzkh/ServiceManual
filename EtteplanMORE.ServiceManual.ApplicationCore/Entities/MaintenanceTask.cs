using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities {
    /// <summary>
    /// Maintenance Task model for db
    /// </summary>
    [Table("MaintenanceTasks", Schema = "ServiceManual")]
    public class MaintenanceTask {
        public int Id { get; set; }
        public int FactoryDeviceId { get; set; }
        public DateTime Time { get; set; }
        public FactoryDevice FactoryDevice { get; set; }
        public string Description { get; set; }
        public Criticality Criticality { get; set; }
        public bool Done { get; set; }
    }

    public enum Criticality {
        Critical,
        Important,
        Minor
    }
}

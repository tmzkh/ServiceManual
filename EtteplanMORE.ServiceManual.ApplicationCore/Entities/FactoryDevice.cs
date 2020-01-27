using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    /// <summary>
    /// Factory Device model for db
    /// </summary>
    [Table("FactoryDevices", Schema = "ServiceManual")]
    public class FactoryDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
    }
}
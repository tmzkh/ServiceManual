namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    /// <summary>
    /// Model for factory device result
    /// </summary>
    public class FactoryDeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
    }
}
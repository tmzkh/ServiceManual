using CsvHelper;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.AppContext {
    public static class DbSeeder {
        private static Random rng = new Random();
        private static readonly string[] descs = new string[] { "Kaikki räjähtää", "Pyörii mutta ei toimi", "Korjataan joskus" };


        /// <summary>
        /// Reads seeddata.csv and seed db with provided devices and creates maintenance tasks to all devices
        /// </summary>
        /// <param name="context"></param>
        public static void Seed(FactoryContext context) {
            // read records from csv file
            string seedDataFilePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            seedDataFilePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(seedDataFilePath).FullName).FullName).FullName).FullName;
            seedDataFilePath += @"\seeddata.csv";
            TextReader reader = new StreamReader(seedDataFilePath);
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<FactoryDeviceFromCsv>();

            foreach (var record in records) {
                var device = new FactoryDevice {
                    Name = record.Name,
                    Type = record.Type,
                    Year = record.Year
                };
                context.FactoryDevices.Add(device);
                context.SaveChanges();
                for (int i = 0; i < 3; i++) {
                    var crit = rng.Next(0, 3);
                    var timeInMilliseconds = DateTime.Now.AddDays(-(rng.Next(0, 365)));
                    context.MaintenanceTasks.Add(new MaintenanceTask {
                        FactoryDeviceId = device.Id,
                        Criticality = (Criticality)crit,
                        Description = descs[crit],
                        Time = timeInMilliseconds
                    });
                }
                context.SaveChanges();
            }
        }
    }

    class FactoryDeviceFromCsv {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
    }

}

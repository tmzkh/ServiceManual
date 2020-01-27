using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.MaintenanceTaskServiceTests {
    public class MaintenanceTaskPostPutAndDelete {
        private readonly DbContextOptionsBuilder<FactoryContext> builder = new DbContextOptionsBuilder<FactoryContext>()
                .UseMySql("server=xxx;database=ServiceManual;userid=xxx;pwd=xxx;SslMode=none");

        private readonly ITestOutputHelper output;

        public MaintenanceTaskPostPutAndDelete(ITestOutputHelper output) {
            this.output = output;
        }

        /// <summary>
        /// First tests creating a new maintenance task, then modifies it and then deletes it
        /// </summary>
        [Fact]
        public async void CreateModifyAndDeleteMT() {

            var fdId = 1;
            var dt = DateTime.Now;
            var desc = "testing creation";
            var crit = 0;

            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService service = new MaintenanceTaskService(context);
                MaintenanceTask created = await CreateMT(
                    service,
                    new MaintenanceTask {
                        FactoryDeviceId = fdId,
                        Time = dt,
                        Description = desc,
                        Criticality = (Criticality)crit
                    });

                Assert.NotNull(created);
                Assert.Equal(fdId, created.FactoryDeviceId);
                Assert.Equal(dt, created.Time);
                Assert.Equal(desc, created.Description);
                Assert.Equal((Criticality)crit, created.Criticality);
                Assert.False(created.Done);

                created.FactoryDeviceId = 2;
                created.Description = "testing modification";
                created.Criticality = Criticality.Minor;
                created.Done = true;

                MaintenanceTask updated = await UpdateMT(service, created);

                Assert.NotNull(updated);
                Assert.Equal(created.Id, updated.Id);
                Assert.NotEqual(fdId, updated.FactoryDeviceId);
                Assert.Equal(created.FactoryDeviceId, updated.FactoryDeviceId);
                Assert.Equal(dt, updated.Time);
                Assert.NotEqual(desc, updated.Description);
                Assert.Equal(created.Description, updated.Description);
                Assert.NotEqual((Criticality)crit, updated.Criticality);
                Assert.Equal(created.Criticality, updated.Criticality);
                Assert.True(created.Done);

                bool deleted = await DeleteMT(service, updated.Id);

                Assert.True(deleted);
                Assert.Null(await service.Get(updated.Id));
            }
        }

        private async Task<MaintenanceTask> CreateMT(
            IMaintenanceTaskService service, MaintenanceTask task) {
            return await service.Create(task);
        }

        private async Task<MaintenanceTask> UpdateMT(
            IMaintenanceTaskService service, MaintenanceTask task) {
            return await service.Update(task);
        }

        private async Task<bool> DeleteMT(
            IMaintenanceTaskService service, int id) {
            return await service.Delete(id);
        }

    }
}

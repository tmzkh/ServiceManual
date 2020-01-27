using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.MaintenanceTaskServiceTests {
    public class MaintenanceTaskGet {

        private readonly DbContextOptionsBuilder<FactoryContext> builder = new DbContextOptionsBuilder<FactoryContext>()
                .UseMySql("server=localhost;database=ServiceManual;userid=ServiceManualUser;pwd=TooEasyToGuess;SslMode=none");

        /// <summary>
        /// Tests fetching all (paginated though) maintenance tasks from db
        /// </summary>
        [Fact]
        public async void AllTasks() {
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var mts = (await maintenanceTaskService.GetAll(paginationOpts)).ToList();
                Assert.NotNull(mts);
                Assert.NotEmpty(mts);
                Assert.Equal(50, mts.Count);
            }
        }

        /// <summary>
        /// Tests fetching maintenance tasks from db with pagination options
        /// </summary>
        [Fact]
        public async void TasksPaginationOptions() {
            var pageSize = 25;
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                paginationOpts.PageSize = pageSize;
                paginationOpts.PageNumber = 5;
                var mts = (await maintenanceTaskService.GetAll(paginationOpts)).ToList();
                Assert.NotNull(mts);
                Assert.NotEmpty(mts);
                Assert.Equal(pageSize, mts.Count);
            }
        }

        /// <summary>
        /// Tests fetching maintenance tasks by existing factory device
        /// </summary>
        [Fact]
        public async void TasksByExistingDevice() {
            var fdId = 25;
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var mts = (await maintenanceTaskService.GetAllByDeviceId(fdId, paginationOpts)).ToList();
                Assert.NotNull(mts); 
                Assert.NotEmpty(mts);
            }
        }

        /// <summary>
        /// Tests fetching maintenance tasks by non existing factory device
        /// </summary>
        [Fact]
        public async void TasksByNonExistingDevice() {
            var fdId = 2222;
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var mts = (await maintenanceTaskService.GetAllByDeviceId(fdId, paginationOpts)).ToList();
                Assert.Empty(mts);
            }
        }

        /// <summary>
        /// Tests fetching existing maintenance task
        /// </summary>
        [Fact]
        public async void ExistingTaskWgId() {
            var mtId = 25;
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var mt = (await maintenanceTaskService.Get(mtId));
                Assert.NotNull(mt);
                Assert.Equal(mt.Id, mtId);
            }
        }

        /// <summary>
        /// Tests fetching non existing maintenance task
        /// </summary>
        [Fact]
        public async void NonExistingTaskWId() {
            var mtId = 2222222;
            using (var context = new FactoryContext(builder.Options)) {
                IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var mt = (await maintenanceTaskService.Get(mtId));
                Assert.Null(mt);
            }
        }

    }
}

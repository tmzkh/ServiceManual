using System;
using System.Linq;
using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.FactoryDeviceServiceTests
{
    public class FactoryDeviceGet
    {

        private readonly DbContextOptionsBuilder<FactoryContext> builder = new DbContextOptionsBuilder<FactoryContext>()
                .UseMySql("server=localhost;database=ServiceManual;userid=ServiceManualUser;pwd=TooEasyToGuess;SslMode=none");

        /// <summary>
        /// Tests fetching all factory devices from db (paginated though)
        /// </summary>
        [Fact]
        public async void AllDevices()
        {
            using (var context = new FactoryContext(builder.Options)) {
                IFactoryDeviceService factoryDeviceService = new FactoryDeviceService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var fds = (await factoryDeviceService.GetAll(paginationOpts)).ToList();
                Assert.NotNull(fds);
                Assert.NotEmpty(fds);
                Assert.Equal(50, fds.Count);
            }

        }

        /// <summary>
        /// Tests fetching existing factory device
        /// </summary>
        [Fact]
        public async void ExistingDeviceWithId()
        {
            int fdId = 1;
            using (var context = new FactoryContext(builder.Options)) {
                IFactoryDeviceService factoryDeviceService = new FactoryDeviceService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var fd = (await factoryDeviceService.Get(fdId));
                Assert.NotNull(fd);
                Assert.Equal(fdId, fd.Id);
            }

        }

        /// <summary>
        /// Tests fetching non existing factory device
        /// </summary>
        [Fact]
        public async void NonExistingDeviceWithId()
        {
            int fdId = 2222;
            using (var context = new FactoryContext(builder.Options)) {
                IFactoryDeviceService factoryDeviceService = new FactoryDeviceService(context);
                PaginationOpts paginationOpts = new PaginationOpts();
                var fd = (await factoryDeviceService.Get(fdId));
                Assert.Null(fd);
            }
        }
    }
}

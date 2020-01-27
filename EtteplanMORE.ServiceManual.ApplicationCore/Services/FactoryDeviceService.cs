using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using Microsoft.EntityFrameworkCore;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService {
        private readonly FactoryContext _context;

        public FactoryDeviceService(FactoryContext context) {
            _context = context;
        }

        /// <summary>
        /// Fetches FactoryDevices from db
        /// </summary>
        /// <param name="paginationOpts">Options for result pagination</param>
        /// <returns>IEnumerable of FactoryDevices</returns>
        public async Task<IEnumerable<FactoryDevice>> GetAll(PaginationOpts paginationOpts) {
            try {
                // if no entries in db, seed db
                // REMOVE if you want to seed with own data
                var b = _context.FactoryDevices.Any();
                if (!b) {
                    DbSeeder.Seed(_context);
                }
                return await _context.FactoryDevices
                    .Skip((paginationOpts.PageNumber - 1) * paginationOpts.PageSize)
                    .Take(paginationOpts.PageSize)
                    .ToListAsync();
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Fetches Factory Device from db by id
        /// </summary>
        /// <param name="id">Factory device id</param>
        /// <returns></returns>
        public async Task<FactoryDevice> Get(int id) {
            try {
                return await _context.FactoryDevices.FirstOrDefaultAsync(fd => fd.Id == id);
            } catch (Exception e) {
                throw e;
            }
        }
    }
}
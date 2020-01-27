using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IFactoryDeviceService {

        /// <summary>
        /// Fetches FactoryDevices from db
        /// </summary>
        /// <param name="paginationOpts">Options for result pagination</param>
        /// <returns>IEnumerable of FactoryDevices</returns>
        Task<IEnumerable<FactoryDevice>> GetAll(PaginationOpts paginationOpts);

        /// <summary>
        /// Fetches Factory Device from db by id
        /// </summary>
        /// <param name="id">Factory device id</param>
        /// <returns>FactoryDevice</returns>
        Task<FactoryDevice> Get(int id);
    }
}
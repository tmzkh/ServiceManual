using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces {
    public interface IMaintenanceTaskService {

        /// <summary>
        /// Fetches Maintenance Task from db by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MaintenanceTask</returns>
        Task<MaintenanceTask> Get(int id);

        /// <summary>
        /// Fetches all Maintenance Tasks from db (paginated)
        /// </summary>
        /// <param name="paginationOpts">Pagination options for result</param>
        /// <returns>IEnumerable<MaintenanceTask></returns>
        Task<IEnumerable<MaintenanceTask>> GetAll(PaginationOpts paginationOpts);

        /// <summary>
        /// Fetches all Maintenance Tasks from db (paginated) by Factory Device Id
        /// </summary>
        /// <param name="deviceId">Factory Device Id</param>
        /// <param name="paginationOpts">Pagination options for result</param>
        /// <returns>IEnumerable<MaintenanceTask></returns>
        Task<IEnumerable<MaintenanceTask>> GetAllByDeviceId(int deviceId, PaginationOpts paginationOpts);

        /// <summary>
        /// Creates new Maintenance Task to db
        /// </summary>
        /// <param name="task">Task to create</param>
        /// <returns>Created MaintenanceTask</returns>
        Task<MaintenanceTask> Create(MaintenanceTask task);

        /// <summary>
        /// Updates Maintenance Task in db
        /// </summary>
        /// <param name="task">Task to update</param>
        /// <returns>Updated MaintenanceTask</returns>
        Task<MaintenanceTask> Update(MaintenanceTask task);

        /// <summary>
        /// Deletes MaintenanceTask from db by id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>If MaintenanceTask Deleted</returns>
        Task<bool> Delete(int id);
    }
}

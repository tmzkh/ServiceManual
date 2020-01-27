using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services {
    public class MaintenanceTaskService : IMaintenanceTaskService {

        private readonly FactoryContext _context;

        public MaintenanceTaskService(FactoryContext context) {
            _context = context;
        }

        /// <summary>
        /// Creates new Maintenance Task to db
        /// </summary>
        /// <param name="task">Task to create</param>
        /// <returns>Created MaintenanceTask</returns>
        public async Task<MaintenanceTask> Create(MaintenanceTask task) {
            try {
                var fdExists = await _context.FactoryDevices.AnyAsync(fd => fd.Id == task.FactoryDeviceId);
                if (fdExists) {
                    _context.MaintenanceTasks.Add(task);
                    await _context.SaveChangesAsync();
                    return task;
                }
                return null;
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Deletes MaintenanceTask from db by id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>If MaintenanceTask Deleted</returns>
        public async Task<bool> Delete(int id) {
            try {
                var mt = await _context.MaintenanceTasks
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (mt != null) {
                    _context.MaintenanceTasks.Remove(mt);
                    var deleted = await _context.SaveChangesAsync();
                    return deleted > 0;
                } else {
                    return false;
                }
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Fetches Maintenance Task from db by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MaintenanceTask</returns>
        public async Task<MaintenanceTask> Get(int id) {
            try {
                return await _context.MaintenanceTasks
                .FirstOrDefaultAsync(mt => mt.Id == id);
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Fetches all Maintenance Tasks from db (paginated)
        /// </summary>
        /// <param name="paginationOpts">Pagination options for result</param>
        /// <returns>IEnumerable<MaintenanceTask></returns>
        public async Task<IEnumerable<MaintenanceTask>> GetAll(PaginationOpts paginationOpts) {
            try {
                return await _context.MaintenanceTasks
                    .OrderBy(mt => mt.Criticality)
                    .ThenByDescending(mt => mt.Time)
                    .Skip((paginationOpts.PageNumber - 1) * paginationOpts.PageSize)
                    .Take(paginationOpts.PageSize)
                    .ToListAsync();
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Fetches all Maintenance Tasks from db (paginated) by Factory Device Id
        /// </summary>
        /// <param name="deviceId">Factory Device Id</param>
        /// <param name="paginationOpts">Pagination options for result</param>
        /// <returns>IEnumerable<MaintenanceTask></returns>
        public async Task<IEnumerable<MaintenanceTask>> GetAllByDeviceId(int deviceId, PaginationOpts paginationOpts) {
            try {
                return await _context.MaintenanceTasks
                .Where(mt => mt.FactoryDeviceId == deviceId)
                .OrderBy(mt => mt.Criticality)
                .ThenByDescending(mt => mt.Time)
                .Skip((paginationOpts.PageNumber - 1) * paginationOpts.PageSize)
                .Take(paginationOpts.PageSize)
                .ToListAsync();
            } catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// Updates Maintenance Task in db
        /// </summary>
        /// <param name="task">Task to update</param>
        /// <returns>Updated MaintenanceTask</returns>
        public async Task<MaintenanceTask> Update(MaintenanceTask request) {
            try {
                var fdExists = await _context.FactoryDevices.AnyAsync(fd => fd.Id == request.FactoryDeviceId);
                if (!fdExists)
                    return null;
                var taskToUpdate = await _context.MaintenanceTasks.FirstOrDefaultAsync(mt => mt.Id == request.Id);
                if (taskToUpdate == null)
                    return null;

                taskToUpdate.FactoryDeviceId = request.FactoryDeviceId;
                taskToUpdate.Description = request.Description;
                taskToUpdate.Criticality = request.Criticality;
                taskToUpdate.Done = request.Done;

                _context.MaintenanceTasks.Update(taskToUpdate);
                var updated = await _context.SaveChangesAsync();
                if (updated > 0) {
                    return taskToUpdate;
                } else {
                    return null;
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

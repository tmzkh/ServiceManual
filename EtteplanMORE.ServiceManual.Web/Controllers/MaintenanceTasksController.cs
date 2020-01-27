using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions;
using EtteplanMORE.ServiceManual.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.Web.Controllers {
    [Route("api/[controller]")]
    public class MaintenanceTasksController : Controller {
        private readonly IMaintenanceTaskService _service;

        public MaintenanceTasksController(IMaintenanceTaskService service) {
            _service = service;
        }

        /// <summary>
        /// HTTP GET: api/maintenancetasks/
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<MaintenanceTaskDto>> Get(PaginationOpts paginationOpts) {
            return (await _service.GetAll(paginationOpts))
                .Select(mt =>
                    new MaintenanceTaskDto {
                        Id = mt.Id,
                        Description = mt.Description,
                        Criticality = mt.Criticality.ToString(),
                        FactoryDeviceId = mt.FactoryDeviceId,
                        Done = mt.Done,
                        Time = mt.Time
                    }
                );
        }

        /// <summary>
        /// HTTP GET: api/maintenancetasks/1
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            var mt = await _service.Get(id);
            if (mt == null) {
                return NotFound();
            }

            return Ok(new MaintenanceTaskDto {
                Id = mt.Id,
                Description = mt.Description,
                Criticality = mt.Criticality.ToString(),
                FactoryDeviceId = mt.FactoryDeviceId,
                Done = mt.Done,
                Time = mt.Time
            });
        }

        /// <summary>
        ///     HTTP GET: api/maintenancetasks/bydevice/1
        /// </summary>
        [HttpGet("bydevice/{deviceId}")]
        public async Task<IEnumerable<MaintenanceTaskDto>> GetByDevice([FromQuery] PaginationOpts paginationOpts, int deviceId) {
            return (await _service.GetAllByDeviceId(deviceId, paginationOpts))
                .Select(mt =>
                    new MaintenanceTaskDto {
                        Id = mt.Id,
                        Description = mt.Description,
                        Criticality = mt.Criticality.ToString(),
                        FactoryDeviceId = mt.FactoryDeviceId,
                        Done = mt.Done,
                        Time = mt.Time
                    }
                );
        }

        /// <summary>
        /// HTTP POST: api/maintenancetasks
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaintenanceTaskDto maintenanceTask) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.Select(inv => inv.Errors.Select(er => er.ErrorMessage));
                    return BadRequest(new { errors });
                }
                var newTask = await _service.Create(new MaintenanceTask {
                    Description = maintenanceTask.Description,
                    FactoryDeviceId = (int)maintenanceTask.FactoryDeviceId,
                    Criticality = (Criticality)maintenanceTask.Criticality,
                    Time = DateTime.Parse(maintenanceTask.Time)
                });
                if (newTask == null)
                    return BadRequest(new { error = "Device with provided id not found" });

                return CreatedAtAction(nameof(Get), new { newTask }, new MaintenanceTaskDto {
                    Id = newTask.Id,
                    Description = newTask.Description,
                    FactoryDeviceId = newTask.FactoryDeviceId,
                    Criticality = newTask.Criticality.ToString(),
                    Time = newTask.Time,
                    Done = newTask.Done
                });
            } catch (DbUpdateException ue) {
                ConsoleLogError("Create", ue, "DbUpdateException");
                if (((MySql.Data.MySqlClient.MySqlException)ue.InnerException).Number == 1452) {
                    return BadRequest(new { error = "Device with provided id not found" });
                }
                return Conflict();
            } catch (Exception e) {
                ConsoleLogError("Create", e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HTTP PUT: api/maintenancetasks
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMaintenanceTaskDto maintenanceTask) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.Select(inv => inv.Errors.Select(er => er.ErrorMessage));
                    return BadRequest(new { errors });
                }

                var task = await _service.Update(new MaintenanceTask {
                    Id = (int)maintenanceTask.Id,
                    FactoryDeviceId = maintenanceTask.FactoryDeviceId,
                    Description = maintenanceTask.Description,
                    Criticality = (Criticality)maintenanceTask.Criticality,
                    Done = (bool)maintenanceTask.Done,
                });

                if (task != null) {
                    return Ok(new MaintenanceTaskDto {
                        Id = task.Id,
                        FactoryDeviceId = task.FactoryDeviceId,
                        Description = task.Description,
                        Criticality = task.Criticality.ToString(),
                        Done = task.Done,
                        Time = task.Time
                    });
                } else {
                    return Conflict();
                }
            } catch (DbUpdateException dbue) {
                ConsoleLogError("Update", dbue, "DbUpdateException");
                return Conflict();
            } catch (Exception e) {
                ConsoleLogError("Update", e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HTTP DELETE: api/maintenancetasks/1
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var deleted = await _service.Delete(id);
                if (deleted) {
                    return Ok();
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                ConsoleLogError("Delete", e);
                return StatusCode(500);
            }
        }


        private static void ConsoleLogError(string method, Exception e, string exType = "exception") {
            Console.WriteLine();
            Console.WriteLine($"------------------{method}----------------------");
            Console.WriteLine();
            Console.WriteLine($"{exType}");
            Console.WriteLine(e);
            Console.WriteLine();
            Console.WriteLine($"------------------/{method}----------------------");
            Console.WriteLine();
        }
    }
}


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;
using UberSystem.Service;
using UberSytem.Dto;
using UberSytem.Dto.Responses;
using UberSystem.Domain.Entities;

namespace UberSystem.Api.Driverv1.Controllers
{
    public class DriverController : BaseApiController
    {
        private readonly UberSystemDbContext _context;
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly DriverLocationService _locationService;
        private readonly ILogger<DriverController> _logger;



        public DriverController(UberSystemDbContext context, IDriverService driverService, IMapper mapper, IHostApplicationLifetime applicationLifetime,
                                                                          DriverLocationService driverLocationService, ILogger<DriverController> logger)
        {
            _context = context;
            _driverService = driverService;
            _mapper = mapper;
            _appLifetime = applicationLifetime;
            _locationService = driverLocationService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve customers in system 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("drivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Driver>>> GetDrivers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            return await _context.Drivers.ToListAsync();
        }

        /// <summary>
        /// Retrieve customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("drivers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Driver>> GetDrivers(long id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Driver in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPut("drivers/{id}")]
        public async Task<IActionResult> UpdateDrivers(long id,DriverReponseModel drivermodel)
        {        
            try
            {
                var driver = await _context.Drivers.FindAsync(id);

                if (driver is null)
                {
                    return BadRequest();
                }
                else
                {
                    var response = _mapper.Map<Driver>(drivermodel);
                   
                    var check = await _driverService.Update(id, response);
                    if (check is true)
                    {
                        return Ok(new ApiResponseModel<UserResponseModel>
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = " Update Success",

                        });
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        /// <summary>
        /// Create Driver in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost("drivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Driver>> PostCustomer(Domain.Entities.Driver driver)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'UberSystemDbContext.Customers'  is null.");
            }
           _context.Drivers.Add(driver);
           try
           {
                await _context.SaveChangesAsync();
           }
            catch (DbUpdateException)
            {
                if (CustomerExists(driver.Id))
                {
                    return Conflict();
               }
               else
               {
                   throw;
               }
           }

           return CreatedAtAction("GetCustomer", new { id = driver.Id }, driver);
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Delete Driver in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpDelete("drivers/{id}")]
        public async Task<IActionResult> DeleteDriver(long id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
           }

          _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

           return NoContent();
        }



        private bool CustomerExists(long id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost("updateStatus/{id}")]
        public async Task<IActionResult> UpdateDriverStatus(long id ,string status)
        {
            
            if (status == "chờ khách" || status == "Đang thực hiện nhiệm vụ")
            {
                if (!_locationService.IsRunning)
                {
                    _logger.LogInformation("Starting UpdateLocationService...");
                    await _locationService.StartAsync(id, new CancellationToken());
                }
            }
            else
            {
                if (_locationService.IsRunning)
                {
                    await _locationService.StopAsync(new CancellationToken());
                    _logger.LogInformation("Stopped UpdateLocationService");
                }
            }

            return Ok(new { message = "Driver status updated" });
        }
    }
}

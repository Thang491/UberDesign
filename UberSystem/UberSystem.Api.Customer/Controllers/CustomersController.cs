using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.NetworkInformation;
using UberSystem.Domain.Entities;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;
using UberSystem.Service;
using UberSytem.Dto;
using UberSytem.Dto.Requests;
using UberSytem.Dto.Responses;

namespace UberSystem.Api.Customer.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly UberSystemDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDriverService _driverService;

        private readonly IHostApplicationLifetime _appLifetime;
        private readonly DriverLocationService _locationService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(UberSystemDbContext context, IUserService userService, IMapper mapper, IDriverService driverService, IHostApplicationLifetime applicationLifetime,
                                                                          DriverLocationService driverLocationService, ILogger<CustomersController> logger)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _driverService = driverService;
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
        [HttpGet("customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Customer>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Retrieve customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Customer>> GetCustomer(long id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPut("customers/{id}")]
        public async Task<IActionResult> PutCustomer(long id)
        {

            /*if (id != customer.Id)
            {
               return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;*/

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {/*
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return NoContent();
        }

        /* POST: api/Customers
 To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754*/
        /// <summary>
        /// Create customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost("customers")]
        public async Task<ActionResult<Domain.Entities.Customer>> PostCustomer(Domain.Entities.Customer customer)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'UberSystemDbContext.Customers'  is null.");
            }
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
        /// <summary>
        /// Delete customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        //DELETE: api/Customers/5
        [HttpDelete("customers/{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        ///  Customer Rating in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost("customers/{id}")]
        public async Task<ActionResult<Domain.Entities.Rating>> CustomerRating(long id, CustomerRatingReponseModel reponse)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'UberSystemDbContext.Customers'  is null.");
            }
            try
            {
                var rating = _mapper.Map<Rating>(reponse);
                Random random = new Random();

                long randomLong = random.Next(10000000, 100000000);
                rating.Id = randomLong;
                await _context.Ratings.AddAsync(rating);
                await _context.SaveChangesAsync();


                return Ok(new ApiResponseModel<CustomerRatingReponseModel>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = " Update Success",
                    Data = reponse
                });

            }
            catch (DbUpdateException)
            {
                if (CustomerExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

        }
        [HttpPost("orderTrip")]
        public async Task<IActionResult> OrderTrip([FromBody] TripRequestModel tripRequest)
        {
            if (tripRequest == null)
            {
                return BadRequest("Invalid trip request.");
            }

            var availableDrivers = await _driverService.GetAvailableDriversAsync(tripRequest.PickupLatitude, tripRequest.PickupLongitude, 2);

            if (!availableDrivers.Any())
            {
                return NotFound("No available drivers in the area.");
            }

            var acceptedDriver = availableDrivers.FirstOrDefault(); // Hoặc bạn có thể lấy tài xế từ yêu cầu của API

            if (acceptedDriver != null)
            {
                // Tạo đối tượng chuyến đi
                var trip = new Trip
                {
                    CustomerId = tripRequest.CustomerId, // Giả định bạn có CustomerId trong request
                    DriverId = acceptedDriver.Id,
                    SourceLatitude = tripRequest.PickupLatitude,
                    SourceLongitude = tripRequest.PickupLongitude,
                    DestinationLatitude = tripRequest.DestinationLatitude,
                    DestinationLongitude = tripRequest.DestinationLongitude,
                    Status = "Dang thuc hien nhiem vu" // Trạng thái chuyến đi
                };

                // Lưu chuyến đi vào cơ sở dữ liệu
                await _context.Trips.AddAsync(trip); // Giả định bạn có service để tạo chuyến đi


                if (trip.Status == "cho khach" || trip.Status == "Dang thuc hien nhiem vu")
                {
                    if (!_locationService.IsRunning)
                    {
                        _logger.LogInformation("Starting UpdateLocationService...");
                        await _locationService.StartAsync(acceptedDriver.Id, new CancellationToken());
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
                return Ok(new { message = "Trip order successful", drivers = availableDrivers });

            }

            return BadRequest("Unable to accept trip.");
        
            // Tiếp tục xử lý việc đặt chuyến, có thể lưu thông tin chuyến đi và thông báo cho tài xế
            // ...

           
        }

        

       

        private bool CustomerExists(long id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

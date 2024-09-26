using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UberSystem.Domain.Interfaces.Services;

namespace UberSystem.Service
{
    public class DriverLocationService 
    {
        private readonly ILogger<DriverLocationService> _logger;
        private readonly IDriverService _driverService;
        private long _driverId;
        private bool _isRunning = false;
        public bool IsRunning => _isRunning;
        public DriverLocationService(ILogger<DriverLocationService> logger,IDriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }

        public async Task StartAsync(long driverId, CancellationToken stoppingToken)
        {
            _driverId = driverId;
            _isRunning = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                // Cập nhật vị trí tài xế với driverId
                UpdateDriverLocation(_driverId);

                _logger.LogInformation("Location updated for driver {id} at: {time}", _driverId, DateTimeOffset.Now);

                // Delay 15 giây trước khi tiếp tục
                await Task.Delay(15000, stoppingToken);
            }
        }
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _isRunning = false;
            _logger.LogInformation("Stopping UpdateLocationService for driver {id}", _driverId);
            return Task.CompletedTask;
        }

        private async void UpdateDriverLocation(long id)
        {
            // Logic để cập nhật vị trí tài xế
          var Driver =  await _driverService.findDriverbyId(id);
            Random random = new Random();
            Driver.LocationLatitude = random.Next(1000, 10000) / 10000.0;
            Driver.LocationLongitude = random.Next(1000, 10000) / 10000.0;
            await _driverService.Update(id,Driver);

        }
    }
}

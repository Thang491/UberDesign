using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Entities;
using UberSystem.Domain.Interfaces;
using UberSystem.Domain.Interfaces.Services;

namespace UberSystem.Service
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DriverService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task Add(Driver driver)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Driver driver)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Driver>> GetDrivers()
        {
            throw new NotImplementedException();
        }
        public async Task<Driver> findDriverbyId(long id)
        {
            var driverExist = await _unitOfWork.Repository<Driver>().FindAsync(id);
            if (driverExist is null) throw new Exception("Driver not exist.");

            else
            {
                return driverExist;
            }
        }

        public async Task<bool> Update(long id, Driver driver)
        {
            var driverExist = await _unitOfWork.Repository<Driver>().FindAsync(id);

            if (driverExist is null) throw new Exception("Driver not exist.");

            else
            {
                driverExist.LocationLatitude = driver.LocationLatitude;
                driverExist.LocationLongitude = driver.LocationLongitude;
                driverExist.Dob = driver.Dob;
                await _unitOfWork.Repository<Driver>().UpdateAsync(driverExist);
                await _unitOfWork.CommitTransaction();

                return true;
            }
        }


        public async Task<List<Driver>> GetAvailableDriversAsync(double pickupLatitude, double pickupLongitude, double radiusInKm)
        {
            var drivers = await _unitOfWork.Repository<Driver>().GetAllAsync();

            var availableDrivers = drivers.Where(d =>
            {
                if (d.LocationLatitude == null || d.LocationLongitude == null)
                    return false;

                double distance = CalculateDistance(pickupLatitude, pickupLongitude, d.LocationLatitude.Value, d.LocationLongitude.Value);
                return distance <= radiusInKm;
            }).OrderByDescending(d => d.Ratings) // Ưu tiên tài xế có đánh giá cao
              .ToList();

            return availableDrivers;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c; // Distance in km
            return distance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}

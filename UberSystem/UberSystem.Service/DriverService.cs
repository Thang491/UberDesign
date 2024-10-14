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


        public async Task<IQueryable<Driver>> GetAvailableDriversAsync(double pickupLatitude, double pickupLongitude, double radiusInKm)
        {
            var drivers = await _unitOfWork.Repository<Driver>().GetAllAsync();

            var ratings = await _unitOfWork.Repository<Rating>().GetAllAsync();

            var driverRatings = ratings
                .GroupBy(r => r.DriverId)
                .ToDictionary(
                    g => g.Key,
                    g => new { AverageRating = g.Average(r => r.Rating1), Count = g.Count() }
                );

            var availableDrivers = drivers
                .Where(d => d.LocationLatitude != null && d.LocationLongitude != null)
                .Select(d => new
                {
                    Driver = d,
                    Distance = CalculateDistance(pickupLatitude, pickupLongitude, d.LocationLatitude.Value, d.LocationLongitude.Value),
                    Rating = driverRatings.TryGetValue(d.Id, out var rating) ? rating.AverageRating : 0,
                    RatingCount = driverRatings.TryGetValue(d.Id, out var count) ? count.Count : 0
                })
                .Where(d => d.Distance <= radiusInKm)
                .OrderByDescending(d => d.Rating)
                .ThenByDescending(d => d.RatingCount)
                .Select(d => d.Driver)
                .ToList();

            return availableDrivers.AsQueryable();
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
        public async Task<IQueryable<Trip>> getInforOrderUber(long id)
        {
            var trip = await _unitOfWork.Repository<Trip>().GetAllAsync();
            var trips =  trip
        .Where(t => t.CustomerId == id) // Lọc theo CustomerId
        .Take(1000) // Giới hạn số lượng bản ghi trả về
        .ToList();


            return trips.AsQueryable();
        }

        public async Task<IQueryable<Trip>> getInforTrip(long id)
        {
            var trip = await _unitOfWork.Repository<Trip>().GetAllAsync();
            var trips = trip
        .Where(t => t.DriverId == id) // Lọc theo CustomerId
        .Take(1000) // Giới hạn số lượng bản ghi trả về
        .ToList();


            return trips.AsQueryable();
        }
    }
}

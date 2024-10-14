using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Entities;

namespace UberSystem.Domain.Interfaces.Services
{
    public interface IDriverService
    {
       // Task<Driver> FindByEmail(string email);
        Task<bool> Update(long id ,Driver driver);
        Task Add(Driver driver);
        Task Delete(Driver driver);
        Task<Driver> findDriverbyId(long id);
        Task<IEnumerable<Driver>> GetDrivers();
        Task<IQueryable<Driver>> GetAvailableDriversAsync(double pickupLatitude, double pickupLongitude, double radiusInKm);
        Task<IQueryable<Trip>> getInforOrderUber(long id);
        Task<IQueryable<Trip>> getInforTrip(long id);

        
    }
}

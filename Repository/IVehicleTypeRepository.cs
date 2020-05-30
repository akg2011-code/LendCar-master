using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface IVehicleTypeRepository
    {
        LendCarDBContext Context { get; }
        VehicleType GetVehicleType(int id);
        List<VehicleType> GetAllVehicleTypes();
        void Add(VehicleType vehicleType);
        void Delete(int id);
        void Save();
        bool Exists(int id);
    }
}

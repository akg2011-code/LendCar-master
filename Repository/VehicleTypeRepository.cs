using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        public VehicleTypeRepository(LendCarDBContext context)
        {
            this.Context = context;
        }
        public LendCarDBContext Context { get; }

        public void Add(VehicleType vehicleType) => Context.VehicleTypes.Add(vehicleType);

        public void Delete(int id) => Context.VehicleTypes.Remove(GetVehicleType(id));

        public List<VehicleType> GetAllVehicleTypes() => Context.VehicleTypes.ToList();

        public VehicleType GetVehicleType(int id) => Context.VehicleTypes.Find(id);

        public void Save() => Context.SaveChanges();
        public bool Exists(int id) => Context.VehicleTypes.Any(vt=>vt.Id == id);
    }
}

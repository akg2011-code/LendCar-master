using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface IBrandRepository
    {
        LendCarDBContext Context { get; }
        Brand GetBrand(int id);
        List<Brand> GetAllBrands();
        void Add(Brand brand);
        void Delete(int id);
        void Save();
        bool Exists(int id);
    }
}

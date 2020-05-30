using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface IBrandModelRepository
    {
        LendCarDBContext Context { get; }
        BrandModel GetBrandModel(int id);
        List<BrandModel> GetAllBrandModels();
        void Add(BrandModel model);
        void Delete(int id);
        void Save();
        bool Exists(int id);
    }
}

using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class BrandModelRepository : IBrandModelRepository
    {
        public LendCarDBContext Context { get; }
        public BrandModelRepository(LendCarDBContext context)
        {
            this.Context = context;
        }
        public void Add(BrandModel model) => Context.BrandModels.Add(model);
        public void Delete(int id) => Context.BrandModels.Remove(GetBrandModel(id));
        public List<BrandModel> GetAllBrandModels() => Context.BrandModels.Include(bm=>bm.Brand).ToList();
        public BrandModel GetBrandModel(int id) => Context.BrandModels.Find(id);
        public void Save() => Context.SaveChanges();
        public bool Exists(int id) => Context.BrandModels.Any(bm => bm.Id == id);
    }
}

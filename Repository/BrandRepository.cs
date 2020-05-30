using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public LendCarDBContext Context { get; }
        public BrandRepository( LendCarDBContext context)
        {
            this.Context = context;
        }
        public void Add(Brand brand) => Context.Brands.Add(brand);
        public void Delete(int id) => Context.Brands.Remove(GetBrand(id));
        public List<Brand> GetAllBrands() => Context.Brands.ToList();
        public Brand GetBrand(int id) => Context.Brands.Find(id);
        public void Save() => Context.SaveChanges();
        public bool Exists(int id) => Context.Brands.Any(b => b.Id == id);
    }
}

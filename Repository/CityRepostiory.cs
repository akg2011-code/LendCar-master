using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class CityRepostiory : ICityRepostiory
    {
        public LendCarDBContext Context { get; }
        public CityRepostiory(LendCarDBContext context)
        {
            Context = context;
        }
        public List<City> GetAllCites() => Context.Cities.ToList();

    }
}

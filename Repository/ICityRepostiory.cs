using System.Collections.Generic;
using LendCar.DBContext;
using LendCar.Models;

namespace LendCar.Repository
{
    public interface ICityRepostiory
    {
        LendCarDBContext Context { get; }

        List<City> GetAllCites();
    }
}
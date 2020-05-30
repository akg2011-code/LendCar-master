using LendCar.DBContext;
using LendCar.Models;

namespace LendCar.Repository
{
    public interface IContactRepository
    {
        LendCarDBContext Context { get; }

        Contact GetCompanyContact();
    }
}
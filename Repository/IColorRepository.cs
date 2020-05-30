using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface IColorRepository
    {
        LendCarDBContext Context { get; }
        Color GetColor(int id);
        List<Color> GetAllColors();
        void Add(Color color);
        void Delete(int id);
        void Save();
        bool Exists(int id);
    }
}

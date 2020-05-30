using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class ColorRepository : IColorRepository
    {
        public LendCarDBContext Context { get; }
        public ColorRepository(LendCarDBContext context)
        {
            this.Context = context;
        }
        public void Add(Color color) => Context.Colors.Add(color);
        public void Delete(int id) => Context.Colors.Remove(GetColor(id));
        public List<Color> GetAllColors() => Context.Colors.ToList();
        public Color GetColor(int id) => Context.Colors.Find(id);
        public void Save() => Context.SaveChanges();
        public bool Exists(int id) => Context.Colors.Any(b => b.Id == id);
    }
}

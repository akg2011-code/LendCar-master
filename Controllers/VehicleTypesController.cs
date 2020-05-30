using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.Models;
using LendCar.Repository;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VehicleTypesController:Controller
    {

        private readonly IVehicleTypeRepository _VTypeRepo;

        public VehicleTypesController(IVehicleTypeRepository VTypeRepo)
        {
            _VTypeRepo = VTypeRepo;
        }

        [HttpGet("{page}/{pagesize}")]
        public IActionResult VTypesList(int page = 1, int pagesize = 10)
        {
            return PartialView("_VehicleTypesList", _VTypeRepo.GetAllVehicleTypes().ToList().ToPagedList(page, pagesize));
        }

        [ActionName("GetVType")]
        [HttpGet("{id}")]
        public IActionResult GetVType(int id)
        {
            var vehicleType = _VTypeRepo.GetVehicleType(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return Ok(vehicleType);
        }


        [ActionName("Edit")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Edit([FromRoute]int id, [FromBody] VehicleType vehicleType, [FromRoute]int page = 1, [FromRoute]int pagesize = 10)
        {
            IActionResult result;
            if (id != vehicleType.Id)
            {
                result = BadRequest();
            }

            _VTypeRepo.Context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                _VTypeRepo.Save();
            }
            catch (DbUpdateConcurrencyException err)
            {
                if (!VTypeExists(id))
                {
                    result = NotFound();
                }
                else
                {
                    result = BadRequest(err.ToString());
                }
            }

            result = VTypesList(page, pagesize);

            return result;
        }


        [ActionName("Delete")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Delete([FromRoute]int id, [FromRoute] int page = 1, [FromRoute] int pagesize = 10)
        {
            var vehicleType = _VTypeRepo.GetVehicleType(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            _VTypeRepo.Delete(id);
            _VTypeRepo.Save();
            return VTypesList(page, pagesize);
        }

        [ActionName("Add")]
        [HttpPost("{page}/{pagesize}")]
        public IActionResult Add([FromBody]VehicleType vehicleType, [FromRoute] int page = 1, [FromRoute]int pagesize = 10)
        {
            _VTypeRepo.Add(vehicleType);
            _VTypeRepo.Save();
            return VTypesList(page, pagesize);
        }

        private bool VTypeExists(int id)
        {
            return _VTypeRepo.Exists(id);
        }
    }
}

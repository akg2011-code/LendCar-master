using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("api/[controller]/[action]")]

    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandsController(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        [HttpGet("{page}/{pagesize}")]
        public IActionResult BrandsList(int page = 1,int pagesize = 10) {
            return PartialView("_BrandsList", _brandRepo.GetAllBrands().ToList().ToPagedList(page, pagesize)); 
        }
        
        [ActionName("GetBrand")]
        [HttpGet("{id}")]
        public IActionResult GetBrand(int id)
        {
            var brand = _brandRepo.GetBrand(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }


        // PUT: api/Brands/edit/id/page
        [ActionName("Edit")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Edit([FromRoute]int id,[FromBody] Brand brand,[FromRoute]int page = 1, [FromRoute]int pagesize = 10)
        {
            IActionResult result;
            if (id != brand.Id)
            {
                result = BadRequest();
            }

            _brandRepo.Context.Entry(brand).State = EntityState.Modified;

            try
            {
                _brandRepo.Save();
            }
            catch (DbUpdateConcurrencyException err)
            {
                if (!BrandExists(id))
                {
                    result =  NotFound();
                }
                else
                {
                    result = BadRequest(err.ToString());
                }
            }

            result =  BrandsList(page,pagesize);
            
            return result;
        }


        [ActionName("Delete")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Delete([FromRoute]int id,[FromRoute] int page = 1, [FromRoute] int pagesize = 10)
        {
            var brand = _brandRepo.GetBrand(id);
            if (brand == null)
            {
                return NotFound();
            }

            _brandRepo.Delete(id);
            _brandRepo.Save();
            return BrandsList(page,pagesize);
        }

        [ActionName("Add")]
        [HttpPost("{page}/{pagesize}")]
        public IActionResult Add([FromBody]Brand brand,[FromRoute] int page = 1,[FromRoute]int pagesize = 10)
        {
            _brandRepo.Add(brand);
            _brandRepo.Save();
            return BrandsList(page, pagesize);
        }

        private bool BrandExists(int id)
        {
            return _brandRepo.Exists(id);
        }
    }
}

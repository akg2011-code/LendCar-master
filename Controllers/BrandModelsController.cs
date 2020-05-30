using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LendCar.Models;
using LendCar.Repository;
using X.PagedList;

namespace LendCar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandModelsController : Controller
    {
        private readonly IBrandModelRepository _bmRepo;

        public BrandModelsController(IBrandModelRepository bmRepo)
        {
            _bmRepo = bmRepo;
        }
        [ActionName("GetBrandModelsPartial")]
        [HttpGet("{brandId}/{page}/{pagesize}")]
        public IActionResult BrandModelsList([FromRoute]int brandId = 0,int page = 1,[FromRoute]int pagesize = 10)
        {
            if(brandId != 0)
                return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().Where(bm=>bm.BrandId == brandId).ToList().ToPagedList(page, pagesize));
            else
                return PartialView("_BrandModelsList", _bmRepo.GetAllBrandModels().ToPagedList(page, pagesize));
        }

        [ActionName("GetBrandModel")]
        [HttpGet("{id}")]
        public IActionResult GetBrandModel(int id)
        {
            var brandModel = _bmRepo.GetBrandModel(id);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }


        // PUT: api/Brands/edit/id/page
        [ActionName("Edit")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Edit([FromRoute]int id, [FromBody] BrandModel brandModel, [FromRoute]int page = 1, [FromRoute]int pagesize = 10)
        {
            IActionResult result;
            if (id != brandModel.Id)
            {
                result = BadRequest();
            }

            _bmRepo.Context.Entry(brandModel).State = EntityState.Modified;

            try
            {
                _bmRepo.Save();
            }
            catch (DbUpdateConcurrencyException err)
            {
                if (!BrandExists(id))
                {
                    result = NotFound();
                }
                else
                {
                    result = BadRequest(err.ToString());
                }
            }

            result = BrandModelsList(0,page,pagesize);
            return result;
        }


        [ActionName("Delete")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Delete([FromRoute]int id, [FromRoute] int page = 1, [FromRoute] int pagesize = 10)
        {
            var brandModel = _bmRepo.GetBrandModel(id);
            if (brandModel == null)
            {
                return NotFound();
            }

            _bmRepo.Delete(id);
            _bmRepo.Save();
            return BrandModelsList(0,page,pagesize);
        }

        [ActionName("Add")]
        [HttpPost("{page}/{pagesize}")]
        public IActionResult Add([FromBody]BrandModel brandModel, [FromRoute] int page = 1, [FromRoute] int pagesize = 10)
        {
            _bmRepo.Add(brandModel);
            _bmRepo.Save();
            return BrandModelsList(0,page,pagesize);
        }

        private bool BrandExists(int id)
        {
            return _bmRepo.Exists(id);
        }

        [ActionName("byBrand")]
        [HttpGet("{id}")]
        public IActionResult GetModelByBrandId([FromRoute]int id)
        {
            var brandModel = _bmRepo.GetAllBrandModels().Where(bm => bm.BrandId == id).OrderBy(bm => bm.Name);

            if (brandModel == null)
            {
                return NotFound();
            }

            return Ok(brandModel);
        }

    }
}

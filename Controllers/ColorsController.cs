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

    public class ColorsController : Controller
    {
        private readonly IColorRepository _colorRepo;

        public ColorsController(IColorRepository colorRepo)
        {
            _colorRepo = colorRepo;
        }

        [HttpGet("{page}/{pagesize}")]
        public IActionResult ColorsList(int page = 1, int pagesize = 10)
        {
            return PartialView("_ColorsList", _colorRepo.GetAllColors().ToList().ToPagedList(page, pagesize));
        }

        [ActionName("GetColor")]
        [HttpGet("{id}")]
        public IActionResult GetColor(int id)
        {
            var color = _colorRepo.GetColor(id);

            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }


        // PUT: api/Colors/edit/id/page
        [ActionName("Edit")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Edit([FromRoute]int id, [FromBody] Color color, [FromRoute]int page = 1, [FromRoute]int pagesize = 10)
        {
            IActionResult result;
            if (id != color.Id)
            {
                result = BadRequest();
            }

            _colorRepo.Context.Entry(color).State = EntityState.Modified;

            try
            {
                _colorRepo.Save();
            }
            catch (DbUpdateConcurrencyException err)
            {
                if (!ColorExists(id))
                {
                    result = NotFound();
                }
                else
                {
                    result = BadRequest(err.ToString());
                }
            }

            result = ColorsList(page, pagesize);

            return result;
        }


        [ActionName("Delete")]
        [HttpPost("{id}/{page}/{pagesize}")]
        public IActionResult Delete([FromRoute]int id, [FromRoute] int page = 1, [FromRoute] int pagesize = 10)
        {
            var color = _colorRepo.GetColor(id);
            if (color == null)
            {
                return NotFound();
            }

            _colorRepo.Delete(id);
            _colorRepo.Save();
            return ColorsList(page, pagesize);
        }

        [ActionName("Add")]
        [HttpPost("{page}/{pagesize}")]
        public IActionResult Add([FromBody]Color color, [FromRoute] int page = 1, [FromRoute]int pagesize = 10)
        {
            _colorRepo.Add(color);
            _colorRepo.Save();
            return ColorsList(page, pagesize);
        }

        private bool ColorExists(int id)
        {
            return _colorRepo.Exists(id);
        }
    }
}

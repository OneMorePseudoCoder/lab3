using lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class material : Controller
    {
        public Lab3Context Context { get; }

        public material(Lab3Context context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Context.Materials.ToList());
        }

        [HttpGet("{material_id}")]
        public IActionResult GetByID(int material_id)
        {
            Material? material = Context.Materials.Where(x => x.MaterialId == material_id).FirstOrDefault();

            if (material == null)
                return BadRequest("Неверный material_id!");

            return Ok(material);
        }

        [HttpPost]
        public IActionResult Add(Material data)
        {
            if ((Context.Materials.Any(u => u.MaterialId == data.MaterialId) || data.MaterialId < 0))
                return BadRequest("Запись с таким material_id уже существует!");

            Context.Materials.Add(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult Update(Material data)
        {
            if ((!Context.Materials.Any(u => u.MaterialId == data.MaterialId) || data.MaterialId < 0))
                return BadRequest("Запись с таким material_id не существует!");

            Context.Materials.Update(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int material_id)
        {
            Material? material = Context.Materials.Where(x => x.MaterialId == material_id).FirstOrDefault();

            if (material == null)
                return BadRequest("Запись с таким material_id не существует!");

            Context.Materials.Remove(material);
            Context.SaveChanges();
            return Ok();
        }
    }
}

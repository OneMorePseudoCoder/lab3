using lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class review : Controller
    {
        public Lab3Context Context { get; }

        public review(Lab3Context context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Context.Reviews.ToList());
        }

        [HttpGet("{review_id}")]
        public IActionResult GetByID(int review_id)
        {
            Review? review = Context.Reviews.Where(x => x.RewiewId == review_id).FirstOrDefault();

            if (review == null)
                return BadRequest("Неверный review_id!");

            return Ok(review);
        }

        [HttpPost]
        public IActionResult Add(Review data)
        {
            if ((Context.Reviews.Any(u => u.RewiewId == data.RewiewId) || data.RewiewId < 0))
                return BadRequest("Запись с таким rewiew_id уже существует!");

            Context.Reviews.Add(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult Update(Review data)
        {
            if ((!Context.Reviews.Any(u => u.RewiewId == data.RewiewId) || data.RewiewId < 0))
                return BadRequest("Запись с таким rewiew_id не существует!");

            Context.Reviews.Update(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int review_id)
        {
            Review? review = Context.Reviews.Where(x => x.RewiewId == review_id).FirstOrDefault();

            if (review == null)
                return BadRequest("Запись с таким rewiew_id не существует!");

            Context.Reviews.Remove(review);
            Context.SaveChanges();
            return Ok();
        }
    }
}

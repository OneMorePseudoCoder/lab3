using lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class course : Controller
    {
        public Lab3Context Context { get; }

        public course(Lab3Context context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Context.Courses.ToList());
        }

        [HttpGet("{course_id}")]
        public IActionResult GetByID(int course_id)
        {
            Course? course = Context.Courses.Where(x => x.CourseId == course_id).FirstOrDefault();

            if (course == null)
                return BadRequest("Неверный course_id!");

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Add(Course data)
        {
            if ((Context.Courses.Any(u => u.CourseId == data.CourseId) || data.CourseId < 0))
                return BadRequest("Запись с таким course_id уже существует!");

            Context.Courses.Add(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult Update(Course data)
        {
            if ((!Context.Courses.Any(u => u.CourseId == data.CourseId) || data.CourseId < 0))
                return BadRequest("Запись с таким course_id не существует!");

            Context.Courses.Update(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int course_id)
        {
            Course? course = Context.Courses.Where(x => x.CourseId == course_id).FirstOrDefault();

            if (course == null)
                return BadRequest("Запись с таким course_id не существует!");

            Context.Courses.Remove(course);
            Context.SaveChanges();
            return Ok();
        }
    }
}

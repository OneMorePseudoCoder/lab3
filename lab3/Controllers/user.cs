using lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class user : ControllerBase
    {
        public Lab3Context Context { get; }

        public user(Lab3Context context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Context.Users.ToList());
        }

        [HttpGet("{user_id}")]
        public IActionResult GetByID(int user_id)
        {
            User? user = Context.Users.Where(x => x.UserId == user_id).FirstOrDefault();

            if (user == null)
                return BadRequest("Неверный user_id!");

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Add(User data)
        {
            if ((Context.Users.Any(u => u.UserId == data.UserId) || data.UserId < 0))
                return BadRequest("Запись с таким user_id уже существует!");

            Context.Users.Add(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult Update(User data)
        {
            if ((!Context.Users.Any(u => u.UserId == data.UserId) || data.UserId < 0))
                return BadRequest("Запись с таким user_id не существует!");

            Context.Users.Update(data);
            Context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int user_id)
        {
            User? user = Context.Users.Where(x => x.UserId == user_id).FirstOrDefault();

            if (user == null)
                return BadRequest("Запись с таким user_id не существует!");

            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok();
        }
    }
}

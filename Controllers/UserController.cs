using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(Database db)
        {
            Db = db;
        }

        // GET api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new User(Db);
            var result = await query.GetAllAsync();
            Console.WriteLine("Test");
            return new OkObjectResult(result);
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new User(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User body)
        {
            await Db.Connection.OpenAsync();
            body.password = BCrypt.Net.BCrypt.HashPassword(body.password);
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine("inserted id="+result);
            return new OkObjectResult(result);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]User body)
        {
            await Db.Connection.OpenAsync();
            var query = new User(Db);
            body.password = BCrypt.Net.BCrypt.HashPassword(body.password);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.username = body.username;
            result.password = body.password;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new User(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }


        public Database Db { get; }
    }
}
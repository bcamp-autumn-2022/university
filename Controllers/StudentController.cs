using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        public StudentController(Database db)
        {
            Db = db;
        }

        // GET api/Student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Student(Db);
            var result = await query.GetAllAsync();
            Console.WriteLine("Test");
            return new OkObjectResult(result);
        }

        // GET api/Student/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Student(Db);
            var result = await query.FindOneAsync(id);
            Console.WriteLine(result);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
            
        }

        // POST api/Student
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Student body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine(body.idstudent);
            Console.WriteLine(body.start_date);
            Console.WriteLine(body.graduate_date);
            if(result == 0){
                return new ConflictObjectResult(0);
            }
            return new OkObjectResult(result);
        }

        // PUT api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Student body)
        {
            await Db.Connection.OpenAsync();
            var query = new Student(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.idstudent = body.idstudent;
            result.start_date = body.start_date;
            result.graduate_date = body.graduate_date;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Student(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }


        public Database Db { get; }
    }
}
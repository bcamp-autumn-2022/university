using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        public TeacherController(Database db)
        {
            Db = db;
        }

        // GET api/Teacher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Teacher(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/Teacher/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Teacher(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Teacher
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Teacher body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine("inserted id="+result);
            if(result == 0){
                return new ConflictObjectResult(0);
            }
            return new OkObjectResult(result);
        }

        // PUT api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Teacher body)
        {
            await Db.Connection.OpenAsync();
            var query = new Teacher(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.iddepartment = body.iddepartment;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }
        

        // DELETE api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Teacher(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}
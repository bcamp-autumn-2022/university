using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        public CourseController(Database db)
        {
            Db = db;
        }

        // GET api/course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/course
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result = await body.InsertAsync();
            //Console.WriteLine(body.date);
            if (result == 1)
            {
                return new OkObjectResult(result);
            }
            else {
                return new ConflictObjectResult(result);
            }

        }

        // PUT api/course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Course body)
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.idcourse = body.idcourse;
            result.name = body.name;
            result.greditpoints = body.greditpoints;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Course(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}
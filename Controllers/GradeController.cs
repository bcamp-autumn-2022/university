using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        public GradeController(Database db)
        {
            Db = db;
        }

        // GET api/grade
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Grade(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET api/grade/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Grade(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/grade
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Grade body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine(body.date);
            return new OkObjectResult(result);
        }

        // PUT api/grade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Grade body)
        {
            await Db.Connection.OpenAsync();
            var query = new Grade(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.date = body.date;
            result.idstudent = body.idstudent;
            result.idteacher = body.idteacher;
            result.idcourse=body.idcourse;
            result.grade=body.grade;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/grade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Grade(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}
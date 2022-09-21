using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        public DepartmentController(Database db)
        {
            Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Department(Db);
            var result = await query.GetAllColumns();
            return new OkObjectResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Department(Db);
            var result = await query.FindOneColumn(id);
            if (result is null) return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Department body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result = await body.InsertAsync();
            return new OkObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Department body)
        {
            await Db.Connection.OpenAsync();
            var query = new Department(Db);
            var result = await query.FindOneColumn(id);
            if (result is null) return new NotFoundResult();
            result.name = body.name;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Department(Db);
            var result = await query.FindOneColumn(id);
            if (result is null) return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }

        public Database Db { get; }
    }
}
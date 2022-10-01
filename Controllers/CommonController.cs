using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        public CommonController(Database db)
        {
            Db = db;
        }

        // GET api/common/student
        [HttpGet("student")]
        public async Task<IActionResult> GetStudentData()
        {
            await Db.Connection.OpenAsync();
            var query = new Common(Db);
            var result = await query.GetAllStudentsAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetOneStudentData()
        {
            await Db.Connection.OpenAsync();
            var query = new Common(Db);
            var result = await query.GetOneStudentAsync(id);
            return new OkObjectResult(result);
        }
       

        public Database Db { get; }
    }
}
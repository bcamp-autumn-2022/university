using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
     [BasicAuthorization]
    [Route("api/[controller]")]
    public class StudentdataController : ControllerBase
    {
        public StudentdataController(Database db)
        {
            Db = db;
        }

        // GET api/Studentdata/student
        [HttpGet("student")]
        public async Task<IActionResult> GetStudentData()
        {
            await Db.Connection.OpenAsync();
            var query = new Studentdata(Db);
            var result = await query.GetAllStudentsAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("student/{username}")]
        public async Task<IActionResult> GetOneStudentData(string username)
        {
            await Db.Connection.OpenAsync();
            var query = new Studentdata(Db);
            var result = await query.GetOneStudentAsync(username);
            return new OkObjectResult(result);
        }    


        [HttpGet("grade")]
        public async Task<IActionResult> GetStudentGrades()
        {
            await Db.Connection.OpenAsync();
            var query = new Studentgrade(Db);
            var result = await query.GetStudentGrades();
            return new OkObjectResult(result);
        } 
        [HttpGet("grade/{username}")]
        public async Task<IActionResult> GetOneStudentGrades(string username)
        {
            await Db.Connection.OpenAsync();
            var query = new Studentgrade(Db);
            var result = await query.GetOneStudentGrades(username);
            return new OkObjectResult(result);
        }   
        public Database Db { get; }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
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

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetOneStudentData(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Studentdata(Db);
            var result = await query.GetOneStudentAsync(id);
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
        [HttpGet("grade/{id}")]
        public async Task<IActionResult> GetOneStudentGrades(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Studentgrade(Db);
            var result = await query.GetOneStudentGrades(id);
            return new OkObjectResult(result);
        }   
        public Database Db { get; }
    }
}
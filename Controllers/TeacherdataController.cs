using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class TeacherdataController : ControllerBase
    {
        public TeacherdataController(Database db)
        {
            Db = db;
        }

        // GET api/teacherdata/teacher
        [HttpGet("teacher")]
        public async Task<IActionResult> GetTeacherData()
        {
            await Db.Connection.OpenAsync();
            var query = new Teacherdata(Db);
            var result = await query.GetAllTeachersAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("teacher/{id}")]
        public async Task<IActionResult> GetOneTeacherData(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Teacherdata(Db);
            var result = await query.GetOneTeacherAsync(id);
            return new OkObjectResult(result);
        }    


        [HttpGet("course")]
        public async Task<IActionResult> GetTeacherCourses()
        {
            await Db.Connection.OpenAsync();
            var query = new Teachercourse(Db);
            var result = await query.GetAllTeacherCoursesAsync();
            return new OkObjectResult(result);
        } 
        [HttpGet("course/{id}")]
        public async Task<IActionResult> GetOneTeacherCourses(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Teachercourse(Db);
            var result = await query.GetOneTeacherCoursesAsync(id);
            return new OkObjectResult(result);
        }  

         
        public Database Db { get; }
    }
}
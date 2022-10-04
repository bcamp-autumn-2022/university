using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        public IdentityController(Database db)
        {
            Db = db;
        }         

        // GET api/Identity/5
        [HttpGet("{username}")]
        public async Task<IActionResult> GetIdentity(string username)
        {
            await Db.Connection.OpenAsync();
            var query = new Identity(Db);
            string result = await query.GetUserIdentity(username);
            Console.WriteLine(result);
            //if (result is null)
            //    return new NotFoundResult();
            return new OkObjectResult(result);
            
        }

    

        public Database Db { get; }
    }
}
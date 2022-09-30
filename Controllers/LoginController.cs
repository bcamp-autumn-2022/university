using Microsoft.AspNetCore.Mvc;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public LoginController(Database db)
        {
            Db = db;
        }


        // POST api/login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User body)
        {
            Console.WriteLine(body.username);
            Console.WriteLine(body.password);
            await Db.Connection.OpenAsync();
            var query = new Login(Db);
            var passwordFromTheDtabase = await query.GetPassword(body.username);
   
            if (passwordFromTheDtabase is null || ! BCrypt.Net.BCrypt.Verify(body.password, passwordFromTheDtabase))
            {
                // authentication failed
                return new OkObjectResult(false);
            }
            else
            {
                // authentication successful
                Singleton objectSingleton = Singleton.Instance;
                objectSingleton.Username=body.username;
                objectSingleton.Password=body.password;
                Console.WriteLine("set : "+objectSingleton.Username);
                return new OkObjectResult(true);
            }
            
        }

    

        public Database Db { get; }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        public AdministratorController(Database db)
        {
            Db = db;
        }

        // GET api/Administrator
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Db.Connection.OpenAsync();
            var query = new Administrator(Db);
            var result = await query.GetAllAsync();
            Console.WriteLine("Test");
            return new OkObjectResult(result);
        }

        // GET api/Administrator/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Administrator(Db);
            var result = await query.FindOneAsync(id);
            Console.WriteLine(result);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
            
        }

        // POST api/Administrator
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Administrator body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            int result=await body.InsertAsync();
            Console.WriteLine("Test Post");
            Console.WriteLine("inserted id="+result);
            if(result == 0){
                return new ConflictObjectResult(0);
            }
            return new OkObjectResult(result);
        }

        // PUT api/Administrator/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Administrator body)
        {
            Console.WriteLine("actual id="+id);
            Console.WriteLine("new id="+body.idadministrator);
            Console.WriteLine("new category="+body.category);
            await Db.Connection.OpenAsync();
            var query = new Administrator(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.idadministrator = body.idadministrator;
            result.category = body.category;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Administrator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new Administrator(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkObjectResult(result);
        }


        public Database Db { get; }
    }
}
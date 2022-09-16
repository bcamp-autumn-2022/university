using Microsoft.AspNetCore.Mvc;
using university.Models;

namespace Exercise9.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet()]
        public string GetStudents(){
            //Student objectStudent=new Student();
            //string results=objectStudent.GetAllStudents();
            Student objStudent=new Student(); 
            string result=objStudent.GetAllStudents();
            Console.WriteLine(result);
            return result;
        }
        [HttpGet("{id}")]
        public string GetOneStudent(int id){
  
            return "This will return student which id="+id;;
        }
        [HttpPost()]
        public string AddStudent(){
            return "This will add new student";
        }
        [HttpPut("{id}")]
        public string UpdateStudent(int id){
            return "This will update student which  id="+id;
        }
        
        [HttpDelete("{id}")]
        public string DeleteStudent(int id){
            return "This will delete student which  id="+id;
        }

    }
}
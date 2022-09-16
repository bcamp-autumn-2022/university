using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exercise9;

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
            return "This will return all students";
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
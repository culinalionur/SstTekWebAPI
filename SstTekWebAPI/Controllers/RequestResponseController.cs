using Microsoft.AspNetCore.Mvc;
using SstTekWebAPI.Linq;
using SstTekWebAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace SstTekWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RequestResponseController : ControllerBase
    {
        [HttpGet]
        [Route("student/{number}")]

        public JsonResult GetStudent(int number)
        {
            Database db = new Database();
            var student = db.Students.FirstOrDefault(f => f.Number == number);
            var response = new ResponseModel
            {
                StatusCode = 200,
                Message = "Success",
                Data = student
            };
        return new JsonResult(response);
        }
        [HttpGet]
        [Route("students")]

        public JsonResult CreateStudent([FromBody]CreateStudentRequest request)
        {
            Database db = new Database();

            var maxNumber = db.Students.MaxBy(m => m.Number)!.Number;
            var newNumber = maxNumber + 1;

            var newStudent = new Student
            {
                Number = newNumber,
                Name = request.Name,
                Surname = request.Surname,
                Height = request.Height,
                Color = request.Color,
                Rank = request.Rank
            };
            db.Students.Add(newStudent);

            var response = new ResponseModel
            {
                StatusCode = 200,
                Message = "Success",
                Data = newStudent

            };
            return new JsonResult(response);
        }
        [HttpPut]
        [Route("student")]

        public JsonResult UpdateStudent([FromBody]UpdateStudentRequest request) 
        {
            Database db = new Database();

            var student = db.Students.FirstOrDefault(s => s.Number == request.Number);
            if (student == null)
            {
                var errorResponse = new ResponseModel
                {
                    StatusCode = 200,
                    Message = "Not Found",
                    Data = null
                };
                return new JsonResult(errorResponse);
            }

            student.Color = request.Color;
            student.Rank = request.Rank;

            var response = new ResponseModel
            {
                StatusCode = 200,
                Message = "Success",
                Data = student

            };
            return new JsonResult(response);
        }
        [HttpDelete]
        [Route("student/{number}")]

        public JsonResult DeleteStudent(int number)
        {
            Database db = new Database();

            var student = db.Students.FirstOrDefault(s => s.Number == number);
            if (student == null)
            {
                var errorResponse = new ResponseModel
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Data = null
                };
                return new JsonResult(errorResponse);
            }
            db.Students.Remove(student);
            var response = new ResponseModel
            {
                StatusCode = 200,
                Message = "Success",
                Data = db.Students
            };
            return new JsonResult(response);
        }
    }
}

using FirstWebEmpty.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebEmpty.Controllers
{
    public class StudentController : Controller
    {

        List<Student> students;
        public StudentController()
        {
            students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    Name = "Rustem",
                    Age = DateTime.Now.AddYears(-24),
                },
                 new Student()
                {
                    Id = 2,
                    Name = "Inci",
                    Age = DateTime.Now.AddYears(-25),
                },

                 new Student()
                {
                    Id = 3,
                    Name = "Rena",
                    Age = DateTime.Now.AddYears(-25),
                },
                 new Student()
                {
                    Id = 4,
                    Name = "Selman",
                    Age = DateTime.Now.AddYears(-25),
                },
                 new Student()
                {
                    Id = 5,
                    Name = "Miri",
                    Age = DateTime.Now.AddYears(-25),
                },
                 new Student()
                {
                    Id = 6,
                    Name = "Gulu",
                    Age = DateTime.Now.AddYears(-24),
                },
            };
        }

        //
       
        //public IActionResult GetAll() 
        //{
        //    JsonResult json = new JsonResult(students);
        //    return json;
        //}

        public IActionResult GetAll()
        {
            return View(students);
        }

        //public JsonResult GetById(int id)
        //{
        //     var student = students.FirstOrDefault(s=>s.Id == id);
        //    if (student is null)
        //    {
        //        return new JsonResult(new { error = 404, errormesage = "Not found" });
        //    }
        //    var jsonresul = new JsonResult(student);
        //    return jsonresul;
        //}

        public IActionResult GetById(int id)
        {

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        public IActionResult Update()
        {
            ////////
            ////////
            ////////
            ////////
            ////////
            ///
            return RedirectToAction("getall", "student");
            //return RedirectToAction(nameof(GetAll));

        }
    }
}

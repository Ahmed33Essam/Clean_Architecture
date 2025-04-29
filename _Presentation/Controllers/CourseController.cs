using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse course;
        private readonly IMapper mapper;

        public CourseController(ICourse course, IMapper mapper)
        {
            this.course = course;
            this.mapper = mapper;
        }

        [HttpGet("ShowCourses")]
        public IActionResult GetAll()
        {
            return Ok(course.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Course res = course.GetByID(id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost("AddCourse")]
        public IActionResult Add([FromBody] CourseAddDTO dto)
        {
            var newCourse = mapper.Map<Course>(dto);

            course.Add(newCourse);
            course.Save();
            return Ok();
        }

        [HttpPut("UpdateCourse")]
        public IActionResult Update([FromBody] CourseUpdateDTO dto)
        {
            var existingCourse = course.GetByID(dto.Id);
            if (existingCourse == null)
                return NotFound();

            var updatedCourse = mapper.Map<Course>(dto);

            course.Update(updatedCourse);
            course.Save();
            return Ok();
        }


        [HttpDelete("DeleteCourse/{id}")]
        public IActionResult Delete(int id)
        {
            Course res = course.GetByID(id);
            if (course == null)
                return NotFound();

            course.Remove(id);
            course.Save();
            return Ok();
        }
    }
}
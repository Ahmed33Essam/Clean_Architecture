using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRate _ratingRepo;
        private readonly ICourse courseRepo;
        private readonly IUser<Instructor> userRepo;

        public RatingController(IRate ratingRepo, ICourse courseRepo, IUser<Instructor> userRepo)
        {
            _ratingRepo = ratingRepo;
            this.courseRepo = courseRepo;
            this.userRepo = userRepo;
        }

        [HttpPost("Add")]
        public IActionResult AddRating(Rate rating)
        {
            _ratingRepo.AddRating(rating);
            _ratingRepo.Save();
            var avgRate = _ratingRepo.GetAverageRating(rating.TargetId, rating.TargetType);
            if(rating.TargetType == "Course")
            {
                var course = courseRepo.GetByID(rating.TargetId);
                course.Rate = avgRate;
                courseRepo.Update(course);
                courseRepo.Save();
            }
            else if (rating.TargetType == "Instructor")
            {
                var instructor = userRepo.GetByID(rating.TargetId);
                instructor.Rate = avgRate;
                userRepo.Update(instructor);
                userRepo.Save();
            }

            return Ok(avgRate);
        }
    }
}

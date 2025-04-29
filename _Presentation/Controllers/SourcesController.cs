using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly ICourse course;
        private readonly EduQuestContext context;

        public SourcesController(ICourse course, EduQuestContext context)
        {
            this.course = course;
            this.context = context;
        }


        // POST: api/course/{id}/upload-video
        [HttpPost("{id}/upload-video")]
        public IActionResult UploadVideo(int id, IFormFile videoFile)
        {
            var ucourse = course.GetByID(id);
            if (ucourse == null)
                return NotFound("Course not found.");

            if (videoFile == null || videoFile.Length == 0)
                return BadRequest("Invalid video file.");

            // Save the video file to a directory
            var filePath = Path.Combine("wwwroot/videos", videoFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                videoFile.CopyTo(stream);
            }

            // Get video duration using MediaToolkit
            var inputFile = new MediaFile { Filename = filePath };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile); // Extract metadata FIRST
            }

            TimeSpan duration = inputFile.Metadata.Duration;
            string formattedDuration = $"{duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}";

            // Create Video entity
            var video = new Video
            {
                VideoUrl = $"/videos/{videoFile.FileName}",
                Duration = formattedDuration,  // Save duration in HH:MM:SS format
                CourseId = id
            };

            VideoRepo videoRepo = new VideoRepo(context);
            videoRepo.Add(video);
            videoRepo.save();

            // Add video ID to the course
            ucourse.Videos ??= new List<int>();
            ucourse.Videos.Add(video.Id);
            course.Save();

            return Ok("Video uploaded successfully.");
        }

        // POST: api/course/{id}/upload-document
        [HttpPost("{id}/upload-document")]
        public IActionResult UploadDocument(int id, IFormFile documentFile)
        {
            var ucourse = course.GetByID(id);
            if (ucourse == null)
                return NotFound("Course not found.");

            if (documentFile == null || documentFile.Length == 0)
                return BadRequest("Invalid document file.");

            // Simulate file saving
            var filePath = Path.Combine("wwwroot/documents", documentFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                documentFile.CopyTo(stream);
            }

            var document = new Document
            {
                DocumentUrl = $"/documents/{documentFile.FileName}",
                CourseId = id
            };

            ucourse.Documents ??= new List<int>();
            ucourse.Documents.Add(document.Id);
            course.Save();

            DocumentRepo documentRepo = new DocumentRepo(context);
            documentRepo.Add(document);
            documentRepo.Save();

            return Ok("Document uploaded successfully.");
        }

        // POST: api/course/{id}/upload-image
        [HttpPost("{id}/upload-image")]
        public IActionResult UploadImage(int id, IFormFile imageFile)
        {
            var ucourse = course.GetByID(id);
            if (ucourse == null)
                return NotFound("Course not found.");

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Invalid image file.");

            // Check if an image already exists for this course
            var existingImage = context.Images.FirstOrDefault(i => i.CourseId == id);
            if (existingImage != null)
            {
                return BadRequest("An image already exists for this course.");
            }

            // Save the file locally
            var filePath = Path.Combine("wwwroot/images", imageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            var image = new Image
            {
                ImagetUrl = $"/images/{imageFile.FileName}",
                CourseId = id
            };

            ucourse.Image = image;
            course.Save();

            ImageRepo imageRepo = new ImageRepo(context);
            imageRepo.Add(image);
            imageRepo.save();

            return Ok("Image uploaded successfully.");
        }

        [HttpGet("video/{id}")]
        public IActionResult GetVideoById(int id)
        {
            // Retrieve the video from the database
            Video? video = context.Videos.FirstOrDefault(v => v.Id == id);

            if (video == null)
            {
                return NotFound("Video not found.");
            }

            // Combine the base directory with the relative URL from the database
            var videoDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var filePath = Path.Combine(videoDirectory, video.VideoUrl.TrimStart('/')); // Trim the leading slash if present

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Video with ID {id} not found at: {filePath}");
            }

            try
            {
                var videoStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(videoStream, "video/mp4"); // Return the video stream with appropriate MIME type
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("image/{courseId}")]
        public IActionResult GetImageByCourseId(int courseId)
        {
            var image = context.Images.FirstOrDefault(i => i.CourseId == courseId);
            if (image == null)
                return NotFound("Image not found.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImagetUrl.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("Image file not found on server.");

            try
            {
                var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(imageStream, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("document/{id}")]
        public IActionResult GetDocumentById(int id)
        {
            var document = context.Documents.FirstOrDefault(d => d.Id == id);
            if (document == null)
                return NotFound("Document not found.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.DocumentUrl.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("Document file not found on server.");

            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "application/octet-stream", Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ICourse course;
        private readonly EduQuestContext context;
        private readonly IQuestionBank questionBankRepo;
        private readonly IQuiz quizRepo;
        private readonly IMapper mapper;

        public QuizController(ICourse course, EduQuestContext context, IQuestionBank questionBankRepo, IQuiz quizRepo, IMapper mapper)
        {
            this.course = course;
            this.context = context;
            this.questionBankRepo = questionBankRepo;
            this.quizRepo = quizRepo;
            this.mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] QuizCreateDTO dto)
        {
            if (dto == null) return BadRequest("Quiz data is required.");
            var quiz = mapper.Map<Quiz>(dto);
            quizRepo.Add(quiz);
            quizRepo.Save();
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            quizRepo.Remove(id);
            quizRepo.Save();
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] QuizUpdateDTO dto)
        {
            var quiz = mapper.Map<Quiz>(dto);
            quizRepo.Update(quiz);
            quizRepo.Save();
            return Ok();
        }


        [HttpGet("GetByCourse/{id}")]
        public IActionResult GetByCourse(int id)
        {
            Course ucourse = course.GetByID(id);
            if (ucourse == null)
                return NotFound("Course not found.");

            List<Quiz> quizzes = new List<Quiz>();
            if (ucourse.Quizzs != null)
            {
                foreach (int quizId in ucourse.Quizzs)
                {
                    Quiz quiz = quizRepo.GetByID(quizId);
                    if (quiz != null)
                        quizzes.Add(quiz);
                }
            }

            return Ok(quizzes);
        }

        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] QuestionCreateDTO dto)
        {
            var question = mapper.Map<Question>(dto);
            quizRepo.AddQuestion(question.QuizId, question);
            quizRepo.Save();

            var quiz = quizRepo.GetByID(dto.QuizId);
            questionBankRepo.AddQuestion(question.Id, quiz.Subject);
            questionBankRepo.Save();

            return Ok();
        }



        [HttpDelete("DeleteQuestion/Quiz/{quizId}/Question/{questionId}")]
        public IActionResult DeleteQuestion(int quizId, int questionId)
        {
            quizRepo.RemoveQuestion(quizId, questionId);
            quizRepo.Save();
            return Ok();
        }

        [HttpPost("EditQuestion")]
        public IActionResult EditQuestion([FromBody] QuestionUpdateDTO dto)
        {
            var question = mapper.Map<Question>(dto);
            quizRepo.EditQuestion(question);
            quizRepo.Save();
            return Ok();
        }


        [HttpGet("GetQuestionsByQuiz/{id}")]
        public IActionResult GetQuestion(int id)
        {
            id = quizRepo.GetByID(id).Id;
            if (id == 0)
                return NotFound("Quiz not found.");
            return Ok(context.Questions.Where(q => q.QuizId == id).ToList());
        }


        [HttpPost("SubmitQuiz")]
        public IActionResult SubmitQuiz([FromBody] QuizSubmissionDTO submission)
        {
            var quiz = quizRepo.GetByID(submission.QuizId);
            if (quiz == null) return NotFound("Quiz not found.");

            var correctAnswers = 0;

            foreach (var answer in submission.Answers)
            {
                var question = context.Questions.FirstOrDefault(q => q.Id == answer.QuestionId && q.QuizId == quiz.Id);
                if (question == null) continue;

                if (string.Equals(question.RightOne.Trim(), answer.SelectedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    correctAnswers++;
                }
            }

            var totalQuestions = submission.Answers.Count;
            double percentage = totalQuestions > 0 ? (correctAnswers * 100.0) / totalQuestions : 0;

            var result = new QuizResultDTO
            {
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                ScorePercentage = Math.Round(percentage, 2)
            };

            return Ok(result);
        }
    }
}

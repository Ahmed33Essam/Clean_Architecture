using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBankController : ControllerBase
    {
        private readonly EduQuestContext context;
        private readonly IQuestionBank questionBankRepo;

        public QuestionBankController(EduQuestContext context, IQuestionBank questionBankRepo)
        {
            this.context = context;
            this.questionBankRepo = questionBankRepo;
        }

        [HttpGet("GetAll")]
        public IActionResult GetNames()
        {
            return Ok(questionBankRepo.GetAll());
        }

        [HttpGet("{subject}")]
        public IActionResult GetBySubject(string subject)
        {
            return Ok(questionBankRepo.getQuestions(subject));
        }

        [HttpDelete("Question/{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            var questionBank = questionBankRepo.GetByID(id);
            if (questionBank == null)
            {
                return NotFound();
            }
            questionBankRepo.Remove(id);
            questionBankRepo.Save();
            return NoContent();
        }
    }
}

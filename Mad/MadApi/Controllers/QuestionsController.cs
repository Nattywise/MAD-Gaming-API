using System;
using System.Threading.Tasks;
using MadDataAccess;
using MadDataAccess.Model;
using MadDataAccess.ModelCustom;
using Microsoft.AspNetCore.Mvc;

namespace MadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {

        // GET api/questions
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hi, I am the Question API :)";
        }

        [HttpPut]
        public ActionResult<string> Put(Question question)
        {
            SimpleResponse simpleResponse = new SimpleResponse();

            try
            {
                CompetitionQuestion competitionQuestion = CompetitionQuestionUtils.RetrieveUsingCompetitionIdAndQuestionId(WebHelper.ConnectionString(), question.CompetitionId, question.QuestionId);
                if (competitionQuestion == null)
                {
                    simpleResponse.Meta = new Meta("400", "Failed to retrieve CompetitionQuestion using CompetitionId: " + question.CompetitionId + " and QuestionId: " + question.QuestionId);
                }
                else
                {
                    if (competitionQuestion.AnswerProvided != 0)
                    {
                        simpleResponse.Meta = new Meta("400", "Answer already submitted for Question");
                    }
                    else
                    {
                        competitionQuestion.AnswerProvided = question.UserOption;
                        competitionQuestion.TimeTaken = question.TimeTaken;
                        competitionQuestion.AnswerIsCorrect = false;
                        switch (question.UserOption)
                        {
                            case 1:
                                competitionQuestion.AnswerIsCorrect = Convert.ToBoolean(question.Option1IsCorrect);
                                break;
                            case 2:
                                competitionQuestion.AnswerIsCorrect = Convert.ToBoolean(question.Option2IsCorrect);
                                break;
                            case 3:
                                competitionQuestion.AnswerIsCorrect = Convert.ToBoolean(question.Option3IsCorrect);
                                break;
                            case 4:
                                competitionQuestion.AnswerIsCorrect = Convert.ToBoolean(question.Option4IsCorrect);
                                break;
                            default:
                                break;
                        }

                        if (!(bool)competitionQuestion.AnswerIsCorrect)
                        {
                            simpleResponse.Meta = new Meta("400", "Answer is incorrect!");
                            competitionQuestion.Score = 0;
                        }
                        else
                        {
                            competitionQuestion.CalculateScore();
                            simpleResponse.Meta = new Meta("201", "Your answer was correct and your scored " + competitionQuestion.Score + " as you answered in " + question.TimeTaken + " seconds");
                        }

                        if (!CompetitionQuestionUtils.Save(WebHelper.ConnectionString(), competitionQuestion))
                        {
                            simpleResponse.Meta = new Meta("400", "Failed to save Answer!");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                simpleResponse.Meta.Message = exception.Message;
            }

            return MadJson(simpleResponse);
        }
    }
}
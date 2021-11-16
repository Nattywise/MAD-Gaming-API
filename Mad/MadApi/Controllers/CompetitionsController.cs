using MadDataAccess.Model;
using MadDataAccess.ModelCustom;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : BaseController
    {

        // GET api/competitions
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hi, I am the competition API :)";
        }

        [HttpGet]
        public ActionResult<string> Get(int genreId)
        {
            SimpleResponse simpleResponse = new SimpleResponse();

            try
            {
                IdName idName = CompetitionUtils.Create(WebHelper.ConnectionString(), genreId);
                if (idName.Id > 0)
                {
                    int competitionId = idName.Id;

                    simpleResponse.Meta.Code = "201";

                    List<CompetitionQuestion> competitionQuestionList = CompetitionQuestionUtils.RetrieveUsingCompetitionId(WebHelper.ConnectionString(), competitionId);
                    List<Question> questionList = new List<Question>();
                    foreach (CompetitionQuestion competitionQuestion in competitionQuestionList)
                    {
                        Question question = QuestionUtils.Retrieve(WebHelper.ConnectionString(), competitionQuestion.QuestionId);
                        if (question != null)
                        {
                            question.CompetitionId = competitionId;
                            questionList.Add(question);
                        }
                    }

                    simpleResponse.Data = JsonConvert.SerializeObject(questionList);

                }
                else
                {
                    simpleResponse.Meta = new Meta("400", idName.Name);
                }
            }
            catch (Exception exception)
            {
                simpleResponse.Meta = new Meta("401", exception.Message);

            }

            return MadJson(simpleResponse);
        }

        [HttpGet]
        public ActionResult<string> GetScore(int competitionId)
        {
            SimpleResponse simpleResponse = new SimpleResponse();

            try
            {
                List<CompetitionQuestion> competitionQuestionList = CompetitionQuestionUtils.RetrieveUsingCompetitionId(WebHelper.ConnectionString(), competitionId);
                if (competitionQuestionList.Count > 0)
                {
                    int score = Convert.ToInt32(competitionQuestionList.Sum(x => x.Score));
                    simpleResponse.Meta.Code = "201";
                    simpleResponse.Data = "Your score is " + score + " out of 500.";
                }
                else
                {
                    simpleResponse.Meta = new Meta("400", "Unable to retrieve score for Competition");
                }
            }
            catch (Exception exception)
            {
                simpleResponse.Meta = new Meta("401", exception.Message);
            }

            return MadJson(simpleResponse);
        }
    }
}
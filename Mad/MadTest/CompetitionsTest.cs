using MadApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MadUtils.Extensions;
using MadDataAccess.ModelCustom;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MadDataAccess.Model;
using System.Collections.Generic;
using MadUtils;
using System;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MadTest
{
    [TestClass]
    public class CompetitionsTest
    {
        [TestMethod]
        public void TestPost()
        {
            int competitionId = 0;
            StringBuilder stringBuilder = new StringBuilder();

            GenresController genresController = new GenresController();

            ActionResult<string> actionResultGenresGet = genresController.Get();

            Assert.IsNotNull(actionResultGenresGet);

            SimpleResponse simpleResponseGenresGet = JsonConvert.DeserializeObject<SimpleResponse>(actionResultGenresGet.Value);

            if (simpleResponseGenresGet.Meta.Code == "201")
            {
                List<Genre> genreList = JsonConvert.DeserializeObject<List<Genre>>(simpleResponseGenresGet.Data);
                Assert.IsNotNull(genreList);

                Assert.AreEqual(3, genreList.Count);
                stringBuilder.AppendLine("Choose as Genre");
                foreach (Genre genre in genreList)
                {
                    stringBuilder.AppendLine("Option: " + genre.Description);
                }

                Genre genreSelected = genreList[CommonUtils.RandomNumber(0, 2)];
                Assert.IsNotNull(genreSelected);
                stringBuilder.AppendLine("You choose Genre: " + genreSelected.Description);
                stringBuilder.AppendLine("--------");

                CompetitionsController competitionsController = new CompetitionsController();
                ActionResult<string> actionResultCompetitionsGet = competitionsController.Get(genreSelected.GenreId);
                Assert.IsNotNull(actionResultCompetitionsGet);

                SimpleResponse simpleResponseCompetitionsGet = JsonConvert.DeserializeObject<SimpleResponse>(actionResultCompetitionsGet.Value);
                if (simpleResponseCompetitionsGet.Meta.Code == "201")
                {
                    List<Question> questionList = JsonConvert.DeserializeObject<List<Question>>(simpleResponseCompetitionsGet.Data);
                    foreach (Question question in questionList)
                    {
                        stringBuilder.AppendLine(question.QuestionPhrase);
                        stringBuilder.AppendLine("Option 1: " + question.Option1);
                        stringBuilder.AppendLine("Option 2: " + question.Option2);
                        stringBuilder.AppendLine("Option 3: " + question.Option3);
                        stringBuilder.AppendLine("Option 4: " + question.Option4);

                        competitionId = question.CompetitionId;

                        int userOption = CommonUtils.RandomNumber(1, 4);
                        question.UserOption = userOption;
                        stringBuilder.AppendLine("You choose Option: " + question.UserOption);

                        int timeTaken = CommonUtils.RandomNumber(10, 60);
                        question.TimeTaken = timeTaken;
                        stringBuilder.AppendLine("You took: " + question.TimeTaken + " seconds to answer.");

                        ///Immediately respond if question is incorrect
                        ///Or send back to the controller


                        QuestionsController questionsController = new QuestionsController();
                        ActionResult<string> actionResultQuestionsPut = questionsController.Put(question);
                        Assert.IsNotNull(actionResultQuestionsPut);

                        SimpleResponse simpleResponseQuestionsPut = JsonConvert.DeserializeObject<SimpleResponse>(actionResultQuestionsPut.Value);

                        stringBuilder.AppendLine(simpleResponseQuestionsPut.Meta.Message);
                        stringBuilder.AppendLine("--------");
                    }
                }


                ActionResult<string> actionResultCompetitionsGetScore = competitionsController.GetScore(competitionId);
                Assert.IsNotNull(actionResultCompetitionsGetScore);

                SimpleResponse simpleResponseCompetitionsGetScore = JsonConvert.DeserializeObject<SimpleResponse>(actionResultCompetitionsGetScore.Value);
                stringBuilder.AppendLine("--------");
                stringBuilder.AppendLine(simpleResponseCompetitionsGetScore.Data);
                stringBuilder.AppendLine("--------");

                string fileName = @"Competition_Printout_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                File.WriteAllText(fileName, stringBuilder.ToString());
                Process.Start(@"c:\Program Files (x86)\Notepad++\notepad++.exe", fileName);

            }
        }
    }
}
;
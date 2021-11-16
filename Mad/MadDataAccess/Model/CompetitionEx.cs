using MadDataAccess.ModelCustom;
using MadUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadDataAccess.Model
{
    public partial class Competition
    {
    }

    public partial class CompetitionUtils
    {
        /// <summary>
        /// Retrieve a Competition using CompetitionAlternateId
        /// </summary>
        /// <param name="madContext"></param>
        /// <returns></returns>
        public static Competition RetrieveUsingAlternateId(string connectionString, string competitionAlternateId)
        {
            Competition competition = null;

            try
            {
                if (competitionAlternateId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = "SELECT * FROM Competition WHERE CompetitionAlternateId = '" + competitionAlternateId + "'";
                        competition = madContext.Competition.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve Competition using CompetitionAlternateId: " + competitionAlternateId, exception);
            }

            return competition;

        }


        public static IdName Create(string connectionString, int? genreId)
        {
            IdName idName = new IdName() { Id = 0 };

            try
            {
                Competition competition = new Competition()
                {
                    GenreId = Convert.ToInt32(genreId),
                };

                bool competitionSaved = Save(connectionString, competition);
                if (!competitionSaved)
                {
                    return idName;
                }

                List<Question> questionList = QuestionUtils.Retrieve(connectionString)
                                                           .Where(questionDB => questionDB.GenreId == genreId)
                                                           .ToList();

                foreach (Question question in questionList)
                {
                    CompetitionQuestion competitionQuestion = new CompetitionQuestion()
                    {
                        CompetitionId = competition.CompetitionId,
                        QuestionId = question.QuestionId,
                        Score = 0,
                        TimeTaken = 0,
                        AnswerProvided = 0,
                    };

                    bool competitionQuestionSaved = CompetitionQuestionUtils.Save(connectionString, competitionQuestion);
                    if (!competitionQuestionSaved)
                    {
                        return idName;
                    }

                }

                idName = new IdName() { Id = competition.CompetitionId };
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to Create Competition", exception);
            }

            return idName;
        }

        /// <summary>
        /// Saves a Competition
        /// </summary>
        /// <param name="rbmWeighBridgeContext"></param>
        /// <param name="userViewModel"></param>
        /// <param name="competition"></param>
        /// <returns></returns>
        public static bool Save(string connectionString, Competition competition)
        {
            bool result = false;
            int? competitionId = null;
            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {
                    if (competition.CompetitionId == 0)
                    {
                        madContext.Competition.Add(competition);
                    }
                    else
                    {
                        competitionId = competition.CompetitionId;
                        madContext.Competition.Update(competition);
                    }

                    madContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to save Competition", exception);
            }

            return result;
        }
    }
}

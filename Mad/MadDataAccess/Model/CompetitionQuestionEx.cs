using MadDataAccess.ModelCustom;
using MadUtils;
using MadUtils.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadDataAccess.Model
{
    public partial class CompetitionQuestion
    {
        public void CalculateScore()
        {
            Score = 0;

            if (TimeTaken <= 10)
            {
                Score = 100;
            }
            else if (TimeTaken <= 20)
            {
                Score = 80;
            }
            else if (TimeTaken <= 30)
            {
                Score = 60;
            }
            else if (TimeTaken <= 60)
            {
                Score = 40;
            }
            else if (TimeTaken > 60)
            {
                Score = 10;
            }

        }
    }

    public partial class CompetitionQuestionUtils
    {
        /// <summary>
        /// Retrieve a CompetitionQuestion using CompetitionQuestionId
        /// </summary>
        /// <param name="madContext"></param>
        /// <returns></returns>
        public static CompetitionQuestion Retrieve(string connectionString, int? competitionQuestionId)
        {
            CompetitionQuestion competitionQuestion = null;

            try
            {
                if (competitionQuestionId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = "SELECT * FROM CompetitionQuestion WHERE CompetitionQuestionId = " + competitionQuestionId;
                        competitionQuestion = madContext.CompetitionQuestion.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve CompetitionQuestion using CompetitionQuestionId: " + competitionQuestionId, exception);
            }

            return competitionQuestion;

        }

        public static CompetitionQuestion RetrieveNext(string connectionString, int? competitionId)
        {
            CompetitionQuestion competitionQuestion = null;

            try
            {
                if (competitionId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = new StringBuilder()
                            .AppendFormat(@"SELECT TOP 1 * 
                                            FROM CompetitionQuestion 
                                            WHERE CompetitionId = {0}
                                                AND DateRequested IS NULL 
                                            ORDER BY [INDEX]", competitionId).ToString();

                        competitionQuestion = madContext.CompetitionQuestion.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve next CompetitionQuestion using CompetitionId: " + competitionId, exception);
            }

            return competitionQuestion;

        }

        public static List<CompetitionQuestion> RetrieveUsingCompetitionId(string connectionString, int? competitionId)
        {
            List<CompetitionQuestion> competitionQuestionList = new List<CompetitionQuestion>();

            try
            {
                if (competitionId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = new StringBuilder()
                            .AppendFormat(@"SELECT * 
                                            FROM CompetitionQuestion 
                                            WHERE CompetitionId = {0}", competitionId).ToString();

                        competitionQuestionList = madContext.CompetitionQuestion.FromSql(sqlScript).ToList();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve CompetitionQuestions using CompetitionId: " + competitionId, exception);
            }

            return competitionQuestionList;
        }

        public static CompetitionQuestion RetrieveUsingCompetitionIdAndQuestionId(string connectionString, int? competitionId, int? questionId)
        {
            CompetitionQuestion competitionQuestion = null;

            try
            {
                if (competitionId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = new StringBuilder()
                            .AppendFormat(@"SELECT * 
                                            FROM CompetitionQuestion 
                                            WHERE CompetitionId = {0}
                                                AND QuestionId  = {1}", competitionId, questionId).ToString();

                        competitionQuestion = madContext.CompetitionQuestion.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve CompetitionQuestion using CompetitionId: " + competitionId + " and QuestionId: " + questionId, exception);
            }

            return competitionQuestion;
        }



        /// <summary>
        /// Saves a CompetitionQuestion
        /// </summary>
        /// <param name="rbmWeighBridgeContext"></param>
        /// <param name="userViewModel"></param>
        /// <param name="competitionQuestion"></param>
        /// <returns></returns>
        public static bool Save(string connectionString, CompetitionQuestion competitionQuestion)
        {
            bool result = false;
            int? competitionQuestionId = null;
            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {

                    if (competitionQuestion.CompetitionQuestionId == 0)
                    {
                        madContext.CompetitionQuestion.Add(competitionQuestion);
                    }
                    else
                    {
                        competitionQuestionId = competitionQuestion.CompetitionQuestionId;
                        madContext.CompetitionQuestion.Update(competitionQuestion);
                    }

                    madContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to save CompetitionQuestion", exception);
            }

            return result;
        }
    }
}

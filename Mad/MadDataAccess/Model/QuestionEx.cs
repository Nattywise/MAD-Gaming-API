using MadDataAccess.ModelCustom;
using MadUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadDataAccess.Model
{
    public partial class Question
    {
        [NotMapped]
        public int CompetitionId { get; set; }

        [NotMapped]
        public int UserOption { get; set; }

        [NotMapped]

        public int TimeTaken { get; set; }



    }

    public partial class QuestionUtils
    {
        /// <summary>
        /// Retrieves all Questions
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Question> Retrieve(string connectionString)
        {
            List<Question> questionList = new List<Question>();

            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {
                    questionList = madContext.Question.ToList();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve Questions", exception);
            }

            return questionList;

        }

        /// <summary>
        /// Retrieve a Question using QuestionId
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Question Retrieve(string connectionString, int? questionId)
        {
            Question question = null;

            try
            {
                if (questionId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = "SELECT * FROM Question WHERE QuestionId = " + questionId;

                        question = madContext.Question.Where(questions => questions.QuestionId == questionId).FirstOrDefault();

                        //question = madContext.Question.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve Question using QuestionId: " + questionId, exception);
            }

            return question;

        }

        /// <summary>
        /// Saves a Question
        /// </summary>
        /// <param name="rbmWeighBridgeContext"></param>
        /// <param name="userViewModel"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool Save(string connectionString, Question question)
        {
            bool result = false;
            int? questionId = null;
            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {

                    if (question.QuestionId == 0)
                    {
                        madContext.Question.Add(question);
                    }
                    else
                    {
                        questionId = question.QuestionId;
                        madContext.Question.Update(question);
                    }

                    madContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to save Question", exception);
            }

            return result;
        }
    }
}

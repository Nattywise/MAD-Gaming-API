using System;
using System.Collections.Generic;

namespace MadApi.Model
{
    public partial class CompetitionQuestion
    {
        public int CompetitionQuestionId { get; set; }
        public int CompetitionId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerProvided { get; set; }
        public bool? AnswerIsCorrect { get; set; }
        public int? TimeTaken { get; set; }
        public int? Score { get; set; }

        public virtual Competition Competition { get; set; }
        public virtual Question Question { get; set; }
    }
}

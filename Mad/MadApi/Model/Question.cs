using System;
using System.Collections.Generic;

namespace MadApi.Model
{
    public partial class Question
    {
        public Question()
        {
            CompetitionQuestion = new HashSet<CompetitionQuestion>();
        }

        public int QuestionId { get; set; }
        public string QuestionPhrase { get; set; }
        public int? GenreId { get; set; }
        public string Option1 { get; set; }
        public bool? Option1IsCorrect { get; set; }
        public string Option2 { get; set; }
        public bool? Option2IsCorrect { get; set; }
        public string Option3 { get; set; }
        public bool? Option3IsCorrect { get; set; }
        public string Option4 { get; set; }
        public bool? Option4IsCorrect { get; set; }
        public int? TimeAllowance { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<CompetitionQuestion> CompetitionQuestion { get; set; }
    }
}

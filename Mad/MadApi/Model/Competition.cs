using System;
using System.Collections.Generic;

namespace MadApi.Model
{
    public partial class Competition
    {
        public Competition()
        {
            CompetitionQuestion = new HashSet<CompetitionQuestion>();
        }

        public int CompetitionId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<CompetitionQuestion> CompetitionQuestion { get; set; }
    }
}

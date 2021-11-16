using System;
using System.Collections.Generic;

namespace MadDataAccess.Model
{
    public partial class Genre
    {
        public Genre()
        {
            Competition = new HashSet<Competition>();
            Question = new HashSet<Question>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Competition> Competition { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}

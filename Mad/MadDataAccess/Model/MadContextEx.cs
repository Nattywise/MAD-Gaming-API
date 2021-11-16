using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadDataAccess.Model
{
    public partial class MadContext
    {
        #region Constructor
        public MadContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

    }
}

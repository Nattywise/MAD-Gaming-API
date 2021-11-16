

using MadDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace MadDataAccess
{
    public class DataAccessUtils
    {
        // /// <summary>
        ///// Returns the Number of Records Saved
        ///// </summary>
        ///// <param name="madContext"></param>
        ///// <returns></returns>
        // public static Task<int> GetResultCount(MadContext madContext)
        // {
        //     return madContext.SaveChangesAsync();
        // }

        public static MadContext MadContextCreate(string connectionString)
        {
            MadContext madContext = null;

            try
            {

                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlServer(connectionString,
                                            options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                                                              .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                                           );

                madContext = new MadContext(optionsBuilder.Options);
                madContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to MadContextCreate", exception);

            }
            return madContext;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadDataAccess.Model
{
    public partial class Genre
    {
    }

    public partial class GenreUtils
    {
        /// <summary>
        /// Retrieve a Genre
        /// </summary>
        /// <param name="madContext"></param>
        /// <returns></returns>
        public static List<Genre> Retrieve(string connectionString)
        {
            List<Genre> genreList = new List<Genre>();

            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {
                    genreList = madContext.Genre.ToList();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve Genres", exception);
            }

            return genreList;

        }

        /// <summary>
        /// Retrieve a Genre using GenreId
        /// </summary>
        /// <param name="madContext"></param>
        /// <returns></returns>
        public static Genre Retrieve(string connectionString, int? genreId)
        {
            Genre genre = null;

            try
            {
                if (genreId != null)
                {
                    using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                    {
                        string sqlScript = "SELECT * FROM Genre WHERE GenreId = " + genreId;
                        genre = madContext.Genre.FromSql(sqlScript).FirstOrDefault();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to retrieve Genre using GenreId: " + genreId, exception);
            }

            return genre;

        }

        /// <summary>
        /// Saves a Genre
        /// </summary>
        /// <param name="rbmWeighBridgeContext"></param>
        /// <param name="userViewModel"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        public static bool Save(string connectionString, Genre genre)
        {
            bool result = false;
            int? genreId = null;
            try
            {
                using (MadContext madContext = DataAccessUtils.MadContextCreate(connectionString))
                {

                    if (genre.GenreId == 0)
                    {
                        madContext.Genre.Add(genre);
                    }
                    else
                    {
                        genreId = genre.GenreId;
                        madContext.Genre.Update(genre);
                    }

                    madContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to save Genre", exception);
            }

            return result;
        }
    }
}

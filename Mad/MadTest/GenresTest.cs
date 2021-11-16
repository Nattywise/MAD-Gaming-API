using MadApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MadUtils.Extensions;
using MadDataAccess.ModelCustom;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using MadDataAccess.Model;

namespace MadTest
{
    [TestClass]
    public class GenresTest
    {
        [TestMethod]
        public void TestGenreList()
        {
            GenresController genresController = new GenresController();

            ActionResult<string> actionResult = genresController.Get();

            Assert.IsNotNull(actionResult);

            SimpleResponse simpleResponse = JsonConvert.DeserializeObject<SimpleResponse>(actionResult.Value);

            if (simpleResponse.Meta.Code == "201")
            {
                List<Genre> genreList = JsonConvert.DeserializeObject<List<Genre>>(simpleResponse.Data);
                Assert.IsNotNull(genreList);

                Assert.AreEqual(3, genreList.Count);
            }
        }
    }
}

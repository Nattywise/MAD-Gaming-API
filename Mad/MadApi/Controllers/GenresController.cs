using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadDataAccess.Model;
using MadDataAccess.ModelCustom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BaseController
    {

        // GET api/competitions
        [HttpGet]
        public ActionResult<string> Get()
        {
            SimpleResponse simpleResponse = new SimpleResponse();

            try
            {
                List<Genre> genreList = GenreUtils.Retrieve(WebHelper.ConnectionString());
                simpleResponse.Meta.Code = genreList.Count > 0 ? "201" : "400";
                simpleResponse.Data = JsonConvert.SerializeObject(genreList);
            }
            catch (Exception exception)
            {
                simpleResponse.Meta.Message = exception.Message;
            }

            return MadJson(simpleResponse);
        }
    }
}
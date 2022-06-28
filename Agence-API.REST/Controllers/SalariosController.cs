using Agence_API.REST.Application;
using Agence_API.REST.Domain.EntitiesDto;
using System.Collections.Generic;
using System.Web.Http;

namespace Agence_API.REST.Controllers
{
    [RoutePrefix("api/salarios")]
    public class SalariosController : ApiController
    {
        // GET api/salarios
        [HttpGet]
        public List<SalarioDto> GetAll()
        {
            var result = SalarioApplication.GetAll();
            return result;
        }
    }
}

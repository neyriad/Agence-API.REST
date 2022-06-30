using Agence_API.REST.Application;
using Agence_API.REST.Domain.EntitiesDto;
using Agence_API.REST.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Agence_API.REST.Controllers
{
    [RoutePrefix("api/salary")]
    public class SalaryController : ApiController
    {
        // GET api/salarios
        [HttpGet]
        public List<SalaryDto> GetAll()
        {
            try
            {
                var result = SalaryApplication.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, "Error getting all salary", 
                    $"Details: {ex.Message}");
            }
        }
    }
}

using Agence_API.REST.Application;
using Agence_API.REST.Domain.EntitiesDto;
using Agence_API.REST.Domain.Helpers;
using Agence_API.REST.Domain.ModelView;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Agence_API.REST.Controllers
{
    [RoutePrefix("api/performance")]
    public class PerformanceController : ApiController
    {
        [HttpGet]
        [Route("consultants")]
        public List<ConsultantDto> GetAllConsultores(SearchParamsDto options)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                    ModelState));

            try
            {
                return PerformanceApplication.GetAllConsultants(options);
            }
            catch (Exception ex)
            {
                throw ExceptionBuilder.Create(HttpStatusCode.NotFound, "Error getting all users", 
                    $"Details: {ex.Message}");
            }
        }
    }
}

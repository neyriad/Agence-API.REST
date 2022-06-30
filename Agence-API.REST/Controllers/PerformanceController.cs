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
        // GET api/performance/consultants
        [Route("consultants")]
        public List<ConsultantDto> GetAllConsultants()
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                    ModelState));

            try
            {
                return PerformanceApplication.GetAllConsultants();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, 
                    "Error getting all consultants", 
                    $"Details: {ex.Message}");
            }
        }

        // GET api/performance/earnings
        [Route("earnings")]
        public List<EarningsDto> GetEarningsByConsultant(RequestRangeParams param)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    ModelState));

            try
            {
                return PerformanceApplication.GetEarningsByConsultant(param);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, 
                    $"Error getting all earning by user {param.User}",
                    $"Details: {ex.Message}");
            }
        }

        // GET api/performance/bymonth
        [Route("bymonth")]
        public PerformanceYearDto GetPerformanceByConsultantAndYear(RequestInYearParams param)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    ModelState));

            if(param.InitMonth > param.EndMonth)
                throw ExceptionHelper.Create(HttpStatusCode.NotFound,
                    $"Error getting performance by user {param.User} and and year {param.Year}",
                    $"Initial month value cannot be greater than end month value");

            try
            {
                return PerformanceApplication.GetPerformanceByConsultantAndYear(param);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, 
                    $"Error getting performance by user {param.User} and and year {param.Year}",
                    $"Details: {ex.Message}");
            }
        }
    }
}

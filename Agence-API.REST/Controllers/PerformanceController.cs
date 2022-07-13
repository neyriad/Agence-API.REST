using Agence_API.REST.Application;
using Agence_API.REST.Domain.EntitiesDto;
using Agence_API.REST.Domain.Helpers;
using Agence_API.REST.Domain.ModelView;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Agence_API.REST.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        // POST api/performance/earnings
        [Route("earnings")]
        [HttpPost]
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

        // POST api/performance/month_range
        [Route("month_range")]
        [HttpPost]
        public PerformanceYearDto GetPerformanceByConsultantAndRange(RequestRangeParams param)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    ModelState));

            if(param.InitDate > param.EndDate)
                throw ExceptionHelper.Create(HttpStatusCode.NotFound,
                    $"Error getting performance by user {param.User}",
                    $"Initial month value cannot be greater than end month value");

            try
            {
                return PerformanceApplication.GetPerformanceByConsultantAndRange(param);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, 
                    $"Error getting performance by user {param.User}",
                    $"Details: {ex.Message}");
            }
        }

        // POST api/performance/percentage
        [Route("percentage")]
        [HttpPost]
        public PerformanceYearDto GetPerformanceByConsultantForPercentage(RequestRangeParams param)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    ModelState));

            if(param.InitDate > param.EndDate)
                throw ExceptionHelper.Create(HttpStatusCode.NotFound,
                    $"Error getting performance by user {param.User}",
                    $"Initial month value cannot be greater than end month value");

            try
            {
                return PerformanceApplication.GetPerformanceForPercentage(param);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.Create(HttpStatusCode.NotFound, 
                    $"Error getting performance by user {param.User}",
                    $"Details: {ex.Message}");
            }
        }
    }
}

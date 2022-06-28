using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Agence_API.REST.Domain.Helpers
{
    public static class ExceptionBuilder
    {
        public static HttpResponseException Create(HttpStatusCode code, string reason, string content)
        {
            return new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = code,
                ReasonPhrase = reason,
                Content = new StringContent(content)
            });
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.ModelView
{
    [DataContract]
    public class SearchParamsDto
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        [Range(5, 25, ErrorMessage = "Page size defined out of range.")]
        public int PageSize { get; set; }

        public SearchParamsDto()
        {
            PageIndex = 0;
            PageSize = 10;
        }
    }
}
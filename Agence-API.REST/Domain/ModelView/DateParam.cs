using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.ModelView
{
    [DataContract]
    public class DateParam
    {
        [DataMember]
        [Range(1, 12, ErrorMessage = "Month value out of range")]
        public int Month { get; set; }

        [DataMember]
        [Range(1950, 2022, ErrorMessage = "Year value out of range")]
        public int Year { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.ModelView
{
    [DataContract]
    public class RequestInYearParams
    {
        [DataMember]
        [Required(ErrorMessage = "User name cannot be empty")]
        public string User { get; set; }

        [DataMember]
        [Range(1, 12, ErrorMessage = "Initial month value out of range")]
        public int InitMonth { get; set; }

        [DataMember]
        [Range(1, 12, ErrorMessage = "End month value out of range")]
        public int EndMonth { get; set; }

        [DataMember]
        [Range(1950, 2022, ErrorMessage = "Year value out of range")]
        public int Year { get; set; }
    }
}
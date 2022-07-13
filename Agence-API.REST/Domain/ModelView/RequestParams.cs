using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.ModelView
{
    [DataContract]
    public class RequestRangeParams
    {
        [DataMember]
        [Required(ErrorMessage = "User name cannot be empty")]
        public string User { get; set; }

        [DataMember]
        [Required]
        public DateTime InitDate { get; set; }

        [DataMember]
        [Required]
        public DateTime EndDate { get; set; }
    }
}
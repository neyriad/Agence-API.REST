using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.EntitiesDto
{
    [DataContract]
    public class PerformanceYearDto
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public List<string> MonthList { get; set; }

        [DataMember]
        public List<double> ValueList { get; set; }

        [DataMember]
        public double TotalPerformance { get; set; }

        [DataMember]
        public double FixedCost { get; set; }
    }
}
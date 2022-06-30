using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.EntitiesDto
{
    [DataContract]
    public class EarningsDto
    {
        [DataMember]
        public double NetEarning { get; set; }

        [DataMember]
        public double FixedCost { get; set; }

        [DataMember]
        public double Commission { get; set; }

        [DataMember]
        public double Profit { get; set; }
    }
}
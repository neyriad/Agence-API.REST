using System;

namespace Agence_API.REST.Domain.EntitiesDto
{
    public class BillingDto
    {
        public int BillId { get; set; }

        public double BillValue { get; set; }

        public DateTime BillDate { get; set; }

        /// <summary>
        /// Percentage value
        /// </summary>
        public double Commission { get; set; }

        /// <summary>
        /// Percentage value
        /// </summary>
        public double Taxes { get; set; }
    }
}
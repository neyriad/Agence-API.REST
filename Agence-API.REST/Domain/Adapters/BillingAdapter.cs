using Agence_API.REST.Domain.EntitiesDto;
using System.Data.Common;

namespace Agence_API.REST.Domain.Adapters
{
    public static class BillingAdapter
    {
        public static BillingDto BusinessEntityForEarnings(DbDataReader entity)
        {
            if (null == entity) return null;

            return new BillingDto
            {
                BillId = entity.IsDBNull(entity.GetOrdinal("BillId"))
                                    ? default : entity.GetInt32(entity.GetOrdinal("BillId")),

                BillValue = entity.IsDBNull(entity.GetOrdinal("BillValue"))
                                    ? default : entity.GetDouble(entity.GetOrdinal("BillValue")),

                Commission = entity.IsDBNull(entity.GetOrdinal("Commission"))
                                    ? default : entity.GetDouble(entity.GetOrdinal("Commission")),

                Taxes = entity.IsDBNull(entity.GetOrdinal("Taxes"))
                                    ? default : entity.GetDouble(entity.GetOrdinal("Taxes"))
            };
        }

        public static BillingDto BusinessEntityForPerformance(DbDataReader entity)
        {
            if (null == entity) return null;

            return new BillingDto
            {
                BillValue = entity.IsDBNull(entity.GetOrdinal("BillValue"))
                                    ? default : entity.GetDouble(entity.GetOrdinal("BillValue")),

                BillDate = entity.IsDBNull(entity.GetOrdinal("BillDate"))
                                    ? default : entity.GetDateTime(entity.GetOrdinal("BillDate")),

                Taxes = entity.IsDBNull(entity.GetOrdinal("Taxes"))
                                    ? default : entity.GetDouble(entity.GetOrdinal("Taxes"))
            };
        }
    }
}
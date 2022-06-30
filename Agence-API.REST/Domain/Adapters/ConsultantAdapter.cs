using Agence_API.REST.Domain.EntitiesDto;
using System.Data.Common;

namespace Agence_API.REST.Domain.Adapters
{
    public static class ConsultantAdapter
    {
        public static ConsultantDto ToBusinessEntity(DbDataReader entity)
        {
            if (null == entity) return null;

            return new ConsultantDto
            {
                Name = entity.IsDBNull(entity.GetOrdinal("Name"))
                                    ? default : entity.GetString(entity.GetOrdinal("Name")),

                EmailJob = entity.IsDBNull(entity.GetOrdinal("EmailJob"))
                                    ? default : entity.GetString(entity.GetOrdinal("EmailJob")),

                EmailPrivate = entity.IsDBNull(entity.GetOrdinal("EmailPrivate"))
                                    ? default : entity.GetString(entity.GetOrdinal("EmailPrivate")),

                Phone = entity.IsDBNull(entity.GetOrdinal("Phone"))
                                    ? default : entity.GetString(entity.GetOrdinal("Phone")),

                UserName = entity.IsDBNull(entity.GetOrdinal("UserName"))
                                    ? default : entity.GetString(entity.GetOrdinal("UserName"))
            };
        }
    }
}
using Agence_API.REST.Domain.DataAccess;
using Agence_API.REST.Domain.EntitiesDto;
using System.Collections.Generic;
using System.Linq;

namespace Agence_API.REST.Application
{
    public static class SalarioApplication
    {
        public static List<SalarioDto> GetAll()
        {
            var list = EntitiesDataAccess.Instance.cao_salario.ToList();
            return list.Select(s => SalarioDto.ToBusinessEntity(s)).ToList();
        }
    }
}
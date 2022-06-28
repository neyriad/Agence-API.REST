using Agence_API.REST.Domain.Models;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.EntitiesDto
{
    [DataContract]
    public class SalaryDto
    {
        [DataMember]
        // Usuario
        public string User { get; set; }

        [DataMember]
        // Alteracao
        public System.DateTime Alteration { get; set; }

        [DataMember]
        // Salario bruto
        public double GrossSalary { get; set; }

        [DataMember]
        // Salario Liquido
        public double LiquidSalary { get; set; }


        public static SalaryDto ToBusinessEntity(cao_salario entity)
        {
            if (null == entity) return null;
            return new SalaryDto
            {
                User = entity.co_usuario,
                Alteration = entity.dt_alteracao,
                GrossSalary = entity.brut_salario,
                LiquidSalary = entity.liq_salario
            };
        }
    }
}
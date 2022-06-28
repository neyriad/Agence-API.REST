using Agence_API.REST.Domain.Models;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.EntitiesDto
{
    [DataContract]
    public class SalarioDto
    {
        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public System.DateTime Alteracao { get; set; }

        [DataMember]
        public double SalarioBruto { get; set; }

        [DataMember]
        public double SalarioLiquido { get; set; }


        public static SalarioDto ToBusinessEntity(cao_salario entity)
        {
            if (null == entity) return null;
            return new SalarioDto
            {
                Usuario = entity.co_usuario,
                Alteracao = entity.dt_alteracao,
                SalarioBruto = entity.brut_salario,
                SalarioLiquido = entity.liq_salario
            };
        }
    }
}
using Agence_API.REST.Domain.Models;
using System.Runtime.Serialization;

namespace Agence_API.REST.Domain.EntitiesDto
{
    [DataContract]
    public class ConsultantDto
    {
        [DataMember]
        // Nombre de usuario
        public string Name { get; set; }

        [DataMember]
        //No_Email
        public string EmailJob { get; set; }
        
        [DataMember]
        // No_Email_Pessoal
        public string EmailPrivate { get; set; }

        [DataMember]
        // Nu_Telefone
        public string Phone { get; set; }

        public static ConsultantDto ToBusinessEntity(cao_usuario entity)
        {
            if (null == entity) return null;

            return new ConsultantDto
            {
                Name = entity.no_usuario,
                EmailJob = entity.no_email,
                EmailPrivate = entity.no_email_pessoal,
                Phone = entity.nu_telefone
            };
        }
    }
}
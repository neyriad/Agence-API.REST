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

        [DataMember]
        // Nu_Telefone
        public string UserName { get; set; }
    }
}
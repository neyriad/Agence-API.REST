//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agence_API.REST.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class permissao_sistema
    {
        public string co_usuario { get; set; }
        public long co_tipo_usuario { get; set; }
        public long co_sistema { get; set; }
        public string in_ativo { get; set; }
        public string co_usuario_atualizacao { get; set; }
        public System.DateTime dt_atualizacao { get; set; }
    }
}
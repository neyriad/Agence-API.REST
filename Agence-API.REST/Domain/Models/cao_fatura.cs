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
    
    public partial class cao_fatura
    {
        public int co_fatura { get; set; }
        public int co_cliente { get; set; }
        public int co_sistema { get; set; }
        public int co_os { get; set; }
        public int num_nf { get; set; }
        public double total { get; set; }
        public double valor { get; set; }
        public System.DateTime data_emissao { get; set; }
        public string corpo_nf { get; set; }
        public double comissao_cn { get; set; }
        public double total_imp_inc { get; set; }
    }
}
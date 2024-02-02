using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class Cargo
    {
        private int _CodCargo;
        private string? _NomeCargo;
        private double _SalarioBase;
        private int _PorcentagemComissao;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCargo { get{return _CodCargo;} set{_CodCargo = value;} }

        [Required]
        [MaxLength(30)]
        public string? NomeCargo { get{return _NomeCargo;} set{if(value != null){_NomeCargo = value;}} }

        public double SalarioBase { get{return _SalarioBase;} set{_SalarioBase = value;} }
        
        public int PorcentagemComissao { get{return _PorcentagemComissao;} set{_PorcentagemComissao = value;} }
    }
}
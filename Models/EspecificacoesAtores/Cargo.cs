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
        private bool _CargoAtivo;
        private double _SalarioBase;
        private double _PorcentagemComissao;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCargo 
        { 
            get { return _CodCargo; } 
            set { _CodCargo = value; }
        }

        [Required]
        [MaxLength(30)]
        public string? NomeCargo 
        { 
            get { return _NomeCargo; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeCargo = value; } }
        }

        [Required]
        public bool CargoAtivo 
        { 
            get { return _CargoAtivo; } 
            set { _CargoAtivo = value; }
        }

        public double SalarioBase 
        { 
            get { return _SalarioBase; }
            set { _SalarioBase = value; }
        }

        public double PorcentagemComissao 
        { 
            get { return _PorcentagemComissao; } 
            set { _PorcentagemComissao = value; }
        }
    }
}
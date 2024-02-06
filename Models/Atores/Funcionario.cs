using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class Funcionario
    {
        private int _CodFuncionario;
        private string? _NomeFuncionario;
        private string? _CpfFuncionario;
        private bool _FuncionarioAtivo;
        private string? _NumeroContatoFuncionario;
        private int? _FkCargosCodCargo;
        private int _FkConcessionariasCodConc;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodFuncionario
        {
            get { return _CodFuncionario; }
            set { _CodFuncionario = value; }
        }

        [Required]
        [MaxLength(50)]
        public string? NomeFuncionario
        {
            get { return _NomeFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeFuncionario = value; } }
        }

        [Required]
        [MaxLength(20)]
        public string? CpfFuncionario
        {
            get { return _CpfFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CpfFuncionario = value; } }
        }

        [Required]
        public bool FuncionarioAtivo
        {
            get { return _FuncionarioAtivo; }
            set { _FuncionarioAtivo = value; }
        }

        [Required]
        [MaxLength(25)]
        public string? NumeroContatoFuncionario
        {
            get { return _NumeroContatoFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NumeroContatoFuncionario = value; } }
        }

        [Required]
        [ForeignKey("Cargos")]
        public int? FkCargosCodCargo
        {
            get { return _FkCargosCodCargo; }
            set { _FkCargosCodCargo = value; }
        }

        [ForeignKey("Concessionarias")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}

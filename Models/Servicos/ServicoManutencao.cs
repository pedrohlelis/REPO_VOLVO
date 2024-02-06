using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    public class ServicoManutencao
    {
        private int _CodManutencao;
        private DateOnly _DataManutencao;
        private double _ValorServicoManutencao;
        private double _QuilometragemCaminhao;
        private string? _DescricaoManutencao;
        private int _FkConcessionariasCodConc;
        private int _FkFuncionariosCodFuncionario;
        private int _FkCaminhoesCodCaminhaoEstoque;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodManutencao 
        { 
            get { return _CodManutencao; } 
            set { _CodManutencao = value; } 
        }

        [Required]
        public DateOnly DataManutencao 
        { 
            get { return _DataManutencao; } 
            set { _DataManutencao = value; } 
        }

        [Required]
        public double ValorServicoManutencao 
        { 
            get { return _ValorServicoManutencao; } 
            set { _ValorServicoManutencao = value; } 
        }

        [Required]
        public double QuilometragemCaminhao 
        { 
            get { return _QuilometragemCaminhao; } 
            set { _QuilometragemCaminhao = value; } 
        }

        [Required]
        [MaxLength(250)]
        public string? DescricaoManutencao 
        { 
            get { return _DescricaoManutencao; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _DescricaoManutencao = value; } } 
        }

        [Required]
        [ForeignKey("CodConc")]
        public int FkConcessionariasCodConc 
        { 
            get { return _FkConcessionariasCodConc; } 
            set { _FkConcessionariasCodConc = value; } 
        }

        [Required]
        [ForeignKey("CodFuncionario")]
        public int FkFuncionariosCodFuncionario 
        { 
            get { return _FkFuncionariosCodFuncionario; } 
            set { _FkFuncionariosCodFuncionario = value; } 
        }

        [Required]
        [ForeignKey("CodCaminhao")]
        public int FkCaminhoesCodCaminhao 
        { 
            get { return _FkCaminhoesCodCaminhaoEstoque; } 
            set { _FkCaminhoesCodCaminhaoEstoque = value; } 
        }
    }
}
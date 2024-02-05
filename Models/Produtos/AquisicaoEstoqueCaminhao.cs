using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class AquisicaoEstoqueCaminhao
    {
        private int _CodAquisicao;
        private string? _CorAquisicaoEstoque;
        private string? _CodChassiAquisicaoEstoque;
        private DateTime _DataHora;
        private int _FkModelosCodModelo;
        private int _FkConcessionariasCodConc;
        
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodAquisicao 
        { 
            get { return _CodAquisicao; } 
            set { _CodAquisicao = value; }
        }

        [Required]
        public string? CorAquisicaoEstoque
        {
            get { return _CorAquisicaoEstoque; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _CorAquisicaoEstoque = value; } }
        }

        [Required]
        public string? CodChassiAquisicaoEstoque
        {
            get { return _CodChassiAquisicaoEstoque; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _CodChassiAquisicaoEstoque = value; } }
        }

        [Required]
        [MaxLength(30)]
        public DateTime DataHora 
        { 
            get { return _DataHora; } 
            set { _DataHora = value; }
        }

        [Required]
        [ForeignKey("CodModelo")]
        public int FkModelosCodModelo 
        { 
            get { return _FkModelosCodModelo; } 
            set { _FkModelosCodModelo = value; } 
        }

        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}
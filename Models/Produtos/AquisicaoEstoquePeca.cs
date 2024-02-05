using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class AquisicaoEstoquePeca
    {
        private int _CodAquisicao;
        private DateTime _DataHora;
        private DateOnly _DataFabPecas;
        private int _FkTiposPecaCodTipoPeca;
        private int _FkConcessionariasCodConc;
        private int _Quantidade;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodAquisicao 
        { 
            get { return _CodAquisicao; } 
            set { _CodAquisicao = value; }
        }

        [Required]
        public DateTime DataHora 
        { 
            get { return _DataHora; } 
            set { _DataHora = value; }
        }

        [Required]
        public DateOnly DataFabPecas 
        { 
            get { return _DataFabPecas; } 
            set { _DataFabPecas = value; }
        }

        [Required]
        [ForeignKey("TiposPeca")]
        public int FkTiposPecaCodTipoPeca 
        { 
            get { return _FkTiposPecaCodTipoPeca; } 
            set { _FkTiposPecaCodTipoPeca = value; } 
        }
        public int Quantidade 
        { 
            get { return _Quantidade; } 
            set { _Quantidade = value; } 
        }

        [Required]
        [ForeignKey("CodConc")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}
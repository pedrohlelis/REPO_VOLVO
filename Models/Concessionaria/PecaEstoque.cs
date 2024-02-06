using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class PecaEstoque
    {
        private int _CodPecaEstoque;
        private DateOnly _DataFabricacao;
        private int _FkTiposPecaCodTipoPeca;
        private int _FkConcessionariasCodConc;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodPecaEstoque 
        { 
            get { return _CodPecaEstoque; } 
            set { _CodPecaEstoque = value; } 
        }

        [Required]
        public DateOnly DataFabricacao 
        { 
            get { return _DataFabricacao; } 
            set { _DataFabricacao = value; } 
        }

        [Required]
        [ForeignKey("FK_EstoquePecas_TiposPeca")]
        public int FkTiposPecaCodTipoPeca 
        { 
            get { return _FkTiposPecaCodTipoPeca; } 
            set { _FkTiposPecaCodTipoPeca = value; } 
        }

        [Required]
        [ForeignKey("FK_EstoquePecas_Concessionarias")]
        public int FkConcessionariasCodConc 
        { 
            get { return _FkConcessionariasCodConc; } 
            set { _FkConcessionariasCodConc = value; } 
        }
    }
}
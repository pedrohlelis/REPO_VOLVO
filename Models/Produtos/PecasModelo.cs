using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class PecasModelo
    {
        private int _CodPecasModelo;
        private int _FkModelosCaminhoesCodModelo;
        private int _FkTiposPecaCodTipoPeca;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodPecasModelo 
        {
            get { return _CodPecasModelo; }
            set { _CodPecasModelo = value; }
        }

        [Required]
        [ForeignKey("CodModelo")]
        public int FkModelosCaminhoesCodModelo 
        {
            get { return _FkModelosCaminhoesCodModelo; }
            set { _FkModelosCaminhoesCodModelo = value; }
        }

        [Required]
        [ForeignKey("CodTipoPeca")]
        public int FkTiposPecaCodTipoPeca 
        {
            get { return _FkTiposPecaCodTipoPeca; }
            set { _FkTiposPecaCodTipoPeca = value; } 
        }
    }
}
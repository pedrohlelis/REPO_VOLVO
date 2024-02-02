using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class ServicoTipoPeca
    {
        private int _CodServicoTipoPeca;
        private int _FkCodManutencao;
        private int _FkCodTipoPeca;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodServicoTipoPeca { get{return _CodServicoTipoPeca;} set{_CodServicoTipoPeca = value;} }

        [Required]
        [ForeignKey("CodManutencao")]
        public int FkCodManutencao { get{return _FkCodManutencao;} set{_FkCodManutencao = value;} }

        [Required]
        [ForeignKey("CodTipoPeca")]
        public int FkCodTipoPeca { get{return _FkCodTipoPeca;} set{_FkCodTipoPeca = value;} }
    }
}
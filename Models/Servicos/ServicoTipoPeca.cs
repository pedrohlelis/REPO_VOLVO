using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class ServicoTipoPeca
    {
        private int _CodServicoTipoPeca;
        private int _FkServicosManutencaoCodManutencao;
        private int _FkTiposPecaCodTipoPeca;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodServicoTipoPeca
        {
            get { return _CodServicoTipoPeca; }
            set { _CodServicoTipoPeca = value; }
        }

        [Required]
        [ForeignKey("FK_ServicoTiposPeca_ServicosManutencao")]
        public int FkCodManutencao
        {
            get { return _FkServicosManutencaoCodManutencao; }
            set { _FkServicosManutencaoCodManutencao = value; }
        }

        [Required]
        [ForeignKey("Fk_ServicoTiposPeca_TiposPeca")]
        public int FkTiposPecaCodTipoPeca
        {
            get { return _FkTiposPecaCodTipoPeca; }
            set { _FkTiposPecaCodTipoPeca = value; }
        }
    }
}
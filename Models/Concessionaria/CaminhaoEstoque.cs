using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class CaminhaoEstoque
    {
        // acho que adicionar numero de chassi seria importante, tanto aqui quando na classe Caminhao. adicionar cor tambem?
        private int _CodCaminhaoEstoque;
        private string? _CodChassiEstoque;
        private string? _CorEstoqueCaminhao;
        private DateOnly _DataFabricacao;
        private int _FkModelosCaminhoesCodModelo;
        private int _FkConcessionariasCodConc;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCaminhaoEstoque
        {
            get { return _CodCaminhaoEstoque; }
            set { _CodCaminhaoEstoque = value; }
        }

        [Required]
        public string? CodChassiEstoque
        {
            get { return _CodChassiEstoque; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CodChassiEstoque = value; } }
        }

        [Required]
        public string? CorEstoqueCaminhao
        {
            get { return _CorEstoqueCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CorEstoqueCaminhao = value; } }
        }
        [Required]
        public DateOnly DataFabricacao
        {
            get { return _DataFabricacao; }
            set { _DataFabricacao = value; }
        }

        [Required]
        [ForeignKey("FK_EstoqueCaminhao_ModelosCaminhoes")]
        public int FkModelosCaminhoesCodModelo
        {
            get { return _FkModelosCaminhoesCodModelo; }
            set { _FkModelosCaminhoesCodModelo = value; }
        }

        [Required]
        [ForeignKey("FK_EstoqueCaminhao_Concessionarias")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}
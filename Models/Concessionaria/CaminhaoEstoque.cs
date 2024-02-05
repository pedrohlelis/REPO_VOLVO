using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class CaminhaoEstoque
    {
        // acho que adicionar numero de chassi seria importante, tanto aqui quando na classe Caminhao. adicionar cor tambem?
        private int _CodCaminhaoEstoque;
        private int _FkModelosCaminhaoCodModelo;
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
        [ForeignKey("CodModelo")]
        public int FkModelosCaminhaoCodModelo 
        { 
            get { return _FkModelosCaminhaoCodModelo; } 
            set { _FkModelosCaminhaoCodModelo = value; } 
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
using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class Caminhao
    {
        private int _CodCaminhao;
        private double _Quilometragem;
        private DateOnly _DataFabricacao;
        private bool _CaminhaoAtivo;
        private int _FkClientesCodCliente;
        private int _FkModelosCaminhoesCodModelo;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCaminhao { get{return _CodCaminhao;} set{_CodCaminhao = value;} }

        [Required]
        public double Quilometragem { get{return _Quilometragem;} set{_Quilometragem = value;} }

        [Required]
        public DateOnly DataFabricacao { get{return _DataFabricacao;} set{_DataFabricacao = value;} }

        [Required]
        public bool CaminhaoAtivo { get{return _CaminhaoAtivo;} set{_CaminhaoAtivo = value;} }
        
        [Required]
        [ForeignKey("CodCliente")]
        //alterei de FkClientesClienteId para FkClientesCodCliente!!!
        public int FkClientesCodCliente { get{return _FkClientesCodCliente;} set{_FkClientesCodCliente = value;} }

        [Required]
        [ForeignKey("CodModelo")]
        //alterei de FkModelosCaminhoesModeloID para FkModelosCaminhoesCodModelo!!!
        public int FkModelosCaminhoesCodModelo { get{return _FkModelosCaminhoesCodModelo;} set{_FkModelosCaminhoesCodModelo = value;} }
    }
}
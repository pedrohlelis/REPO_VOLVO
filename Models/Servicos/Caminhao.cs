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
        private string? _PlacaCaminhao;
        private string? _CorCaminhao;
        private string? _CodChassiCaminhao;
        private DateOnly _DataFabricacao;
        private bool _CaminhaoAtivo;
        private int _FkClientesCodCliente;
        private int _FkModelosCaminhoesCodModelo;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCaminhao
        {
            get { return _CodCaminhao; }
            set { _CodCaminhao = value; }
        }

        [Required]
        public double Quilometragem
        {
            get { return _Quilometragem; }
            set { _Quilometragem = value; }
        }

        [Required]
        public string? PlacaCaminhao
        {
            get { return _PlacaCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _PlacaCaminhao = value; } }
        }

        [Required]
        public string? CorCaminhao
        {
            get { return _CorCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CorCaminhao = value; } }
        }

        [Required]
        public string? CodChassiCaminhao
        {
            get { return _CodChassiCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CodChassiCaminhao = value; } }
        }

        [Required]
        public DateOnly DataFabricacao
        {
            get { return _DataFabricacao; }
            set { _DataFabricacao = value; }
        }

        [Required]
        public bool CaminhaoAtivo
        {
            get { return _CaminhaoAtivo; }
            set { _CaminhaoAtivo = value; }
        }

        [Required]
        [ForeignKey("FK_Caminhoes_Clientes")]
        public int FkClientesCodCliente
        {
            get { return _FkClientesCodCliente; }
            set { _FkClientesCodCliente = value; }
        }

        [Required]
        [ForeignKey("FK_Caminhoes_ModelosCaminhao")]
        public int FkModelosCaminhoesCodModelo
        {
            get { return _FkModelosCaminhoesCodModelo; }
            set { _FkModelosCaminhoesCodModelo = value; }
        }
    }
}
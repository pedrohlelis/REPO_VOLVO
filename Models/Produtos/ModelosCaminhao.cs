using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class ModelosCaminhao
    {
        private int _CodModelo;
        private string? _NomeModelo;
        private bool _ModelosAtivo;
        private double _ValorModeloCaminhao;
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodModelo { get{return _CodModelo;} set{_CodModelo = value;} }

        [Required]
        [MaxLength(30)]
        public string? NomeModelo { get{return _NomeModelo;} set{if(value != null){_NomeModelo = value;}} }

        [Required]
        public bool ModelosAtivo { get{return _ModelosAtivo;} set{_ModelosAtivo = value;} }

        [Required]
        //alterei de ValorModelosCaminhao pra ValorModeloCaminhao!!!
        public double ValorModeloCaminhao { get{return _ValorModeloCaminhao;} set{_ValorModeloCaminhao = value;} }
    }
}
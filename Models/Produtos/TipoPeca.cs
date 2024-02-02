using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class TipoPeca
    {
        private int _CodTipoPeca;
        private string? _NomeTipoPeca;
        private bool _PecaAtivo;
        private double _ValorTipoPeca;
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodTipoPeca { get{return _CodTipoPeca;} set{_CodTipoPeca = value;} }

        [MaxLength(40)]
        public string? NomeTipoPeca { get{return _NomeTipoPeca;} set{if(value != null){_NomeTipoPeca = value;}} }

        [Required]
        public bool PecaAtivo { get{return _PecaAtivo;} set{_PecaAtivo = value;} }

        [Required]
        public double ValorTipoPeca { get{return _ValorTipoPeca;} set{_ValorTipoPeca = value;} }
    }
}
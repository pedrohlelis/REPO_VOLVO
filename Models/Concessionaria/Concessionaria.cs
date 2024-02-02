using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    public class Concessionaria
    {
        private int _CodConc;
        private string? _NomeConc;
        private string? _CepConcessionaria;
        private bool _ConcessionariaAtivo;
        private string? _Pais;
        private string? _Estado;
        private string? _Cidade;
        private string? _Rua;
        private int _Numero;

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodConc { get{return _CodConc;} set{_CodConc = value;} }

        [Required]
        [MaxLength(40)]
        public string? NomeConc { get{return _NomeConc;} set{if(value != null){_NomeConc = value;}} }

        [Required]
        [MaxLength(10)]
        public string? CepConcessionaria { get{return _CepConcessionaria;} set{if(value != null){_CepConcessionaria = value;}} }
        
        [Required]
        public bool ConcessionariaAtivo { get{return _ConcessionariaAtivo;} set{{_ConcessionariaAtivo = value;}}  }
        
        [Required]
        [MaxLength(30)]
        public string? Pais {get{return _Pais;} set{if(value != null){_Pais = value.ToUpper();}} }

        [Required]
        [MaxLength(30)]
        public string? Estado { get{return _Estado;} set{if(value != null){_Estado = value.ToUpper();}} }

        [Required]
        [MaxLength(30)]
        public string? Cidade { get{return _Cidade;} set{if(value != null){_Cidade = value.ToUpper();}} }

        [Required]
        [MaxLength(30)]
        public string? Rua { get{return _Rua;} set{if(value != null){_Rua = value;}} }

        [Required]
        [MaxLength(5)]
        public int Numero { get{return _Numero;} set{_Numero = value;} }
    }
}
using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace TRABALHO_VOLVO
{
    public class Cliente
    {
        private int _CodCliente;
        private string? _NomeCliente;
        private string? _DocIdentificador;
        private bool _ClienteAtivo;
        private string? _EmailCliente;
        private string? _NumeroContatoCliente;
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCliente { get{return _CodCliente;} set{_CodCliente = value;} }

        [Required]
        [MaxLength(50)]
        public string? NomeCliente { get{return _NomeCliente;} set{if(value != null){_NomeCliente = value;}} }
        
        [Required]
        [MaxLength(20)]
<<<<<<< HEAD
        public string? DocIdentificadorCliente { get{return _DocIdentificador;} set{if(value != null){_DocIdentificador = value;}} }

        [Required]
        public bool ClienteAtivo { get{return _ClienteAtivo;} set{_ClienteAtivo = value;} }
=======
        public string? DocIdentificadorCliente { get; set; } 
>>>>>>> cb1c4a445598c4c102b50ad09dadd4a7e27b4277
        
        [MaxLength(100)]
        public string? EmailCliente { get{return _EmailCliente;} set{if(value != null){_EmailCliente = value;}} }

        [Required]
        [MaxLength(25)]
        public string? NumeroContatoCliente { get{return _NumeroContatoCliente;} set{if(value != null){_NumeroContatoCliente = value;}}}
    }
}

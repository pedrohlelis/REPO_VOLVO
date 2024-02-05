using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    public class VendaCaminhao
    {
        private int _CodVenda;
        private DateOnly _DataVenda;
        private int _FkClientesCodCliente;
        private int _FkConcessionariasCodConc;
        private int _FkFuncionariosCodFuncionario;
        private int _FkEstoqueCaminhoesCodCaminhaoEstoque;
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodVenda 
        { 
            get { return _CodVenda; } 
            set { _CodVenda = value; } 
        }

        [Required]
        public DateOnly DataVenda 
        { 
            get { return _DataVenda; } 
            set { _DataVenda = value; } 
        }

        [Required]
        [ForeignKey("CodCliente")]
        public int FkClientesCodCliente 
        { 
            get { return _FkClientesCodCliente; } 
            set { _FkClientesCodCliente = value; } 
        }

        [Required]
        [ForeignKey("CodConc")]
        public int FkConcessionariasCodConc 
        { 
            get { return _FkConcessionariasCodConc; } 
            set { _FkConcessionariasCodConc = value; } 
        }

        [Required]
        [ForeignKey("CodFuncionario")]
        public int FkFuncionariosCodFuncionario 
        { 
            get { return _FkFuncionariosCodFuncionario; } 
            set { _FkFuncionariosCodFuncionario = value; } 
        }

        [Required]
        [ForeignKey("CodCaminhaoEstoque")]
        public int FkEstoqueCaminhoesCodCaminhaoEstoque 
        { 
            get { return _FkEstoqueCaminhoesCodCaminhaoEstoque; } 
            set { _FkEstoqueCaminhoesCodCaminhaoEstoque = value; } 
        }
    }
}
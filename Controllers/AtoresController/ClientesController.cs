using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {

        [HttpPost("Cadastrar")]
        public IActionResult PostCliente([FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                ValidationHelper.CheckUniqueDoc(_context, cliente.DocIdentificadorCliente);
                ValidationHelper.ValidateNameFormat(cliente.NomeCliente, "Nome invalido.");
                ValidationHelper.ValidateNumericFormat(cliente.DocIdentificadorCliente, "Formato do Documento Identificador invalido.");
                ValidationHelper.ValidateEmailFormat(cliente.EmailCliente, "Email invalido.");
                ValidationHelper.ValidateNumericFormat(cliente.NumeroContatoCliente, "Formato de telefone invalido.");
                try
                {
                    cliente.CodCliente = 0;
                    cliente.ClienteAtivo = true;
                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                    return Ok("Cliente cadastrado com sucesso.");
                }
                catch (Exception e)
                {
                    if (e.InnerException.Message == "Cannot insert duplicate key row in object 'dbo.Clientes' with unique index 'IX_Clientes_DocIdentificadorCliente'. The duplicate key value is (123123).")
                    {
                        throw new DuplicateUniqueValueException($"O Documento '{cliente.DocIdentificadorCliente}' ja esta cadastrado. Tente Novamente.");
                    }
                    throw;
                }
            }
        }

        [HttpGet("Listar")]
        public List<Cliente> GetTodosClientes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Clientes.ToList();
            }
        }

        [HttpGet("Buscar/{Documento}")]
        public IActionResult GetCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Documento}")]
        public IActionResult PutCliente(string Documento, [FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                ValidationHelper.ValidateNameFormat(cliente.NomeCliente, "Nome invalido.");
                ValidationHelper.ValidateEmailFormat(cliente.EmailCliente, "Email invalido.");
                ValidationHelper.ValidateNumericFormat(cliente.NumeroContatoCliente, "Formato de telefone invalido.");
                try
                {
                    item.NomeCliente = cliente.NomeCliente;
                    item.EmailCliente = cliente.EmailCliente;
                    item.NumeroContatoCliente = cliente.NumeroContatoCliente;
                    _context.SaveChanges();
                    return Ok("Os dados do cliente foram atualizados.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpPut("Desativar/{Documento}")]
        public IActionResult PutDeleteCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }

                var caminhaoCliente = _context.Caminhoes.Where(f => f.FkClientesCodCliente == item.CodCliente);
                foreach (var caminhao in caminhaoCliente)
                {
                    caminhao.CaminhaoAtivo = false;
                }

                item.ClienteAtivo = false;
                _context.SaveChanges();

                return Ok("O cLiente foi desativado.");
            }
        }

        [HttpDelete("Deletar/{Documento}")]
        public IActionResult DeleteCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                ManipulacaoDadosHelper.RegistrarDelete("Clientes", "Cliente", item);
                _context.Clientes.Remove(item);
                _context.SaveChanges();

                return Ok("O cliente foi deletado.");
            }
        }
    }
}
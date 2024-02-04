using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Principal;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        //Cadastra um novo cliente via Form
        [HttpPost("CadastrarCliente")]
        public async Task<IActionResult> CadastrarCliente([FromForm] Cliente mcliente)
        {
                using (var _context = new TrabalhoVolvoContext())
                {
                    if(mcliente.NomeCliente == "1"){throw new ArgumentOutOfRangeException("testando");}
                    _context.Clientes.Add(mcliente);
                    await _context.SaveChangesAsync();
                }
                return Ok("Cliente Cadastrado Com Sucesso.");
        }

        //Joga todos os clientes em uma lista e formata em uma table na view "ListaClientes.cshtml"
        [HttpGet("TodosClientes")]
        public IActionResult GetTodosClientes()
        {
            List<Cliente> ListaClientes;
            using (var _context = new TrabalhoVolvoContext())
            {
                ListaClientes = _context.Clientes.ToList();
            }
            return View("ListaClientes", ListaClientes);
        }

        //Retorna o cliente cujo id é = id fornecido
        [HttpGet("Get/byID/{id}")]
        public IActionResult Get(int id)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.CodCliente == id);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpGet("Get/byDoc/{doc}")]
        public IActionResult GetByCpf(string doc)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == doc);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        //Atualiza os dados do cliente cujo CPJ/CNPJ é = Doc fornecido
        [HttpPut("Update/byDoc/{Doc}")]
        public void Put(string Doc, [FromForm] Cliente mcliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Doc);
                if(item == null)
                {
                    return;
                }
                // _context.Entry(item).CurrentValues.SetValues(mcliente);
                item.NomeCliente = mcliente.NomeCliente;
                item.EmailCliente = mcliente.EmailCliente;
                item.NumeroContatoCliente = mcliente.NumeroContatoCliente;
                _context.SaveChanges();
            }
        }
    }
}

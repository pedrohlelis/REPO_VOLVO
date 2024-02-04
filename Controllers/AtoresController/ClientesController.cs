using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                cliente.CodCliente = 0;
                cliente.ClienteAtivo = true;
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Listar")]
        public List<Cliente> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Clientes.ToList();
            }
        }

        [HttpGet("Buscar/{Documento}")]
        public IActionResult Get(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Documento}")]
        public IActionResult Put(string Documento, [FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeCliente = cliente.NomeCliente;
                item.EmailCliente = cliente.EmailCliente;
                item.NumeroContatoCliente = cliente.NumeroContatoCliente;
                _context.SaveChanges();
                return Ok();
            }
        }

        /* Todos os caminhoes desse cliente ficam inativos? Ele pode deletar a conta?? é preciso isso??
        [HttpPut("Deletar/{Documento}")]
        public void Put(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                if (item == null)
                {
                    return;
                }
                item.ClienteAtivo = false;
                _context.SaveChanges();
            }
        }
        */
    }
}
using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class VendaCaminhaoController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostVendaCaminhao([FromForm] VendaCaminhao vendaCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.Funcionarios.Any(c => c.CodFuncionario == vendaCaminhao.FkFuncionariosCodFuncionario)
                    && _context.EstoqueCaminhao.Any(c => c.CodCaminhaoEstoque == vendaCaminhao.FkEstoqueCaminhoesCodCaminhaoEstoque)
                    && _context.Clientes.Any(c => c.CodCliente == vendaCaminhao.FkClientesCodCliente))
                {
                    vendaCaminhao.CodVenda = 0;
                    _context.VendaCaminhoes.Add(vendaCaminhao);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<VendaCaminhao> GetTodasVendaCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.VendaCaminhoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetVendaCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.VendaCaminhoes.FirstOrDefault(t => t.CodVenda == Codigo);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }
    }
}
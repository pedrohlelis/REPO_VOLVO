using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class CaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostCaminhao([FromForm] Caminhao caminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.Clientes.Any(c => c.CodCliente == caminhao.FkClientesCodCliente)
                && _context.ModelosCaminhoes.Any(c => c.CodModelo == caminhao.FkModelosCaminhoesCodModelo))
                {
                    caminhao.CodCaminhao = 0;
                    caminhao.CaminhaoAtivo = true;
                    _context.Caminhoes.Add(caminhao);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<Caminhao> GetTodosCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Caminhoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }
    
        [HttpPut("Deletar/{Codigo}")]
        public IActionResult PutDeleteCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.CaminhaoAtivo = false;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
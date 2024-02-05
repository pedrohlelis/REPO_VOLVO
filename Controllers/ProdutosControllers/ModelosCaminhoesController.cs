using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ModelosCaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostModelosCaminhao([FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                modelosCaminhao.CodModelo = 0;
                modelosCaminhao.ModelosAtivo  = true;
                _context.ModelosCaminhoes.Add(modelosCaminhao);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPost("Cadastrar/Pecas")]
        public IActionResult PostPecasModelo([FromForm] PecasModelo pecasModelo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.ModelosCaminhoes.Any(c => c.CodModelo == pecasModelo.FkModelosCaminhoesCodModelo)
                && _context.TipoPecas.Any(c => c.CodTipoPeca == pecasModelo.FkTipoPecasCodTipoPeca))
                {
                    pecasModelo.CodPecasModelo = 0;
                    _context.PecasModelos.Add(pecasModelo);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<ModelosCaminhao> GetTodosModelosCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ModelosCaminhoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetModelosCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutModelosCaminhao(int Codigo, [FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeModelo = modelosCaminhao.NomeModelo;
                item.ValorModeloCaminhao = modelosCaminhao.ValorModeloCaminhao;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
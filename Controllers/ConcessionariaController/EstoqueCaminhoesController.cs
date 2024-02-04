using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoqueCaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] CaminhaoEstoque caminhaoEstoque)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.Concessionarias.Any(c => c.CodConc == caminhaoEstoque.FkConcessionariasCodConc)
                    && _context.ModelosCaminhoes.Any(c => c.CodModelo == caminhaoEstoque.FkModelosCaminhaoCodModelo))
                {
                    caminhaoEstoque.CodCaminhaoEstoque = 0;
                    _context.EstoqueCaminhao.Add(caminhaoEstoque);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<CaminhaoEstoque> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoqueCaminhao.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult Delete(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                _context.EstoqueCaminhao.Remove(item);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
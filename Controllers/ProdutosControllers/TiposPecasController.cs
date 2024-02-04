using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class TiposPecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                tipoPeca.CodTipoPeca = 0;
                tipoPeca.PecaAtivo = true;
                _context.TipoPecas.Add(tipoPeca);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Listar")]
        public List<TipoPeca> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.TipoPecas.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TipoPecas.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult Put(int Codigo, [FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TipoPecas.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeTipoPeca = tipoPeca.NomeTipoPeca;
                item.ValorTipoPeca = tipoPeca.ValorTipoPeca;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
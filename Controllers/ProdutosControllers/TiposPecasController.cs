using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class TiposPecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostTiposPeca([FromForm] TipoPeca tipoPeca)
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
        public List<TipoPeca> GetTodosTiposPecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.TipoPecas.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetTiposPeca(int Codigo)
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
        public IActionResult PutTiposPeca(int Codigo, [FromForm] TipoPeca tipoPeca)
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
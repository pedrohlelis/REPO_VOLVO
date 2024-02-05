using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoquePecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoquePeca([FromForm] PecaEstoque pecaEstoque)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.Concessionarias.Any(c => c.CodConc == pecaEstoque.FkConcessionariasCodConc
                    && _context.TipoPecas.Any(c => c.CodTipoPeca == pecaEstoque.FkTipoPecasCodTipoPeca)))
                {
                    pecaEstoque.CodPecaEstoque = 0;
                    _context.EstoquePecas.Add(pecaEstoque);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<PecaEstoque> GetTodasEstoquePecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoquePecas.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                _context.EstoquePecas.Remove(item);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
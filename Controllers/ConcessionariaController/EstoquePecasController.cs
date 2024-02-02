using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoquePecasController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] PecaEstoque pecaEstoque)
        {
            var _context = new TrabalhoVolvoContext();

            if(_context.Concessionarias.Any(c => c.CodConc == pecaEstoque.FkConcessionariasCodConc 
                && _context.TipoPecas.Any(c => c.CodTipoPeca == pecaEstoque.FkTipoPecasCodTipoPeca)))
            {
                _context.EstoquePecas.Add(pecaEstoque);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public List<PecaEstoque> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoquePecas.ToList();
            }
        }

        [HttpGet("{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpDelete("{Codigo}")]
        public void Delete(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if(item == null)
                {
                    return; 
                }
                _context.EstoquePecas.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
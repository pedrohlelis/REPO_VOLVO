using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class TiposPecasController : Controller
    {
        [HttpPost]
        public void Post([FromBody] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                _context.TipoPecas.Add(tipoPeca);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<TipoPeca> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.TipoPecas.ToList();
            }
        }

        [HttpGet("{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TipoPecas.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("{Codigo}")]
        public void Put(int Codigo,[FromBody] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TipoPecas.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if(item == null)
                {
                    return; 
                }
                item.NomeTipoPeca = tipoPeca.NomeTipoPeca;
                item.ValorTipoPeca = tipoPeca.ValorTipoPeca;
                _context.SaveChanges();
            }
        }
    }
}
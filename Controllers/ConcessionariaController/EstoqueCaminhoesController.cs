using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoqueCaminhoesController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] CaminhaoEstoque caminhaoEstoque)
        {
            var _context = new TrabalhoVolvoContext();

            if(_context.Concessionarias.Any(c => c.CodConc == caminhaoEstoque.FkConcessionariasCodConc)
                && _context.ModelosCaminhoes.Any(c => c.CodModelo == caminhaoEstoque.FkModelosCaminhaoCodModelo))
            {
                _context.EstoqueCaminhao.Add(caminhaoEstoque);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public List<CaminhaoEstoque> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoqueCaminhao.ToList();
            }
        }

        [HttpGet("{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
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
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                if(item == null)
                {
                    return; 
                }
                _context.EstoqueCaminhao.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
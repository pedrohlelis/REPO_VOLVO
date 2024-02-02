using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ModelosCaminhoesController : Controller
    {
        [HttpPost]
        public void Post([FromBody] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                _context.ModelosCaminhoes.Add(modelosCaminhao);
                _context.SaveChanges();
            }
        }

        [HttpPost("CadastroPecas")]
        public IActionResult Post([FromBody] PecasModelo pecasModelo)
        {
            var _context = new TrabalhoVolvoContext();

            if (_context.ModelosCaminhoes.Any(c => c.CodModelo == pecasModelo.FkModelosCaminhoesCodModelo)
            && _context.TipoPecas.Any(c => c.CodTipoPeca ==pecasModelo.FkTipoPecasCodTipoPeca))
            {
                _context.PecasModelos.Add(pecasModelo);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public List<ModelosCaminhao> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ModelosCaminhoes.ToList();
            }
        }

        [HttpGet("{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("{Codigo}")]
        public void Put(int Codigo,[FromBody] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if(item == null)
                {
                    return; 
                }
                item.NomeModelo = modelosCaminhao.NomeModelo;
                item.ValorModelosCaminhao = modelosCaminhao.ValorModelosCaminhao;
                _context.SaveChanges();
            }
        }
    }
}
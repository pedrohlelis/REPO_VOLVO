using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ServicoManutencaoController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostServicoManutencao([FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.Funcionarios.Any(c => c.CodFuncionario == servicoManutencao.FkFuncionariosCodFuncionario)
                    && _context.Caminhoes.Any(c => c.CodCaminhao == servicoManutencao.FkEstoqueCaminhaoCodCaminhao))
                {
                    servicoManutencao.CodManutencao = 0;
                    _context.ServicosManutencao.Add(servicoManutencao);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpPost("CadastrarPecaUsada/{CodigoManutencao}/{CodigoPeca}")]
        public IActionResult PostServicoTipoPeca([FromForm] ServicoTipoPeca servicoTipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.ServicosManutencao.Any(c => c.CodManutencao == servicoTipoPeca.FkCodManutencao)
                && _context.TiposPeca.Any(c => c.CodTipoPeca == servicoTipoPeca.FkTiposPecaCodTipoPeca))
                {
                    servicoTipoPeca.CodServicoTipoPeca = 0;
                    _context.ServicoTiposPeca.Add(servicoTipoPeca);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<ServicoManutencao> GetTodosServicosManutencao()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ServicosManutencao.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutServicoManutencao(int Codigo, [FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.DescricaoManutencao = servicoManutencao.DescricaoManutencao;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ServicoManutencaoController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                 if (_context.Funcionarios.Any(c => c.CodFuncionario == servicoManutencao.FkFuncionariosCodFuncionario)
                    && _context.Caminhoes.Any(c => c.CodCaminhao == servicoManutencao.FkEstoqueCaminhaoCodCaminhao))
                {
                    servicoManutencao.CodManutencao = 0;
                    _context.ServicoManutencoes.Add(servicoManutencao);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpPost("CadastrarPecaUsada/{CodigoManutencao}/{CodigoPeca}")]
        public IActionResult Post([FromForm] ServicoTipoPeca servicoTipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.ServicoManutencoes.Any(c => c.CodManutencao == servicoTipoPeca.FkCodManutencao)
                && _context.TipoPecas.Any(c => c.CodTipoPeca == servicoTipoPeca.FkCodTipoPeca))
                {
                    servicoTipoPeca.CodServicoTipoPeca = 0;
                    _context.ServicoTipoPecas.Add(servicoTipoPeca);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpGet("Listar")]
        public List<ServicoManutencao> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ServicoManutencoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicoManutencoes.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult Put(int Codigo, [FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicoManutencoes.FirstOrDefault(t => t.CodManutencao == Codigo);
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
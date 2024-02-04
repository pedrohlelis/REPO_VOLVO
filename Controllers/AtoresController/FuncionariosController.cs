using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionariosController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] Funcionario funcionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                funcionario.CodFuncionario = 0;
                funcionario.FuncionarioAtivo = true;
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Listar")]
        public List<Funcionario> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Funcionarios.ToList();
            }
        }

        [HttpGet("Buscar/{Documento}")]
        public IActionResult Get(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Documento}")]
        public IActionResult Put(string Documento, [FromForm] Funcionario funcionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeFuncionario = funcionario.NomeFuncionario;
                item.NumeroContatoFuncionario = funcionario.NumeroContatoFuncionario;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut("Deletar/{Documento}")]
        public IActionResult Put(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                if (item == null)
                {
                    return NotFound();
                }
                item.FuncionarioAtivo = false;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
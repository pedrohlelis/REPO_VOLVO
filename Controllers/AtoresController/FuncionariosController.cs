using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionariosController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostFuncionario([FromForm] Funcionario funcionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //vai verificar a integridade das FKs fornecidas
                if (!_context.Concessionarias.Any(c => c.CodConc == funcionario.FkCargosCodCargo))
                {
                    throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
                }
                else if (!_context.ModelosCaminhoes.Any(c => c.CodModelo == funcionario.FkConcessionariasCodConc))
                {
                    throw new FKNotFoundException("Nenhum Modelo de caminhao registrado possui esse codigo.");
                }
                // FKs okay, hora de validar os demais campos
                funcionario.CodFuncionario = 0;
                funcionario.FuncionarioAtivo = true;
                ValidationHelper.ValidateNameFormat(funcionario.NomeFuncionario,"Nome invalido.");
                ValidationHelper.ValidateNumericFormat(funcionario.CpfFuncionario,"Formato de CPF invalido.");
                ValidationHelper.ValidateNumericFormat(funcionario.NumeroContatoFuncionario,"Formato de telefone invalido.");
                //se chegou ate aqui significa que as informacoes inseridas estao ok, hora de tentar registrar no bd!!!
                try
                {
                    _context.Funcionarios.Add(funcionario);
                    _context.SaveChanges();
                    return Ok("Funcionario cadastrado com sucesso.");
                }catch(Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("Listar")]
        public List<Funcionario> GetTodosFuncionarios()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Funcionarios.ToList();
            }
        }

        [HttpGet("Buscar/{Documento}")]
        public IActionResult GetFuncionario(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum funcionario foi encontrado com esse CPF.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Documento}")]
        public IActionResult PutFuncionario(string Documento, [FromForm] Funcionario funcionario)
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
        public IActionResult PutDeleteFuncionario(string Documento)
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
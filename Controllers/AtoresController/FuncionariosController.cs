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
                // if (!_context.Concessionarias.Any(c => c.CodConc == funcionario.FkConcessionariasCodConc))
                // {
                //     throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                // }
                // else if (!_context.Cargos.Any(c => c.CodCargo == funcionario.FkCargosCodCargo))
                // {
                //     throw new FKNotFoundException("Nenhum Cargo registrado possui esse codigo.");
                // }

                funcionario.CodFuncionario = 0;
                funcionario.FuncionarioAtivo = true;

                ValidationHelper.ValidateNameFormat(funcionario.NomeFuncionario, "Nome invalido.");
                ValidationHelper.ValidateNumericFormat(funcionario.CpfFuncionario, "Formato de CPF invalido.");
                ValidationHelper.ValidateNumericFormat(funcionario.NumeroContatoFuncionario, "Formato de telefone invalido.");

                try
                {
                    _context.Funcionarios.Add(funcionario);
                    _context.SaveChanges();

                    return Ok("Funcionario cadastrado com sucesso.");
                }
                catch (Exception)
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
            try
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
            catch (Exception)
            {
                throw;
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
                    throw new FKNotFoundException("Nenhum Funcionario foi encontrado com esse CPF.");
                }

                if (!_context.Concessionarias.Any(c => c.CodConc == funcionario.FkConcessionariasCodConc))
                {
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                }
                else if (!_context.Cargos.Any(c => c.CodCargo == funcionario.FkCargosCodCargo))
                {
                    throw new FKNotFoundException("Nenhum Cargo registrado possui esse codigo.");
                }

                ValidationHelper.ValidateNameFormat(funcionario.NomeFuncionario, "Nome invalido.");
                ValidationHelper.ValidateNumericFormat(funcionario.NumeroContatoFuncionario, "Formato de telefone invalido.");
                try
                {
                    item.NomeFuncionario = funcionario.NomeFuncionario;
                    item.NumeroContatoFuncionario = funcionario.NumeroContatoFuncionario;
                    _context.SaveChanges();

                    return Ok("Os dados do funcionario foram atualizados.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpPut("Desativar/{Documento}")]
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

                return Ok("O status do funcionario foi desativado.");
            }
        }

        [HttpDelete("Deletar/{Documento}")]
        public IActionResult DeleteFuncionario(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);

                if (item == null)
                {
                    return NotFound();
                }

                _context.Funcionarios.Remove(item);
                _context.SaveChanges();

                return Ok("O funcionario foi deletado.");
            }
        }
    }
}